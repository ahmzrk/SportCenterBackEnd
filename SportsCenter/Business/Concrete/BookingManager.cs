using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookingManager : IBookingService
    {
        IBookingDal _bookingDal;
        public BookingManager(IBookingDal bookingDal)
        {
                _bookingDal = bookingDal;
        }

        public IResult Add(int classId, int memberId)
        {
            _bookingDal.Add(new Booking
            {
                ClassId = classId,
                MemberId = memberId,
                BookingDate = DateTime.Now,
                Status = "Pending"
            });
            return new SuccessResult("Rezervasyon Talebi Oluşturuldu.");
        }

        public IDataResult<List<Booking>> GetAllBookingsAvailable()
        {
            return new SuccessDataResult<List<Booking>>(
                _bookingDal.GetAll(b => b.Status == "Active"),
                "Müsait Rezervasyonlar Listelendi."
            );
        }
    }
}
