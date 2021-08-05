using System;
using System.Collections.Generic;

namespace CSharp.Quizz.Common
{
    public class UserRepository
    {
        private static readonly IDictionary<User, List<(Guid, string)>> AnsweredQuestions 
            = new Dictionary<User, List<(Guid, string)>>();

        public void SaveAnswer(User user, Question quizz, string answer)
        {
            if (AnsweredQuestions.TryGetValue(user, out var answeredQuestions))
            {
                answeredQuestions.Add((quizz.Id, answer));
                return;
            }

            AnsweredQuestions.Add(user, new List<(Guid, string)>() { (quizz.Id, answer) });
        }
    }
}
