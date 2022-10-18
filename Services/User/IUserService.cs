﻿using System;
using fuel_queue_server.Models;

namespace fuel_queue_server.Services
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string id);
        User Login(string username, string password);
        User Create(User user);
        void Update(string id, User user);
        void Delete(string id);
    }
}

