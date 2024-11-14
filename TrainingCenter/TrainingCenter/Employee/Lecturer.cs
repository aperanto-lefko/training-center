using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Employee
{
    public class Lecturer : Person<Lecturer> 
    {
        public override string JobTitle { get; } = "Преподаватель";
       
        public Lecturer(string name, long password, DateTime dateBirth, int experience, long salary)
            : base(name, password, dateBirth, salary)
        {
            Experience = experience;
        }
    }
}
