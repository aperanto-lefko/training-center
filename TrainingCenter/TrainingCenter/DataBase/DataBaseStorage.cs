using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;

namespace TrainingCenter.DataBase
{
    public abstract class DataBaseStorage<T> where T:IEmployee
    {
        public Dictionary<long, T> Storage { get; set; }

        public DataBaseStorage()
        {
            Storage = new Dictionary<long, T>();
            activationBase(Storage, createListEmployee());
        }

        public abstract List<T> createListEmployee();

        public void activationBase(Dictionary<long, T> storage, List<T> employees)
        {
            employees.ForEach(person => storage.Add(person.Id, person));
        }
        public void addToStorage(T employee)
        {
            Storage.Add(employee.Id, employee);
        }

        public void removeFromStorage(T employee)
        {
            Storage.Remove(employee.Id);
        }
    }
}
