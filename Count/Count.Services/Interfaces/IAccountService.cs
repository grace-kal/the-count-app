﻿using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> FindUserByUsername(string username);
    }
}
