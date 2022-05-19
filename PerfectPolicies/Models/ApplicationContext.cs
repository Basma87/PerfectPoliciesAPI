using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicies.Models
{
    /// <summary>
    /// Class that represent database tables that will be created.
    /// </summary>
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options) :base(options)
            { 

            }

        public DbSet <Quiz> Quizes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<UserInfo> Users { get; set; }


        /// <summary>
        /// method that insert data into table once database and tab;e are created.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Quiz>().HasData(new Quiz { 

            QuizID=1,
            QuizTitle="Quiz one",
            QuizTopic="Quiz Topic one",
            CreatorName="Creator 1",
            Created=DateTime.Now,
            PassPercentage=50
            },

            new Quiz{
                QuizID = 2,
                QuizTitle = "Quiz Two",
                QuizTopic = "Quiz Topic Two",
                CreatorName = "Creator 1",
                Created = DateTime.Now,
                PassPercentage = 50
            },

            new Quiz
            {
                QuizID = 3,
                QuizTitle = "Quiz three",
                QuizTopic = "Quiz Topic Three",
                CreatorName = "Creator 2",
                Created = DateTime.Now,
                PassPercentage = 50
            }
            );


            builder.Entity<Question>().HasData(
                new Question
            {
                QuestionID = 1,
                QuestionTopic = "Qestion Topic one",
                QuestionText = "what is australia",
                QuizID=1


            },
          new Question  {
                QuestionID = 2,
                QuestionTopic = "Qestion Topic Two",
                QuestionText = "Where  is australia",
                QuizID = 1


            },
           new Question {
                QuestionID = 3,
                QuestionTopic = "Qestion Topic Three",
                QuestionText = "where is egypt",
                QuizID = 1


            },


           new Question
           {
               QuestionID = 4,
               QuestionTopic = "Qestion Topic one",
               QuestionText = "what is australia",
               QuizID = 2


           },
          new Question
          {
              QuestionID = 5,
              QuestionTopic = "Qestion Topic Two",
              QuestionText = "Where  is australia",
              QuizID = 1


          },
           new Question
           {
               QuestionID = 6,
               QuestionTopic = "Qestion Topic Three",
               QuestionText = "where is egypt",
               QuizID = 2


           }
            );


            builder.Entity<Option>().HasData(new Option
            {
                OptionID = 1,
                OptionText = "option A",
                OptionNumber =1,
                CorrectAnswer = true,
                QuestionID=1


            },

            new Option
            {
                OptionID = 2,
                OptionText = "option B",
                OptionNumber = 2,
                CorrectAnswer = true,
                QuestionID = 1


            },

            new Option
            {
                OptionID = 3,
                OptionText = "option c",
                OptionNumber = 3,
                CorrectAnswer = false,
                QuestionID = 1


            },

            new Option
            {
                OptionID = 4,
                OptionText = "option D",
                OptionNumber = 4,
                CorrectAnswer = false,
                QuestionID = 1
            }

            );

            builder.Entity<UserInfo>().HasData(
                new UserInfo
            {
                UserInfoID = 1,
                UserName = "AdminPerfectPolicies",
                Password = "PerfectPolicies!22"
            }
            );
        }


    }

}
