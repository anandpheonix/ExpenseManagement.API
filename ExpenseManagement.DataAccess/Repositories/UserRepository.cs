﻿using DataAccess.DBContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using static Common.UserDefinedConstants;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ExpensesContext dbContext;

    public UserRepository(ExpensesContext dBContext)
    {
        this.dbContext = dBContext;
    }

    public async Task<Tuple<Users, string>> AuthenticateUser(string username, string password)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Name == username);

        if (user is null)
        {
            return new Tuple<Users, string>(null, InvalidUser);
        }

        if (user.Password != password)
        {
            return new Tuple<Users, string>(null, InvalidPassword);
        }

        if (user.IsActive is false)
        {
            return new Tuple<Users, string>(null, InactiveUser);
        }

        return new Tuple<Users, string>(user, ValidUser);
    }
}
