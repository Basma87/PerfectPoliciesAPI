using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerfectPolicies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


/// <summary>
///  controller responsible for handling Quiz module functions.
/// </summary>
namespace PerfectPolicies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private static List<Quiz> quizesList;

        private readonly ApplicationContext _AppContext;

        /// <summary>
        /// use injected database connection
        /// </summary>
        /// <param name="context"> database context</param>
        public QuizController(ApplicationContext context)
        {
            if (_AppContext == null)
            {
                _AppContext = context;
            }
            //  quizesList = _AppContext.Quizes.Include(c => c.Questions).ThenInclude(c => c.Options).ToList();
        }
       
        /// <summary>
        /// Method that retrieves all Quizzes
        /// </summary>
        /// <returns> quizzes list</returns>
        [HttpGet]
        public ActionResult<List<Quiz>> GetAllQuizes()
        {
            quizesList = _AppContext.Quizes.Include(c => c.Questions).ThenInclude(c => c.Options).ToList();

            if (quizesList == null)
            {
                return NotFound(new { message = "No Quizes are found" });
            }

            return quizesList;
        }

        /// <summary>
        /// method tat retrieves quizzes by an organization name
        /// </summary>
        /// <param name="orgName"> organization name</param>
        /// <returns>filtered quizzes list by organization name</returns>
        [HttpGet]
        [Route("GetQuizzesByOrganizationName/{orgName}")]
        public ActionResult<List<Quiz>> GetQuizzesByOrganizationName(string orgName)
        {
            quizesList = _AppContext.Quizes.Where(c => c.CreatorName == orgName).ToList();

            if (quizesList == null)
            {
                return NotFound(new { message = "No Quizes are found"});
            }

            return quizesList;
        }

       /// <summary>
       /// method to retrieve data of certain quiz.
       /// </summary>
       /// <param name="id"> quiz ID</param>
       /// <returns> single quiz </returns>
        [HttpGet("{id}")]
        public ActionResult<Quiz> GetSingleQuiz(int id)
        {
            var quiz = _AppContext.Quizes.Find(id);

            if (quiz == null)//(!quizesList.Any(c=>c.QuizID==id))
            {
                return NotFound(new { message = "this quiz not exist" });
            }

            var QuizResult = _AppContext.Quizes.Where(c => c.QuizID == id).Include(c => c.Questions).FirstOrDefault();

            return Ok(QuizResult);
        }

      
        /// <summary>
        /// method to add (create) a new Quiz
        /// </summary>
        /// <param name="newQuiz"> new quiz object</param>
        /// <returns>ew created object</returns>
        [HttpPost]
        public ActionResult<Quiz> AddNewQuiz([FromBody] Quiz newQuiz)
        {
            
            // check if no quiz object is passed , or any of provided values are empty
            if (newQuiz ==null ||String.IsNullOrEmpty( newQuiz.QuizTitle)||String.IsNullOrEmpty(newQuiz.QuizTopic) 
                || String.IsNullOrEmpty(newQuiz.PassPercentage.ToString()) 
                ||String.IsNullOrEmpty(newQuiz.CreatorName)||String.IsNullOrEmpty(newQuiz.Created.ToString()))
            {
                return BadRequest(new { message="some fields are empty"});
            }

            else
            {
                _AppContext.Quizes.Add(newQuiz);
                _AppContext.SaveChanges();

                return Ok(newQuiz);
            }
        }

       /// <summary>
       /// method that updates existing quiz
       /// </summary>
       /// <param name="id"> id of existing quiz</param>
       /// <param name="newQuiz"> new Quiz object data that will replace old data</param>
       /// <returns> updated old quiz </returns>
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateQuiz(int id, [FromBody] Quiz newQuiz)
        {
            var OldQuiz= _AppContext.Quizes.Find(id);

            if (OldQuiz==null||newQuiz==null)
            {
                return BadRequest("Error while updating the Quiz");
            }

            else
            {
                OldQuiz.QuizID = id;
                OldQuiz.QuizTitle = newQuiz.QuizTitle;
                OldQuiz.QuizTopic = newQuiz.QuizTopic;
                OldQuiz.CreatorName = newQuiz.CreatorName;
                OldQuiz.Created = newQuiz.Created;
                OldQuiz.PassPercentage = newQuiz.PassPercentage;


                _AppContext.Quizes.Update(OldQuiz);
                _AppContext.SaveChanges();
                return Ok(OldQuiz);
            }
        }
        /// <summary>
        /// method to delete quiz
        /// </summary>
        /// <param name="id"> id of the quiz that will be deleted</param>
        /// <returns> success/ failure message whene deletion is done</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteQuiz(int id)
        {
            var Quiz = _AppContext.Quizes.Find(id);
            if (Quiz==null)
            {
                return BadRequest("Error while deleting the Quiz");
            }

            _AppContext.Quizes.Remove(Quiz);
            _AppContext.SaveChanges();
            return Ok(new { message = "Quiz is deleted" });

        }
    }
}
