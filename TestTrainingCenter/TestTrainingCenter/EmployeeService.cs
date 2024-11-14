using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Employee;
using TrainingCenter.DataBase;
using TrainingCenter.Attributes;
using TrainingCenter.ExceptionType;
using System.ComponentModel.DataAnnotations;
using TrainingCenter.Documents;
using System.Xml;



namespace TestTrainingCenter
{
    internal class EmployeeService
    {

        private AdministratorStorage AStorage = new AdministratorStorage();
        private StudentStorage SStorage = new StudentStorage();
        private ManagerStorage MStorage = new ManagerStorage();
        private LecturerStorage LStorage = new LecturerStorage();
        private Administrator admin;
        private Lecturer lecturer;
        private Manager manager;
        private Student student;
        public List<Notification> NotificationList { get; private set; } = new List<Notification>();


        public IEmployee CheckPassword(int password)
        {
            foreach (Administrator valueA in AStorage.Storage.Values)
            {
                if (valueA.Password == password)
                {
                    admin = valueA;
                    admin.EventActionsWithUsers += EventHandler;

                    return admin;

                }
                foreach (Manager valueM in MStorage.Storage.Values)
                {
                    if (valueM.Password == password)
                    {
                        manager = valueM;
                        manager.EventApplication += EventHandler;
                        return manager;

                    }
                }
                foreach (Lecturer valueL in LStorage.Storage.Values)
                {
                    if (valueL.Password == password)
                    {
                        lecturer = valueL;
                        lecturer.EventApplication += EventHandler;
                        return lecturer;

                    }
                }
                foreach (Student valueS in SStorage.Storage.Values)
                {
                    if (valueS.Password == password)
                    {
                        student = valueS;
                        student.EventApplication += EventHandler;
                        return student;

                    }
                }
            }
            if (admin == null && manager == null && lecturer == null && student == null)
            {
                throw new BadRequestException("Неверный код доступа");
            }
            return null;
        }


        public void AdminWork()
        {
            Console.WriteLine($"Здравствуйте, {admin.Name}");
            Console.WriteLine($"Ваш текущий статус: {admin.JobTitle}");
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? " +
                    "\n 1. Посмотреть список пользователей " +
                    "\n 2. Принять пользователя на работу" +
                    "\n 3. Посмотреть список задач на увольнение и произвести увольнение" +
                    "\n 4. Завершить сеанс");

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        admin.UsersList(SStorage, LStorage, MStorage);
                        break;
                    case 2:
                        admin.CreateEmployee(SStorage, LStorage, MStorage);
                        break;
                    case 3:
                        admin.Dismissal(SStorage, LStorage, MStorage);
                        break;
                    case 4:
                        admin.EventActionsWithUsers -= EventHandler;
                        return;
                    default:
                        continue;
                }
            }

        }
        public void ManagerWork()
        {
            Console.WriteLine($"Здравствуйте, {manager.Name}");
            Console.WriteLine($"Ваш текущий статус: {manager.JobTitle}");
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? " +
                    "\n 1. Посмотреть список задач и произвести расчет заработной платы " +
                    "\n 2. Составить заявление на увольнение" +
                    "\n 3. Составить заявление на расчет заработной платы" +
                    "\n 4. Завершить сеанс");

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        manager.PayrollCalculation();
                        break;
                    case 2:
                        Administrator admin = AStorage.Storage.Values.FirstOrDefault();
                        manager.SubmitAResignationLetter(admin);
                        break;
                    case 3:
                        manager.GetSalaryInformation(manager);
                        break;
                    case 4:
                        manager.EventApplication -= EventHandler;
                        return;
                    default:
                        continue;
                }
            }
        }
        public void LecturerWork()
        {
            Console.WriteLine($"Здравствуйте, {lecturer.Name}");
            Console.WriteLine($"Ваш текущий статус: {lecturer.JobTitle}");
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? " +
                    "\n 1. Составить заявление на увольнение" +
                    "\n 2. Составить заявление на расчет заработной платы" +
                    "\n 3. Завершить сеанс");

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Administrator admin = AStorage.Storage.Values.FirstOrDefault();
                        lecturer.SubmitAResignationLetter(admin);
                        break;
                    case 2:
                        Manager manager = MStorage.Storage.Values.FirstOrDefault();
                        lecturer.GetSalaryInformation(manager);
                        break;
                    case 3:
                        lecturer.EventApplication += EventHandler;
                        return;
                    default:
                        continue;
                }
            }
        }
        public void StudentWork()
        {
            Console.WriteLine($"Здравствуйте, {student.Name}");
            Console.WriteLine($"Ваш текущий статус: {student.JobTitle}");
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? " +
                    "\n 1. Составить заявление на отчисление" +
                    "\n 2. Составить заявление на расчет стипендии" +
                    "\n 3. Завершить сеанс");

                    switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Administrator admin = AStorage.Storage.Values.FirstOrDefault();
                        student.SubmitAResignationLetter(admin);
                        break;
                    case 2:
                        Manager manager = MStorage.Storage.Values.FirstOrDefault();
                        student.GetSalaryInformation(manager);
                        break;
                    case 3:
                        student.EventApplication += EventHandler;
                        return;
                    default:
                        continue;
                }
            }
        }
        public void EventHandler(Notification notification)
        {
            NotificationList.Add(notification);
        }

        public void PrintLog(int password)
        {
            List<long> passwords = new List<long>();
            foreach (Administrator admin in AStorage.Storage.Values)
                passwords.Add(admin.Password);
            if (!passwords.Contains(password))
            {
                Console.WriteLine("У вас нет доступа к журналу логирования");
            }
            else
            {
                NotificationList.Sort();
                NotificationList.ForEach(x => Console.WriteLine(x));
                using StreamWriter writer = new StreamWriter("logs.txt");
                    NotificationList.ForEach(x => writer.WriteLine(x));
                Console.WriteLine("Данные записаны в файл logs.txt");
            }
        }
    }
}
