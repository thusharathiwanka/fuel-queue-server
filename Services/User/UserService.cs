﻿using System;
using MongoDB.Driver;
using fuel_queue_server.Models;
using fuel_queue_server.Services;
using fuel_queue_server.Models.Database;
using MongoDB.Bson;

namespace fuel_queue_server.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            Console.Write(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(settings.UserCollectionName);
        }

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Delete(string id)
        {
            _user.DeleteOne(user => user.Id == id);
        }

        public List<User> Get()
        {
            return _user.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _user.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Update(string id, User user)
        {
            _user.ReplaceOne(user => user.Id == id, user);
        }

        public User Login(string username, string password)
        {
            var document = new BsonDocument
            {
                { "username", username },
                { "password", password}
            };

            return _user.Find(document).FirstOrDefault();
        }
    }
}

