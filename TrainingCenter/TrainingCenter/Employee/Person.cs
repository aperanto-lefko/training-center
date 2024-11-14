using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrainingCenter.Attributes;
using TrainingCenter.Documents;
using TrainingCenter.ExceptionType;

namespace TrainingCenter.Employee
{
    public abstract class Person<T> : IEmployee
    {
        public abstract string JobTitle { get; }
        public long Password { get; set; }
        protected static long counter = 0;
        public DateTime dateBirth;
        public string name;
        public long Salary {  get; set; }
        public long Id { get; set; }
        public int Experience { get; set; }

        [Required(ErrorMessage = "Поле \"Имя\" не может быть пустым")]
        public string Name
        {
            get => name;
            set
            {
                Validate(nameof(Name), value);
                name = value;
            }
        }

        [DataValid]
        public DateTime DateBirth
        {
            get => dateBirth;


            set
            {
                Validate(nameof(DateBirth), value);
                dateBirth = value;
            }
        }
        public delegate void ApplCreate(Notification note);
        public event ApplCreate EventApplication;
        protected Person(string name, long password, DateTime dateBirth, long salary)
        {
            Id = ++counter;
            Name = name;
            DateBirth = dateBirth;
            Password = password;
            Salary = salary;
            
        }

       
        public void SubmitAResignationLetter(Administrator admin) //заявление на увольнение
        {
            if (admin == null) 
            { 
                throw new BadRequestException("Подходящего администратора для обработки вашей заявки нет");
            }
            Application application = new Application("Заявление на увольнение", this, admin);
            admin.ApplicationList.Add(application);
            Console.WriteLine("Заявление на увольнение составлено. Будет обработано администратором в ближайшее время.");
            Console.WriteLine("Вашу заявку обрабатывает " + admin.Name);
            this.EventApplication?.Invoke(new Notification(($"Заявление на увольнение от пользователя {this.Name}. Исполнитель {admin.name}.")));
        }

        public void GetSalaryInformation(Manager manager)
        {
            if (manager == null)
            {
                throw new BadRequestException("Подходящего администратора для обработки вашей заявки нет");
            }
            Application application = new Application("Заявление на расчет заработной платы", this, manager);
            manager.SalaryList.Add(application);
            Console.WriteLine("Заявление на расчет зар.платы составлено. Будет обработано менеджером в ближайшее время");
            Console.WriteLine("Вашу заявку обрабатывает " + manager.Name);
            this.EventApplication?.Invoke(new Notification(($"Заявление на расчет заработной платы для {this.Name}. Исполнитель {manager.name}.")));
        }
        private void Validate(string propertyName, object value)
        {
            var validationContext = new ValidationContext(this) { MemberName = propertyName };
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(value, validationContext, results);
            if (!isValid)
            {
                foreach (var result in results)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                throw new ValidationException($"Ошибка валидации для свойства {propertyName}");
            }

        }
        public override string ToString()
        {
            return string.Format($"Пользователь {Name}.\n Статус {JobTitle}.\n Дата рождения {DateBirth.ToString("dd-MM-yyyy")}");
        }

    }
}
