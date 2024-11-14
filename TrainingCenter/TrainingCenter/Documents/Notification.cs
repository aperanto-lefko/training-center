using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Documents
{
    public class Notification : IComparable<Notification>
    {
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public Notification(String description)
        {
            this.Description = description;
            this.DateTime = DateTime.Now;
        }
        public int CompareTo(Notification obj)
        {
            if (DateTime.Compare(this.DateTime, obj.DateTime) == 0) { return 0; } //проверить 
            if (DateTime.Compare(this.DateTime, obj.DateTime) > 0) { return 1; }
            else return -1;
        }
        public override string ToString()
        {
            return $"{Description} Время выполнения {DateTime}";
        }
    }

    
}
