using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Class : IEntity
    {
        public int ClassId { get; set; }
        public int TrainerId { get; set; }
        public string ClassName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime Date { get; set; }
    }
}
