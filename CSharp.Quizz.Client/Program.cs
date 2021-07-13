using CSharp.Quizz.Common;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace CSharp.Quizz.Client
{
    public static class Program
    {
        private static QuizzRepository _quizzRepository = new();
        private static UserRepository _userRepository = new();

        public static async Task Main(string[] args)
        {
            PrintUsage();

            var option = Console.ReadLine();

            switch (option)
            {
                case "/start":
                    await StartQuizz();
                    break;
                case "/quit":
                    Quit();
                    return;
                default:
                    Console.WriteLine("Invalid parameter, closing");
                    return;
            }
        }

        private static void Quit()
        {
            return;
        }

        private static async Task StartQuizz()
        {
            Console.WriteLine("Welcome to the .NET Quizz!");
            AnsiConsole.WriteLine();
            Console.WriteLine("Please type your name:");
            var username = Console.ReadLine();
            AnsiConsole.WriteLine();
            Console.WriteLine($"Ok {username} now please type your email to initiate your quizz:");
            var email = Console.ReadLine();

            var user = new User(username, email);

            await AnsiConsole.Status().StartAsync($"Preparing your quizz...",
                async ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.Status = "Preparando seu teste...";
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    ctx.Status = "Teste completo! Iniciando...";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                });

            var quizzList = _quizzRepository.RandomizeQuizz();
            Console.Clear();

            foreach (var quizz in quizzList)
            {
                Console.Clear();
                Console.WriteLine(quizz);
                var anwser = Console.ReadLine();
                _userRepository.SaveAnswer(user, quizz, anwser);
            }

            PrintAnsweredQuestions();

            Console.WriteLine($"Your quizz now is finished! Thank you {user.Username} for participating!");

        }

        private static void PrintAnsweredQuestions()
        {
            Console.WriteLine("Printing your answers... (Unfinished)");
        }

        private static void PrintUsage()
        {
            var table = new Table()
            {
                Border = TableBorder.None,
                Expand = true,
            }.HideHeaders();
            table.AddColumn(new TableColumn("One"));

            var markup = new Markup(
               "Type [bold fuchsia]/start[/] to start your quizz.\n"
               + "Or type [bold fuchsia]/quit[/] to leave.");

            table.AddColumn(new TableColumn("Two"));

            var menuTable = new Table()
                .HideHeaders()
                .Border(TableBorder.None)
                .AddColumn(new TableColumn("Content"));

            menuTable
                .AddEmptyRow()
                .AddRow(markup);

            table.AddRow(menuTable);

            AnsiConsole.Render(table);
            AnsiConsole.WriteLine();
        }
    }
}
