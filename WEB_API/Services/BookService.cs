using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WEB_API.Extensions;
using WEB_API.Models;

namespace BooksApi.Services
{
    public class BookService : IBookService
    {
        public const string DocumentName = "BookDocument";
        
        private readonly IMongoCollection<Book> _books;
        private readonly ILogger<BookService> _logger;

        public BookService(
            ILogger<BookService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            
            var (connectionString, databaseName, collectionName) =
                configuration.GetMongoDBConfig(DocumentName);

            var client = new MongoClient(connectionString);
            
            var database = client.GetDatabase(databaseName);
            
            ConfigurationExtension.PingMongoDB(database);
            
            _books = database.GetCollection<Book>(collectionName);
        }

        public async Task InsertDemo()
        {
            try
            {
                var books = @"[{'Name':'Design Patterns','Price':54.93,'Category':'Computers','Author':'Ralph Johnson'}, {'Name':'Clean Code','Price':43.15,'Category':'Computers','Author':'Robert C. Martin'}]";
                await _books.InsertManyAsync(JsonConvert.DeserializeObject<List<Book>>(books));
                
                _logger.LogInformation("Insert successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);
    }
}