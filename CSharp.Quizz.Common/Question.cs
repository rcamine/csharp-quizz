using System;

namespace CSharp.Quizz.Common
{
    public record Question(string Description, Difficulty Difficulty, Category Category)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
