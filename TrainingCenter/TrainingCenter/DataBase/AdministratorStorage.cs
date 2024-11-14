using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.DataBase
{
    public class AdministratorStorage : DataBaseStorage<Administrator>
    {

        public override List<Administrator> createListEmployee()
        {
            return new List<Administrator>
            {
    new Administrator("Васильев Александр Петрович", 3285, new DateTime(1985, 10, 21), 5, 40000)
            };
        }

    }

}

