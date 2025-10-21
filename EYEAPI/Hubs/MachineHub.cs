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
    public class MachineHub(MongoClient mongoClient, ILogger<MachineHub> logger) : Hub
    {
        public static string MachineGroupPrefix = "machine_";
        public static string MachinesGroupPrefix = "machines_";
        public async Task<List<Measurement>> SubscribeToMachine(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{MachineGroupPrefix}{group}");

            IMongoDatabase? database = mongoClient.GetDatabase("Eye");
            IMongoCollection<BsonDocument>? collection = database.GetCollection<BsonDocument>("Telemtry");

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

        public async Task UnsubscribeToMachine(string[] groups)
        {
            foreach (string group in groups)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{MachineGroupPrefix}{group}");
        }

        public async Task SubscribeToMachines(string[] groups)
        {
            foreach (string group in groups)
                await Groups.AddToGroupAsync(Context.ConnectionId, $"{MachinesGroupPrefix}{group}");


        }

        public async Task UnsubscribeToMachines(string[] groups)
        {
            foreach (string group in groups)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{MachinesGroupPrefix}{group}");
        }
    }
}