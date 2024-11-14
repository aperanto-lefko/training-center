using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.DataBase
{
    public class LecturerStorage : DataBaseStorage<Lecturer>
    {

        public override List<Lecturer> createListEmployee()
        {
            return new List<Lecturer>
            {
            new Lecturer("Игнатов Сергей Иванович", 1516, new DateTime(1944, 01, 10), 15, 50000),
            new Lecturer("Смородинова Татьяна Константиновна", 235, new DateTime(1978, 05, 15), 20, 50000)
            };
        }
    }
}
