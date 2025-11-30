using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TrainerManager : ITrainerService
    {
        ITrainerDal _trainerDal;
        public TrainerManager(ITrainerDal trainerDal)
        {
                _trainerDal = trainerDal;
        }
    }
}
