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
            Console.Clear();

            PrintWelcome();

            await StartQuizz();

            Console.WriteLine("Type enter to close ...");
            Console.ReadLine();
        }

        private static async Task StartQuizz()
        {
            AnsiConsole.MarkupLine("Welcome to the [bold deepskyblue1].NET Quizz[/]! Made by [dim fuchsia]rcamine[/].");
            AnsiConsole.WriteLine();
            string username = AnsiConsole.Ask<string>("What's your [aqua]name[/]?");
            AnsiConsole.WriteLine();
            var email = AnsiConsole
                .Prompt(new TextPrompt<string>("[grey][[Optional]][/] Please type your [aqua]email[/]?")
                .AllowEmpty());

            var user = new User(username, email);

            await AnsiConsole.Status().StartAsync("Preparing your quizz", async ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);
                ctx.Status = "[bold]Preparing your quizz...[/]";
                await Task.Delay(TimeSpan.FromSeconds(3));

                ctx.Status = "[bold green]Loading complete![/]";
                await Task.Delay(500);
            });

            var quizzList = _quizzRepository.RandomizeQuizz();
            Console.Clear();

            var count = 0;
            var percent = 0;

            foreach (var quizz in quizzList)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[yellow]{percent}% completed.[/]");

                var questionTable = new Table();
                questionTable.AddColumn("Difficulty");
                questionTable.AddColumn(new TableColumn("Categories").Centered());
                questionTable.AddRow($"{quizz.Difficulty}", $"[green]{quizz.Category}[/]");
                AnsiConsole.Render(questionTable);
                AnsiConsole.MarkupLine($"[lightyellow3]{quizz.Description}[/]");

                string anwser = Console.ReadLine();
                _userRepository.SaveAnswer(user, quizz, anwser);
                count++;
                percent = count * quizzList.Count;
            }

            Console.Clear();
            AnsiConsole.MarkupLine($"[bold green]{percent}% completed![/]");
            AnsiConsole.WriteLine();

            PrintAnsweredQuestions();

            Console.WriteLine($"Your quizz now is finished! Thank you {user.Username} for participating!");
        }

        private static void PrintAnsweredQuestions()
        {
            Console.WriteLine("Printing your answers... (Unfinished)");
        }

        private static void PrintWelcome()
        {
            AnsiConsole.Render(
                new FigletText(".NET Quizz")
                    .LeftAligned()
                    .Color(Color.LightCoral));

            AnsiConsole.WriteLine();
        }
    }
}
