using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Events
{
    public class BookingCreatedEvent
    {
        public Guid BookingId { get; set; }
        public Guid MemberId { get; set; }
        public decimal Price { get; set; }
    }
}
