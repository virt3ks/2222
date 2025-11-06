using Microsoft.EntityFrameworkCore;

namespace _2222
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 — Додати студента");
            Console.WriteLine("2 — Оновити студента");
            Console.WriteLine("3 — Видалити студента");
            Console.WriteLine("4 — Вивести всіх");
            Console.WriteLine("5 — Вийти");
            Console.Write("Виберіть: ");

            var choice = Console.ReadLine();

            if (choice == "1")
                AddStudent();
            else if (choice == "2")
                UpdateStudent();
            else if (choice == "3")
                DeleteStudent();
            else if (choice == "4")
                ShowAllStudents();
            else if (choice == "5")
                return; 
            else
                Console.WriteLine("Невірний вибір");

            Menu(); 
        }

        static void AddStudent()
        {
            using var db = new AppDbContext();

            Console.Write("Введіть імʼя: ");
            string name = Console.ReadLine();

            Console.Write("Вік: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Оцінка: ");
            int grades = int.Parse(Console.ReadLine());

            Console.Write("Email: ");
            string email = Console.ReadLine();

            var student = new Student
            {
                FullName = name,
                Age = age,
                Grades = grades,
                Email = email
            };

            db.Students.Add(student);
            db.SaveChanges();

            Console.WriteLine("Студента додано!");
        }

        static void UpdateStudent()
        {
            using var db = new AppDbContext();

            Console.Write("Введіть Id: ");
            int id = int.Parse(Console.ReadLine());

            var st = db.Students.FirstOrDefault(x => x.Id == id);

            if (st == null)
            {
                Console.WriteLine("Студент не знайдений");
                return;
            }

            Console.Write($"Нове імʼя ({st.FullName}): ");
            st.FullName = Console.ReadLine();

            Console.Write($"Новий вік ({st.Age}): ");
            st.Age = int.Parse(Console.ReadLine());

            Console.Write($"Нова оцінка ({st.Grades}): ");
            st.Grades = int.Parse(Console.ReadLine());

            Console.Write($"Новий email ({st.Email}): ");
            st.Email = Console.ReadLine();

            db.SaveChanges();

            Console.WriteLine("Оновлено!");
        }

        static void DeleteStudent()
        {
            using var db = new AppDbContext();

            Console.Write("Введіть Id: ");
            int id = int.Parse(Console.ReadLine());

            var st = db.Students.FirstOrDefault(x => x.Id == id);

            if (st == null)
            {
                Console.WriteLine("Студент не знайдений");
                return;
            }

            db.Students.Remove(st);
            db.SaveChanges();

            Console.WriteLine("Видалено!");
        }

        static void ShowAllStudents()
        {
            using var db = new AppDbContext();

            var students = db.Students.ToList();

            if (!students.Any())
            {
                Console.WriteLine("Список порожній");
                return;
            }

            Console.WriteLine("\n===== УСІ СТУДЕНТИ =====");
            foreach (var s in students)
                Console.WriteLine($"Id:{s.Id} | {s.FullName} | {s.Age} | {s.Grades} | {s.Email}");
        }
    }
}
