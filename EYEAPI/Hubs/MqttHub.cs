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
using MongoDB.Bson.Serialization.Serializers;

namespace EYEAPI.Hubs
{
    public class MqttHub(MongoClient mongoClient, ILogger<MqttHub> logger) : Hub
    {
        public async Task SendMessage(string payload)
        {
            await Clients.All.SendAsync("ReceiveMessage", payload);
        }

        public async Task<List<Measurement>> Subscribe(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            IMongoDatabase? database = mongoClient.GetDatabase("SensorData");
            IMongoCollection<BsonDocument>? collection = database.GetCollection<BsonDocument>("Sensor");

            BsonDocument[] pipeline =
            [
                new BsonDocument("$match", new BsonDocument(nameof(Measurement.MachineId).Camelize(), int.Parse(group))),
                new BsonDocument("$sort", new BsonDocument(nameof(Measurement.ReadingTime).Camelize(), -1)),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", $"${nameof(Measurement.MeasurementType).Camelize()}" },
                    { "latestDocument", new BsonDocument("$first", "$$ROOT") }
                }),
                new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$latestDocument")),
                new BsonDocument("$project", new BsonDocument("_id", 0))
            ];


            var aggregation = await collection.AggregateAsync(PipelineDefinition<BsonDocument, Measurement>.Create(pipeline));
            List<Measurement>? measurements = await aggregation.ToListAsync();
            return measurements;
        }
    }
}