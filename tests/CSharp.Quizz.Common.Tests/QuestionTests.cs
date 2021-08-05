using FluentAssertions;
using Xunit;

namespace CSharp.Quizz.Common.Tests
{
    public class QuestionTests
    {
        [Fact(DisplayName = "When a new question is initialized should add a new valid Guid")]
        public void When_NewQuestion_Should_AddValidNewGuid()
        {
            var question = new Question("Test Question", Difficulty.Easy, Category.ComputerScience);
            question.Id.Should().NotBeEmpty();
        }
    }
}
