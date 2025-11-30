using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EquipmentManager : IEquipmentService
    {
        IEquipmentDal _equipmentDal;
        public EquipmentManager(IEquipmentDal equipmentDal)
        {
            _equipmentDal = equipmentDal;
        }

    
    }
}
