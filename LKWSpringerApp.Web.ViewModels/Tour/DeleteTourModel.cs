﻿namespace LKWSpringerApp.Web.ViewModels.Tour
{
    public class DeleteTourModel
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = null!;
        public int TourNumber { get; set; }
    }
}
