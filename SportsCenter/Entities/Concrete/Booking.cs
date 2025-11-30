using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Booking : IEntity
    {
        public int BookingId { get; set; }
        public int MemberId { get; set; }
        public int ClassId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Active";
        public decimal Price { get; set; }
    }
}
