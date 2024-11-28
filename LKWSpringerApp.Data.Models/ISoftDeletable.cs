using System;
namespace LKWSpringerApp.Data.Models
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
