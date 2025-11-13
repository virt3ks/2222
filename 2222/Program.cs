using System;
using System.Linq;
using _2222.DAL;
using Microsoft.EntityFrameworkCore;

namespace _2222
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== ГОЛОВНЕ МЕНЮ =====");
                Console.WriteLine("1 — Меню авторів");
                Console.WriteLine("2 — Меню книг");
                Console.WriteLine("3 — Вийти");
                Console.Write("Виберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AuthorMenu();
                        break;
                    case "2":
                        BookMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }
        static void AuthorMenu()
        {
            using var db = new AppDbContext();

            while (true)
            {
                Console.WriteLine("\n===== МЕНЮ АВТОРІВ =====");
                Console.WriteLine("1 — Показати всіх авторів");
                Console.WriteLine("2 — Додати автора");
                Console.WriteLine("3 — Оновити дані автора");
                Console.WriteLine("4 — Видалити автора");
                Console.WriteLine("5 — Повернутися в головне меню");
                Console.Write("Виберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var authors = db.Authors.ToList();
                        if (authors.Count == 0)
                            Console.WriteLine("Авторів немає.");
                        else
                            foreach (var a in authors)
                                Console.WriteLine($"ID: {a.Id} | Ім’я: {a.Name}");
                        break;

                    case "2":
                        Console.Write("Введіть ім’я автора: ");
                        var name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            db.Authors.Add(new Author { Name = name });
                            db.SaveChanges();
                            Console.WriteLine("✅ Автора додано!");
                        }
                        break;

                    case "3":
                        Console.Write("Введіть ID автора: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var author = db.Authors.FirstOrDefault(a => a.Id == updateId);
                            if (author == null)
                            {
                                Console.WriteLine("Автор не знайдений.");
                            }
                            else
                            {
                                Console.Write($"Нове ім’я ({author.Name}): ");
                                var newName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newName))
                                    author.Name = newName;
                                db.SaveChanges();
                                Console.WriteLine("✅ Дані оновлено!");
                            }
                        }
                        break;

                    case "4":
                        Console.Write("Введіть ID автора для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            var author = db.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == deleteId);
                            if (author == null)
                            {
                                Console.WriteLine("Автор не знайдений.");
                            }
                            else if (author.Books?.Any() == true)
                            {
                                Console.WriteLine("⚠ У цього автора є книги. Видалення заборонено!");
                            }
                            else
                            {
                                db.Authors.Remove(author);
                                db.SaveChanges();
                                Console.WriteLine("🗑️ Автора видалено!");
                            }
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }
        static void BookMenu()
        {
            using var db = new AppDbContext();

            while (true)
            {
                Console.WriteLine("\n===== МЕНЮ КНИГ =====");
                Console.WriteLine("1 — Показати всі книги");
                Console.WriteLine("2 — Додати книгу");
                Console.WriteLine("3 — Оновити дані книги");
                Console.WriteLine("4 — Видалити книгу");
                Console.WriteLine("5 — Повернутися в головне меню");
                Console.Write("Виберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var books = db.Books.Include(b => b.Author).ToList();
                        if (books.Count == 0)
                            Console.WriteLine("Книг немає.");
                        else
                            foreach (var b in books)
                                Console.WriteLine($"ID: {b.Id} | Назва: {b.Title} | Рік: {b.Year} | Автор: {b.Author?.Name}");
                        break;

                    case "2":
                        Console.Write("Введіть назву книги: ");
                        var title = Console.ReadLine();
                        Console.Write("Введіть рік видання: ");
                        int.TryParse(Console.ReadLine(), out int year);
                        Console.Write("Введіть ID автора: ");
                        int.TryParse(Console.ReadLine(), out int authorId);

                        var author = db.Authors.FirstOrDefault(a => a.Id == authorId);
                        if (author == null)
                        {
                            Console.WriteLine("Автор не знайдений.");
                            break;
                        }

                        var newBook = new Book { Title = title, Year = year, AuthorId = authorId };
                        db.Books.Add(newBook);
                        db.SaveChanges();
                        Console.WriteLine("✅ Книгу додано!");
                        break;

                    case "3":
                        Console.Write("Введіть ID книги: ");
                        if (int.TryParse(Console.ReadLine(), out int updateBookId))
                        {
                            var book = db.Books.FirstOrDefault(b => b.Id == updateBookId);
                            if (book == null)
                            {
                                Console.WriteLine("Книгу не знайдено.");
                            }
                            else
                            {
                                Console.Write($"Нова назва ({book.Title}): ");
                                var newTitle = Console.ReadLine();
                                Console.Write($"Новий рік ({book.Year}): ");
                                int.TryParse(Console.ReadLine(), out int newYear);

                                if (!string.IsNullOrWhiteSpace(newTitle))
                                    book.Title = newTitle;
                                if (newYear > 0)
                                    book.Year = newYear;

                                db.SaveChanges();
                                Console.WriteLine("✅ Дані книги оновлено!");
                            }
                        }
                        break;

                    case "4":
                        Console.Write("Введіть ID книги для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteBookId))
                        {
                            var book = db.Books.FirstOrDefault(b => b.Id == deleteBookId);
                            if (book == null)
                            {
                                Console.WriteLine("Книгу не знайдено.");
                            }
                            else
                            {
                                db.Books.Remove(book);
                                db.SaveChanges();
                                Console.WriteLine("🗑️ Книгу видалено!");
                            }
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }
    }
}
