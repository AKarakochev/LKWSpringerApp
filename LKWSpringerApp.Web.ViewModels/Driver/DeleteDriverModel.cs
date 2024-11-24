using System;
using System.Collections.Generic;
namespace LKWSpringerApp.Web.ViewModels.Driver
{
    public class DeleteDriverModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
    }
}
