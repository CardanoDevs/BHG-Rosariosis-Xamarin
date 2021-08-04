using DataStore.Customization.Helpers;
using DataStore.Customization.Paths;
using DataStore.Customization.Requests.ClassRoom;
using DataStore.Customization.Responses.ClassRoom;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///using Xamarin.Essentials;

namespace DataStore.Customization.Stores
{
    public class ClassStore
    {

        public async Task<ClassRoomResponse> GetClassData()
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.CoursePeriod).ToString();// "https://stratusarchives.com/assignment/api/CoursePeriods.php";
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                return await RequestProvider.GetAsync<ClassRoomResponse>(uri, token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AssignmentResponse> GetAssignmentData(string id, string student_id = "")
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.Course,
                    new UriQueryBuilder()
                    .AddParam("course_period_id", id)
                    .AddParam("student_id", student_id))
                    .ToString();
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                return await RequestProvider.GetAsync<AssignmentResponse>(uri, token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AssignmentDetailResponse> GetAssignmentDetailData(string id)
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.Assignment, new UriQueryBuilder().AddParam("assignment_id", id)).ToString();// "https://www.stratusarchives.com/assignment/api/Assignments.php?assignment_id=" +id;
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                return await RequestProvider.GetAsync<AssignmentDetailResponse>(uri, token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AssignmentDetailResponse> PostAssignmentDetailData(AssignmentDetailRequest item, string assignment_id, string student_id = "")
        {
            try
            {

                var uri = UriPathBuilder.GetPath(ApiResources.Assignment, new UriQueryBuilder()
                    .AddParam("assignment_id", assignment_id)
                    .AddParam("student_id", student_id))
                    .ToString();

                var formVariables = new List<KeyValuePair<string, string>>();
                formVariables.Add(new KeyValuePair<string, string>("message", item.Message));
                formVariables.Add(new KeyValuePair<string, string>("submit_assignment", "1"));
                
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                
                return await RequestProvider.MediaPostAsync<AssignmentDetailRequest, AssignmentDetailResponse>(uri, formVariables, item.Files, token);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Annoucement> GetAnnoucement()
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.Annoucements).ToString();// "https://www.stratusarchives.com/assignment/api/Announcements.php";
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                var result = await RequestProvider.GetAsync<Annoucement>(uri, token);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GradesResponse> GetGradesData(string course_period_id, string student_id = "")
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.Grades, new UriQueryBuilder()
                    .AddParam("course_period_id", course_period_id)
                    .AddParam("student_id", student_id))
                    .ToString();
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                return await RequestProvider.GetAsync<GradesResponse>(uri, token);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> SendToken()
        {
            try
            {
                if (CrossSecureStorage.Current.HasKey("FCMToken"))
                {
                    RequestProvider.PostToken();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
