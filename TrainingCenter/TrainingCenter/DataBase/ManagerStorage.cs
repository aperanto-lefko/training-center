using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.DataBase
{
    public class ManagerStorage : DataBaseStorage<Manager>
    {
        public override List<Manager> createListEmployee()
        {
            return new List<Manager>
            {
            new Manager("Сергеев Степан Петрович", 5645, new DateTime(1984, 06, 09), 15, 30000),
            new Manager("Голышева Вера Степановна",3469, new DateTime(1982, 11, 25), 17, 30000)
            };
        }
    }
}
