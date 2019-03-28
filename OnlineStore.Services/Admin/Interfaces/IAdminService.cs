using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System;
using System.Collections.Generic;

namespace OnlineStore.Services.Admin.Interfaces
{
    public interface IAdminService
    {
        IndexViewModel PrepareIndexModelForEditing();

        IEnumerable<User> FilterUsersByRegisterDate(DateTime startDate, DateTime endDate);

        IEnumerable<Order> FilterOrdersByDate(DateTime startDate, DateTime endDate);
    }
}
