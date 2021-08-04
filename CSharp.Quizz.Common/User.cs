using System;

namespace CSharp.Quizz.Common
{
    public record User(string Username, string Email)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}