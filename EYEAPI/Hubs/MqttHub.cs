using EYEAPI.BackgroundServices;
using EYEAPI.Models.Entities;
using EYEAPI.Services.MqttService;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MQTTnet;
using System.Linq.Expressions;

namespace EYEAPI.Hubs
{
    public class MqttHub(MongoClient mongoClient) : Hub
    {
        public async Task SendMessage(string payload)
        {
            await Clients.All.SendAsync("ReceiveMessage", payload);
        }

        public async Task<List<Measurement>> Subscribe(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            var filterDefinition = Builders<Measurement>.Filter.Where(machineId => machineId.MachineId == int.Parse(group));

            var database = mongoClient.GetDatabase("SensorData");
            var collection = database.GetCollection<BsonDocument>("Sensor");

            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument(nameof(Measurement.MachineId).Camelize(), int.Parse(group))),
                new BsonDocument("$sort", new BsonDocument(nameof(Measurement.ReadingTime).Camelize(), -1)),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", $"${nameof(Measurement.MeasurementType).Camelize()}" },
                    { "latestDocument", new BsonDocument("$first", "$$ROOT") }
                }),
                new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$latestDocument")),
            };

            var aggregation = await collection.AggregateAsync<BsonDocument>(pipeline);
            var bsonList = await aggregation.ToListAsync();
            var measurements = bsonList.Select(bson => BsonSerializer.Deserialize<Measurement>(bson)).ToList();

            return measurements;
        }
    }
}