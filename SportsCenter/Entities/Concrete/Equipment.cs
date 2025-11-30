using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Equipment : IEntity
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime? LastMaintenanceDate { get; set; } 
        public bool IsAvailable { get; set; } = true; 
    }
}
