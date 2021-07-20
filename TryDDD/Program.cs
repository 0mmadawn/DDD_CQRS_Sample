using LibraryApplication.Commands.Handlers;
using LibraryApplication.Commands.Requests;
using LibraryApplication.Queries.Handlers;
using LibraryApplication.Queries.RepositoryIf;
using LibraryApplication.Queries.Requets;
using LibraryApplication.Users.Commands.Requests;
using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using LibraryDomain.Models.Users;
using LibraryDomain.Services;
using LibraryInfrastructure;
using LibraryInfrastructure.Books;
using LibraryInfrastructure.Queries;
using LibraryInfrastructure.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TryDDD
{
    class Program
    {
        // DI
        static ServiceProvider ConfigureService()
        {
            var serviceProvider = new ServiceCollection()
                // Repository
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IBookStockRepository, BookStockRepository>()
                .AddTransient<IQueryRepository, QueryRepository>()
                // Factory
                .AddTransient<IBookStockFactory, BookStockFactory>(serviceProvider =>
                    new BookStockFactory(serviceProvider.GetService<IBookStockRepository>()))
                // DomainService
                .AddTransient<BookService>(serviceProvider =>
                    new BookService(serviceProvider.GetService<IBookRepository>()))
                .AddTransient<BookStockService>(serviceProvider =>
                    new BookStockService(serviceProvider.GetService<IBookStockRepository>()))
                // Specification
                .AddTransient<RentalLimitCountSpecification>(serviceProvider =>
                    new RentalLimitCountSpecification(serviceProvider.GetService<IBookStockRepository>()))
                // Command handler
                .AddTransient<RegisterUserHandler>(serviceProvider =>
                    new RegisterUserHandler(serviceProvider.GetService<IUserRepository>()))
                .AddTransient<RegisterBookHandler>(serviceProvider =>
                    new RegisterBookHandler(
                        serviceProvider.GetService<IBookRepository>(),
                        serviceProvider.GetService<IBookStockRepository>(),
                        serviceProvider.GetService<IBookStockFactory>()
                    ))
                .AddTransient<LendBookHandler>(serviceProvider =>
                    new LendBookHandler(
                        serviceProvider.GetService<IBookStockRepository>(),
                        serviceProvider.GetService<BookService>(),
                        serviceProvider.GetService<BookStockService>()
                    ))
                // Query handler
                .AddTransient<GetUserHandler>(serviceProvider =>
                    new GetUserHandler(serviceProvider.GetService<IQueryRepository>()))
                .AddTransient<GetAllUserHandler>(serviceProvider =>
                    new GetAllUserHandler(serviceProvider.GetService<IQueryRepository>()))
                .AddTransient<GetAllBooksHandler>(serviceProvider =>
                    new GetAllBooksHandler(serviceProvider.GetService<IQueryRepository>()))
                // Build
                .BuildServiceProvider();
            return serviceProvider;
        }

        static void Main(string[] args)
        {
            InitSqlite3.SetUp();
            var serviceProvider = ConfigureService();

            // register users
            var registerUserHandler = serviceProvider.GetService<RegisterUserHandler>();
            registerUserHandler.Handle(new RegisterUserCommand("John", "Petrucci"));
            registerUserHandler.Handle(new RegisterUserCommand("Jordan", "Rudess"));
            registerUserHandler.Handle(new RegisterUserCommand("Mike", "Portnoy"));
            registerUserHandler.Handle(new RegisterUserCommand("Tony", "Levin"));

            // get all users
            var getAllUserHandler = serviceProvider.GetService<GetAllUserHandler>();
            var resUsers = getAllUserHandler.Handle();
            Console.WriteLine("---users---");
            foreach (var i in resUsers)
            {
                Console.WriteLine($"{i.Id} | {i.FirstName} {i.FamilyName}");
            }
            Console.WriteLine("-----------");

            // register books
            var registerBookHandler = serviceProvider.GetService<RegisterBookHandler>();
            registerBookHandler.Handle(new RegisterBookCommand("978-0008322069", "1984 Nineteen Eighty-Four", 1));
            registerBookHandler.Handle(new RegisterBookCommand("978-0141033006", "The Day of the Triffids", 3));

            // get some user(John Petrucci)
            var getUserHandler = serviceProvider.GetService<GetUserHandler>();
            var getUserRes = getUserHandler.Handle(new GetUserQuery("John", "Petrucci"));
            var someUserId = getUserRes.Id;

            // lend a book(lend 1984 to John Petrucci)
            var lendBookHandler = serviceProvider.GetService<LendBookHandler>();
            var lendResult = lendBookHandler.Handle(new LendBookCommand(someUserId, "978-0008322069"));

            // get all books and their checkout status
            var getAllBooksHandler = serviceProvider.GetService<GetAllBooksHandler>();
            var resBooks = getAllBooksHandler.Handle();
            Console.WriteLine("---books---");
            foreach (var i in resBooks)
            {
                var status = (i.IsAvailable) ? "Available" : "-";
                Console.WriteLine($"{i.Id} | {i.Name} | {status}");
            }
            Console.WriteLine("-----------");

            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
