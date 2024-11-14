using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrainingCenter.Employee
{
    public class Student:Person<Student> //подает заявку на экзамен, принятие,отчисление
    {
        public override string JobTitle { get; } = "Студент";

        public Student(string name, long password, DateTime dateBirth, long salary)
            : base(name, password, dateBirth,salary) { }
    }
}

