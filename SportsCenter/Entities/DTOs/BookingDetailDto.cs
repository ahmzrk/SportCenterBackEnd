using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BookingDetailDto : IDto
    {
        public int BookingId { get; set; }
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }

}
