﻿namespace StayFit.Services.Data
{
    using StayFit.Data.Models;

    public interface IUsersService
    {
        double CalculateUserCalories(ApplicationUser user);
    }
}