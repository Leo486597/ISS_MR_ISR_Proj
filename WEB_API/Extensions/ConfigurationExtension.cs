using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WEB_API.Extensions
{
    public static class ConfigurationExtension
    {
        public static (string, string, string) GetMongoDBConfig(this IConfiguration configuration, string documentName)
        {
            var mongoSection = configuration.GetSection("MongoDB");
            var connectionString = mongoSection["ConnectionString"];

            var documentSection = mongoSection.GetSection(documentName);
            var databaseName = documentSection["DatabaseName"];
            var collectionName = documentSection["CollectionName"];
            
            Serilog.Log.Logger.Debug($"GetMongoDBConfig, {(connectionString, databaseName, collectionName)}");
            return (connectionString, databaseName, collectionName);
        }

        public static void PingMongoDB(IMongoDatabase database)
        {
            var isValid = database.RunCommandAsync(
                    (Command<BsonDocument>)"{ping:1}")
                .Wait(1000);
            
            if(isValid)
                Serilog.Log.Logger.Debug("MonogDB Connection Valid");
            else
            {
                Serilog.Log.Logger.Error("MonogDB Connection is not Valid");
                throw new ArgumentException("Please make sure the MongoDb config in appsettings.json is correct or make sure the MongoDB server is running");
            }
        }
    }
}