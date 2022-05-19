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
/// controller responsible for handling Question module functions.
/// </summary>
namespace PerfectPolicies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationContext _AppContext;

        private static List<Question> questionsList;


        /// <summary>
        /// Create instance of database context to create database, tables and seed tables with data.
        /// and initialize questions list that will be used thgout the controller.
        /// </summary>
        /// <param name="context"></param>
        public QuestionController(ApplicationContext context)
        {
            if (_AppContext ==null)
            {
                _AppContext = context;
            }
        }
        /// <summary>
        /// GET method that returns all questions list and their options
        /// </summary>
        /// <returns> Questions list</returns>
        // GET: api/<Question>
        [HttpGet]
        public ActionResult< List<Question>>GetAllQuestions()
        {
            questionsList = _AppContext.Questions.Include(c => c.Options).ToList();

            if (questionsList == null || questionsList.Count == 0)
            {
                return NotFound(new { Message="No data exists"});
            }
            else

            return Ok(questionsList);
        }

        /// <summary>
        /// GET method to retrieve Single question and its related options.
        /// </summary>
        /// <param name="id">quetsion id</param>
        /// <returns> single question</returns>
        // GET api/<Question>/5
        [HttpGet("{id}")]
        public ActionResult<Question> GetSingleQuestion(int id)
        {
            var question = _AppContext.Questions.Where(c => c.QuestionID==id).Include(c => c.Options).FirstOrDefault();

            if (question == null)
            {
                return NotFound( new { message="this Questions not exist"});
            }
            return Ok (question);
        }
        [HttpGet]
        [Route("getAllQuestionsOfQuiz/{id}")]
        public ActionResult<List<Question>> getAllQuestionsOfQuiz(int id)
        {
            var questionsList = _AppContext.Questions.Where(c => c.QuizID.Equals(id)).ToList();

            if (questionsList == null)
            {
                return BadRequest(new { message = "NO questions are found" });
            }

            else
                return questionsList;
        }

        /// <summary>
        /// POST method to create a new question.
        /// </summary>
        /// <param name="newQuestion"> newQuestion Object</param>
        /// <returns> returns newQuestion in case of success / or error message in case of failure</returns>
        // POST api/<Question>
        [HttpPost]
       // [Authorize]
        public ActionResult<Question> AddQuestion([FromBody] Question newQuestion)
        {
            if (newQuestion == null ||String.IsNullOrEmpty( newQuestion.QuestionTopic)
                || String.IsNullOrEmpty(newQuestion.QuestionText)
                ||String.IsNullOrEmpty( newQuestion.QuizID.ToString()))
            {
                return BadRequest(new { message="Question can't be added"});

            }
             
            _AppContext.Questions.Add(newQuestion);
            _AppContext.SaveChanges();
            return Ok (newQuestion);
        }

        /// <summary>
        /// PUT method to update existing question.
        /// </summary>
        /// <param name="id">id of old question</param>
        /// <param name="newQuestion"> newQuestion object</param>
        /// <returns>Success / failure message</returns>
        // PUT api/<Question>/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Question> UpdateQustion(int id, [FromBody] Question newQuestion)
        {
            // check if id not exist or object body is empty+

            var question = _AppContext.Questions.Find(id);
            if (question == null || newQuestion == null)
            {
                return BadRequest(new { message = "Error while updating Question" });
            }

            else

                question.QuestionID = newQuestion.QuestionID;
                question.QuestionTopic = newQuestion.QuestionTopic;
                question.QuestionText = newQuestion.QuestionText;
                question.ImagePath = newQuestion.ImagePath;
                question.QuizID = newQuestion.QuizID;


                _AppContext.Questions.Update(question);
                _AppContext.SaveChanges();

            return Ok(new {message= "Question is updated" });
        
        }

        /// <summary>
        /// DELETE method to delete a question
        /// </summary>
        /// <param name="id">id of question</param>
        /// <returns> success message when deleted / or failure message if problem happened</returns>
        // DELETE api/<Question>/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteQuestion(int id)
        {
            var question = _AppContext.Questions.Find(id);

            if (question==null)
            {
                return NotFound(new { message = "this question not exist" });
            }
            else

                _AppContext.Questions.Remove(question);
                _AppContext.SaveChanges();

            return Ok(new { message="Question is deleted. "});

        }
    }
}
