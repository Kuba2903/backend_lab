using ApplicationCore.Models.QuizAggregate;
using FluentValidation;

namespace WebApi.Validators
{
    public class QuizItemValidator : AbstractValidator<QuizItem>
    {
        public QuizItemValidator()
        {
            RuleFor(q => q.Question)
                .MaximumLength(200).WithMessage("Pytanie za długie")
                .MinimumLength(5).WithMessage("Pytanie za krótkie")
                .Must(q => q.EndsWith("?")).WithMessage("brak pytajnika na końcu");

            RuleFor(q => new { q.CorrectAnswer, q.IncorrectAnswers })
                .Must(p => !p.IncorrectAnswers.Contains(p.CorrectAnswer)).WithMessage("Poprawna odpowiedź nie może występować w liście niepoprawnych");


            RuleForEach(q => q.IncorrectAnswers)
                .MinimumLength(2)
                .MaximumLength(100);
        }
    }
}
