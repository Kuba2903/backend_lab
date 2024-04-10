using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models.QuizAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/v1/admin/quizzes")]
    [ApiController]
    public class ApiQuizAdminController : ControllerBase
    {
        private IQuizAdminService _quizAdminService;

        public ApiQuizAdminController(IQuizAdminService quizAdminService)
        {
            _quizAdminService = quizAdminService;
        }

        [HttpPost]

        public ActionResult<Quiz> CreateQuiz(LinkGenerator generator,NewQuizDto dto)
        {
            var quiz = _quizAdminService.AddQuiz(new Quiz() { Title = dto.Title });

            return Created(
                generator.GetUriByAction(HttpContext, nameof(GetQuiz), null, new { quizId = quiz.Id }), quiz
            );
        }


        [HttpGet]
        [Route(template:"{quizId}")]

        public ActionResult<Quiz> GetQuiz(int quizId)
        {
            var quiz = _quizAdminService.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);

            return quiz == null ? NotFound() : quiz;
        }


        [HttpPatch]
        [Route("{quizId}")]

        public ActionResult<Quiz> UpdateQuiz(IValidator<QuizItem> validator,int quizId, JsonPatchDocument<Quiz>? patchDocument)
        {
            var quiz = _quizAdminService.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);

            if(quiz is null || patchDocument is null)
            {
                return NotFound();
            }

            var disabledOperation = patchDocument.Operations.
                FirstOrDefault(o => o.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Remove && o.path.Contains("/items"));

            if(disabledOperation is not null)
            {
                return BadRequest(error: "Cant remove from items!");
            }


            patchDocument.ApplyTo(quiz);
            QuizItem item = quiz.Items[^1];
            var result = validator.Validate(item);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            if(quiz.Id != quizId)
            {
                return BadRequest(error: "Cant change id");
            }

            if(ModelState.IsValid)
            {
                _quizAdminService.UpdateQuiz(quiz);
                return quiz;
            }

            return BadRequest(ModelState);
        }
    }
}
