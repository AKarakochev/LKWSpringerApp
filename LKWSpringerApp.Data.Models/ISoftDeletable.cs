using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Data.Models
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
