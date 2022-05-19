using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerfectPolicies.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

/// <summary>
/// controller responsible for handling Options module functions.
/// </summary>
namespace PerfectPolicies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {

        private readonly ApplicationContext _AppContext;
        private static List<Option> OptionsList;

        /// <summary>
        /// controller that create database connection by injecting database context.
        /// </summary>
        /// <param name="context"></param>
        public OptionsController(ApplicationContext context)
        {
            if (_AppContext==null)
            {
                _AppContext = context;
            }
          
         
          
        }

       /// <summary>
       /// method that return all options.
       /// </summary>
       /// <returns> list of options </returns>
        [HttpGet]
        public ActionResult< List<Option> >GetAllOptions()
        {
            OptionsList = _AppContext.Options.ToList();

            if (OptionsList ==null)
            {
                return NotFound(new { message="No Options exist"});
            }


            return OptionsList;
        }
        /// <summary>
        /// method that return data of single option
        /// </summary>
        /// <param name="id">option ID</param>
        /// <returns> single option object</returns>
        [HttpGet("{id}")]
        public ActionResult <Option> GetSingleOption(int id)
        {
            var option = _AppContext.Options.Find(id);
          
            if (option==null)
            {
                return NotFound(new { message="this option not exist"});
            }

    
            return Ok (option);
        }

        /// <summary>
        /// method that retrieves options related to a question
        /// </summary>
        /// <param name="id"> question ID</param>
        /// <returns> list of optios</returns>
        [HttpGet]
        [Route("QuestionOptions/{id}")]
        public ActionResult getAllOptionsOfQuestion(int id)
        {
            var optionsList = _AppContext.Options.Where(c => c.QuestionID == id).ToList();

            if (optionsList == null)
            {
                return NotFound(new { message = "No Options are found" });
            }

            else
                return Ok(optionsList);
        }

        /// <summary>
        /// method to create a new option
        /// </summary>
        /// <param name="newOption">new option object</param>
        /// <returns> bad request if failed or success if option is created</returns>
        [HttpPost]
        public ActionResult<Option> CreateOption([FromBody] Option newOption)
        {
            if (newOption == null || String.IsNullOrEmpty(newOption.OptionText) || String.IsNullOrEmpty(newOption.OptionNumber.ToString())
               || String.IsNullOrEmpty(newOption.CorrectAnswer.ToString())
               || String.IsNullOrEmpty(newOption.QuestionID.ToString()) )
            {
                return BadRequest(new { message = "some fields are empty" });
            }

            else
            {  
                    _AppContext.Options.Add(newOption);
                    _AppContext.SaveChanges();

                    return Ok(newOption);
             }
            
        }

        /// <summary>
        /// method to update option
        /// </summary>
        /// <param name="id">option ID to be updated</param>
        /// <param name="newOption"> new option object</param>
        /// <returns>updated option</returns>
        [HttpPut("{id}")]
        public ActionResult updateOption(int id, [FromBody] Option newOption)
        {
            var option = _AppContext.Options.Find(id);

            if (option==null || newOption == null)
            {
                return BadRequest("Error while updating the Quiz");
            }

            else
            {
                option.OptionID = newOption.OptionID;
                option.OptionText = newOption.OptionText;
                option.OptionNumber = newOption.OptionNumber;
                option.CorrectAnswer = newOption.CorrectAnswer;
                option.QuestionID = newOption.QuestionID;

                _AppContext.Options.Update(option);
                _AppContext.SaveChanges();
                return Ok(newOption);
            }
        }

        /// <summary>
        /// method to delete option
        /// </summary>
        /// <param name="id">option ID</param>
        /// <returns>Success / failure status</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteOption(int id)
        {
            var option = _AppContext.Options.Find(id);

            if (option==null)
            {
                return BadRequest("this Options deosn't exist");
            }

            _AppContext.Options.Remove(option);
            _AppContext.SaveChanges();
            return Ok(new { message="Option is deleted"});

        }
    }
}
