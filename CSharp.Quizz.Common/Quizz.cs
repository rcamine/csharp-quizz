using System;

namespace CSharp.Quizz.Common
{
    public class Quizz
    {
        public Quizz(string description, Difficulty difficulty, Category category)
        {
            Id = Guid.NewGuid();
            Description = description;
            Difficulty = difficulty;
            Category = category;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Category Category { get; private set; }
    }
}
