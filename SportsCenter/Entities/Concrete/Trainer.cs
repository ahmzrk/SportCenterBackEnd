using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Trainer : IEntity
    {
        public int TrainerId { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsAvailable { get; set; } = true; 
    }
}
