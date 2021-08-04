using DataStore.Customization.Helpers;
using DataStore.Customization.Paths;
using Newtonsoft.Json.Linq;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Customization.Stores
{
    public class QuizStore
    {

        public async Task<QuizDTO> GetQuiz(string assignment_id, string student_id)
        {
            try
            {
                var uqb = new UriQueryBuilder()
                    .AddParam("assignment_id", assignment_id);

                if (!String.IsNullOrEmpty(student_id))
                    uqb.AddParam("student_id", student_id);

                var uri = UriPathBuilder.GetPath(ApiResources.Quiz, uqb).ToString();
                
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");

                var result = await RequestProvider.GetAsync<QuizDTO>(uri, token);
                
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<QuizDTO> SubmitQuiz(QuizDTO quiz)
        {
            try
            {
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");

                var uri = UriPathBuilder.GetPath(ApiResources.Quiz,
                    new UriQueryBuilder()
                    .AddParam("assignment_id", quiz.ASSIGNMENT_ID)
                    .AddParam("student_id", quiz.STUDENT_ID))
                    .ToString();

                var obj = JObject.FromObject(quiz).ToString();

                var formVariables = new List<KeyValuePair<string, string>>();
                formVariables.Add(new KeyValuePair<string, string>("Quiz", obj));

                return await RequestProvider.PostAsync<QuizDTO, QuizDTO>(uri, formVariables, token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class Question
    {
        public string ID { get; set; }
        public string QUESTION_ID { get; set; }
        public string QUIZ_ID { get; set; }
        public string TITLE { get; set; }
        public string POINTS { get; set; }
        public string TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public object FILE { get; set; }
        public object SORT_ORDER { get; set; }
        public List<string> OPTIONS { get; set; }

        public List<string> Answers = new List<string>();

    }

    public class Quiz
    {
        public string ID { get; set; }
        public string STAFF_ID { get; set; }
        public string TITLE { get; set; }
        public string OPTIONS { get; set; }
        public string ASSIGNED_DATE { get; set; }
        public object DUE_DATE { get; set; }
        public string POINTS { get; set; }
        public object ANSWERED { get; set; }
        public string DESCRIPTION { get; set; }
        public string COURSE_TITLE { get; set; }
        public string CREATED_AT { get; set; }
        public string CREATED_BY { get; set; }
        public object ASSIGNMENT_TYPE_COLOR { get; set; }
        public string ASSIGNMENT_TITLE { get; set; }
    }

    public class QuizDTO
    {
        public List<Question> QUESTIONS { get; set; }

        public string ASSIGNMENT_ID { get; set; }

        public Quiz QUIZ { get; set; }

        public string STUDENT_ID { get; set; }

        public bool SUCCESS { get; set; }

        public string MINUTES { get; set; }
        public string HOURS { get; set; }

    }

}
