﻿using ApplicationCore.Commons.Specification;
using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models.QuizAggregate;

namespace ApplicationCore.Interfaces.Criteria;

public class QuizItemByQuestion: BaseSpecification<QuizItem>
{
    public QuizItemByQuestion(string question): base(item => item.Question == question)
    {
        AddInclude(item => item.IncorrectAnswers);
    }
}