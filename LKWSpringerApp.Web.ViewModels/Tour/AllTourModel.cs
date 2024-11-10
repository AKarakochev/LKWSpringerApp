﻿using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace LKWSpringerApp.Web.ViewModels.TourModels
{
    public class AllTourModel
    {
        public Guid Id { get; set; }
        public int TourNumber { get; set; }
        public string TourName { get; set; } = null!;
        public bool IsDeleted { get; set; }

        // List of associated clients
        public List<ClientModel> Clients { get; set; } = new List<ClientModel>();
    }

    public class ClientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
