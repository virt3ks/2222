using Microsoft.EntityFrameworkCore;

namespace _2222
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");


            var db = new AppDbContext();

            var student = new Student
            {
                FullName = "Illya",
                Age = 16,
                Grades = 12,
                Email = "example@gmail.com"
            };

            //db.Students.Add(student);
            //db.SaveChanges();

            var students = db.Students.ToList();

            foreach (var st in students) 
            {
                Console.WriteLine($"Id: {st.Id} - {st.FullName} - {st.Age} - {st.Grades} - {st.Email}");    
            }

        }
    }
}
