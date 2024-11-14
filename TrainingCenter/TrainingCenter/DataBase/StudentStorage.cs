using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.DataBase
{
    public class StudentStorage : DataBaseStorage<Student>
    {
        public override List<Student> createListEmployee()
        {
            return new List<Student>
            {
            new Student("Колесникова Светлана Петровна", 2154, new DateTime(1995, 03, 04), 5000),
            new Student("Колесов Александр Степанович", 5689, new DateTime(1995, 05, 01), 5000)
            };
        }
    }
}
