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
            AnsiConsole.MarkupLine("Welcome to the .NET Quizz! Made by [red]rcamine[/]");
            AnsiConsole.WriteLine();
            string username = AnsiConsole.Ask<string>("What's your [green]name[/]?");
            AnsiConsole.WriteLine();
            var email = AnsiConsole
                .Prompt(new TextPrompt<string>("[grey][[Optional]][/] Please type your [green]email[/]?")
                .AllowEmpty());

            var user = new User(username, email);

            await AnsiConsole.Status().StartAsync($"Preparing your quizz...",
                async ctx =>
                {
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.Status = "Preparing your quizz...";
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    ctx.Status = "Loading complete! Initiating your quizz...";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                });

            var quizzList = _quizzRepository.RandomizeQuizz();
            Console.Clear();

            foreach (var quizz in quizzList)
            {
                Console.Clear();
                string anwser = AnsiConsole.Ask<string>(quizz.ToString());
                _userRepository.SaveAnswer(user, quizz, anwser);
            }

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
                    .Color(Color.Red));

            AnsiConsole.WriteLine();
        }
    }
}
