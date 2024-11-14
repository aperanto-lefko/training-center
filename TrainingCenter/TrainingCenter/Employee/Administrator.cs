using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using TrainingCenter.DataBase;
using TrainingCenter.Documents;
using static TrainingCenter.Employee.Administrator;

namespace TrainingCenter.Employee
{
    public class Administrator : Person<Administrator>, IEmployee //нанимает, увольняет
    {
        public override string JobTitle { get; } = "Администратор";

        public List<Application> ApplicationList { get; private set; } //список заявок на увольнение

        public delegate void ActionsWithUsers(Notification note);
        public event ActionsWithUsers EventActionsWithUsers;

        public Administrator(string name, long password, DateTime dateBirth, int experience, long salary)
            : base(name, password, dateBirth, salary)
        {
            Experience = experience;
            ApplicationList = new List<Application>();

        }

        public void CreateEmployee(StudentStorage sStorage, LecturerStorage lStorage, ManagerStorage mStorage)
        {
            try
            {
                Console.WriteLine("На какую должность вы претендуете? \n" +
                    "1.Студент \n" +
                    "2.Преподаватель \n" +
                    "3.Менеджер \n");
                int jobTitle = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите Ваше в формате Фамилия Имя Отчество");
                string name = Console.ReadLine();
                Console.WriteLine("Установите пароль");
                long password = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите дату рождения формате год-месяц-число");
                DateTime dateBirth = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Укажите стаж работы");
                int experience = int.Parse(Console.ReadLine());
                switch (jobTitle)
                {
                    case 1:
                        Student student = new Student(name, password, dateBirth, 5000);
                        sStorage.addToStorage(student);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{student.Name} принят на работу. Исполнитель {this.Name}.")));
                        break;
                    case 2:
                        Lecturer lecturer = new Lecturer(name, password, dateBirth, experience, 50000);
                        lStorage.addToStorage(lecturer);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{lecturer.Name} принят на работу. Исполнитель {this.Name}.")));
                        break;
                    case 3:
                        Manager manager = new Manager(name, password, dateBirth, experience, 30000);
                        mStorage.addToStorage(manager);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{manager.Name} принят на работу. Исполнитель {this.Name}.")));
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UsersList(StudentStorage sStorage, LecturerStorage lStorage, ManagerStorage mStorage)
        {

            foreach (Student value in sStorage.Storage.Values)
            {
                Console.WriteLine(value + " пароль " + value.Password);
            }
            foreach (Lecturer value in lStorage.Storage.Values)
            {
                Console.WriteLine(value + " пароль " + value.Password);
            }
            foreach (Manager value in mStorage.Storage.Values)
            {
                Console.WriteLine(value + " пароль " + value.Password);
            }
        }

        public void Dismissal(StudentStorage sStorage, LecturerStorage lStorage, ManagerStorage mStorage)
        {
            if (ApplicationList.Count == 0)
            {
                Console.WriteLine("Список заявок на увольнение пуст");

            }
            else
            {
                ApplicationList.ForEach(x => Console.WriteLine(x));
                foreach (Application application in ApplicationList)
                {
                    if (application.Employee is Student)
                    {
                        sStorage.removeFromStorage((Student)application.Employee);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{application.Employee.Name} отчислен. Исполнитель {this.Name}.")));
                        continue;

                    }
                    else if (application.Employee is Lecturer)
                    {
                        lStorage.removeFromStorage((Lecturer)application.Employee);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{application.Employee.Name} уволен. Исполнитель {this.Name}.")));
                        continue;
                    }
                    else if (application.Employee is Manager)
                    {
                        mStorage.removeFromStorage((Manager)application.Employee);
                        this.EventActionsWithUsers?.Invoke(new Notification(($"{application.Employee.Name} уволен. Исполнитель {this.Name}.")));
                        continue;
                    }
                }
                Console.WriteLine("Все заявки отработаны");

            }
        }
        public void AddToApplicationStorage(Application application)
        {
            ApplicationList.Add(application);
        }


    }
}
