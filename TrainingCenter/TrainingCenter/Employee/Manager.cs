using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Documents;

namespace TrainingCenter.Employee
{
    public class Manager : Person<Manager>
    {
        public override string JobTitle { get; } = "Менеджер";

        public List<Application> SalaryList { get; private set; } //список заявок на зарплату
        public Manager(string name, long password, DateTime dateBirth, int experience, long salary)
            : base(name, password, dateBirth, salary)
        {
            Experience = experience;
            SalaryList = new List<Application>();
        }

        public void PayrollCalculation()
        {
            if (SalaryList.Count == 0)
            {
                Console.WriteLine("Список заявок на расчет зар.платы пуст");
            }
            else
            {
                SalaryList.ForEach(x => {Console.WriteLine(x);});
                foreach (Application app in SalaryList)
                {
                    if (app.Employee is Student)
                    {
                        Console.WriteLine("Отчет по " + app.Employee.Name + ". Ваша стипендия составляет " + app.Employee.Salary);
                    }
                    else if (app.Employee is Lecturer)
                    {
                        Console.WriteLine("Отчет по " + app.Employee.Name + ". Ежемесячная ставка по зар.плате составляет " + app.Employee.Salary +
                            ". Дополнительная доплата за каждые 10 лет стажа 2000 р. Итого с учетом надбавок " +
                            (app.Employee.Salary + (int)(app.Employee.Experience / 10) * 2000));
                    }
                    else if (app.Employee is Manager)
                    {
                        Console.WriteLine("Отчет по " + app.Employee.Name + ". Ежемесячная ставка по зар.плате составляет " + app.Employee.Salary +
                            ". Дополнительная доплата за каждые 10 лет стажа 1000 р. Итого с учетом надбавок " +
                            (app.Employee.Salary + (int)(app.Employee.Experience / 10) * 1000));
                    }
                }

            }

        }
    }
}

