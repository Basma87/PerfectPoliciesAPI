using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectPolicies.DTO;
using PerfectPolicies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
///  controller responsible for handling report module functions.
/// </summary>
namespace PerfectPolicies.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// use injected database connection
        /// </summary>
        /// <param name="context">database context</param>
        public ReportController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// method that retrieve number of questions per each quiz 
        /// </summary>
        /// <returns>list of  quizzes and questions counter for each</returns>
        [HttpGet("DataReport")]
        public IActionResult GetQuestionsPerQuiz()
        {
            var quizzesList = _context.Quizes.Include(c => c.Questions).ToList();

            List<QuestionsPerQuizViewModel> reportData = quizzesList.Select(c => new QuestionsPerQuizViewModel
            {
                quizName = c.QuizTitle,
                questionsCount = c.Questions.Count
            }).ToList();


            return Ok(reportData);
        }
    }
}
