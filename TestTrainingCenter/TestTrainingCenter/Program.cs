using System.ComponentModel.DataAnnotations;
using TrainingCenter.ExceptionType;
using static TestTrainingCenter.EmployeeService;
using TrainingCenter.Employee;
using System.ComponentModel.Design;

namespace TestTrainingCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Вас приветствует пользовательская система Учебного центра \"Новые горизонты\"");
                EmployeeService service = new EmployeeService();
                while (true)
                {
                    Console.WriteLine("Введите пароль");
                    IEmployee emp = service.CheckPassword(int.Parse(Console.ReadLine()));
                    if (emp is Administrator) { service.AdminWork(); }
                    else if (emp is Lecturer) { service.LecturerWork(); }
                    else if (emp is Manager) { service.ManagerWork(); }
                    else if (emp is Student) { service.StudentWork(); }
                    Console.WriteLine("Сменить пользователя? \t 1.Да \t 2.Нет.Выйти из приложения \t 3. Получить журнал логирования(для администраторов)");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            continue;
                        case 2:
                            return;
                        case 3:
                            Console.WriteLine("Введите пароль администратора");
                            service.PrintLog(int.Parse(Console.ReadLine()));
                            return;
                        default:
                            continue;
                    }
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                Main(args);
            }
            catch (BadRequestException ex)
            {
                Console.WriteLine(ex.Message);
                Main(args);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main(args);
            }
        }


    }
}
