using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ClassManager : IClassService
    {
        IClassDal _classDal;
        public ClassManager(IClassDal classDal)
        {
            _classDal = classDal;
        }

        public IDataResult<List<Class>> GetAllClassesByTrainerId(int id)
        {
            return new SuccessDataResult<List<Class>>(_classDal.GetAll(c => c.TrainerId == id),id+"numaralı idye sahip antrenörün dersleri");
        }
    }
}
