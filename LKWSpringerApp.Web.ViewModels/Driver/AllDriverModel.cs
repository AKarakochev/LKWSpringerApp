﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Web.ViewModels.Driver
{
    public class AllDriverModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public IEnumerable<string> TourNames { get; set; } = new List<string>();
    }
}