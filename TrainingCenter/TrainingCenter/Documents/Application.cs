using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.Documents
{
    public class Application
    {
        public string Description { get; set; }

        public IEmployee Employee { get; set; }

        public IEmployee Executor { get; set; }

        public Application(string description, IEmployee employee, IEmployee executor)
        {
            this.Description = description;
            this.Employee = employee;
            this.Executor = executor;
        }

        public override string ToString()
        {
            return string.Format($"{Description}. От пользователя {Employee.Name}. Исполнитель {Executor.Name}");

        }
    }
}
