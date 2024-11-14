using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Employee
{
    public interface IEmployee
    {
        string Name { get; }
        long Password { get; set; }
        long Id { get; set; }
        long Salary {  get; set; }
        int Experience { get; set; }

        DateTime DateBirth { get; set; }

        
       void SubmitAResignationLetter(Administrator admin); //подать заявку об увольнении

        void GetSalaryInformation(Manager manager); //получить сведения о заработной плате


    }
}
