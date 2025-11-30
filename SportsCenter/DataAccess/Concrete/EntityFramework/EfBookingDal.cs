using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookingDal : EfEntityRepositoryBase<Booking, DatabaseContext>, IBookingDal
    {
        public List<BookingDetailDto> GetBookingDetail(int BookingId)
        {
            using (var context = new DatabaseContext())
            {
                var result = from b in context.Bookings
                             join m in context.Members on b.MemberId equals m.MemberId
                             where b.BookingId == BookingId
                             select new BookingDetailDto
                             {
                                 BookingId = b.BookingId,
                                 MemberId = m.MemberId,
                                 FullName = m.FullName,
                                 BookingDate = b.BookingDate
                             };
                return result.ToList();
            }
        }
    }
}
