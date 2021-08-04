using CSharp.Quizz.Common;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace CSharp.Quizz.Client
{
    public static class Program
    {
        private static QuestionRepository _questionRepository = new();
        private static UserRepository _userRepository = new();

        public static async Task Main(string[] args)
        {
            Console.Clear();

            PrintWelcome();
            await StartInterview();

            Console.WriteLine("Type enter to close ...");
            Console.ReadLine();
        }

        private static async Task StartInterview()
        {
            AnsiConsole.MarkupLine("Welcome to the [bold deepskyblue1].NET Question[/]! Made by [dim fuchsia]rcamine[/].");
            AnsiConsole.WriteLine();
            var username = AnsiConsole.Ask<string>("What's your [aqua]name[/]?");
            AnsiConsole.WriteLine();
            var email = AnsiConsole
                .Prompt(new TextPrompt<string>("[grey][[Optional]][/] Please type your [aqua]email[/]")
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

            var questionList = _questionRepository.GetRandomizedQuestions(Seniority.Senior, 10);
            Console.Clear();

            var count = 0;
            var percent = 0;

            foreach (var question in questionList)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[aqua]{count}[/] of [aqua]{questionList.Count}[/] questions answered [yellow]({percent}%)[/]");

                var questionTable = new Table();
                questionTable.AddColumn("Difficulty");
                questionTable.AddColumn(new TableColumn("Categories").Centered());
                questionTable.AddRow($"{question.Difficulty}", $"[green]{question.Category}[/]");
                AnsiConsole.Render(questionTable);
                AnsiConsole.MarkupLine($"[lightyellow3]{question.Description}[/]");

                var anwser = Console.ReadLine();
                _userRepository.SaveAnswer(user, question, anwser);
                count++;
                percent = count * questionList.Count;
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
                new FigletText(".NET Question")
                    .LeftAligned()
                    .Color(Color.LightCoral));

            AnsiConsole.WriteLine();
        }
    }
}
