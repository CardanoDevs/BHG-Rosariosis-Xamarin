using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Assignments
{
    class QuizViewModel : BasePageViewModel
    {

        bool StartTimer = true;

        private USER _userData;
        public USER UserData
        {
            get { return _userData; }
            set { SetField(ref _userData, value); }
        }

        private STUDENT _selectedChild;
        public STUDENT SelectedChild
        {
            get { return _selectedChild; }
            set { _selectedChild = value; }
        }

        private bool _isParent;
        public bool IsParent
        {
            get { return _isParent; }
            set { SetField(ref _isParent, value); }
        }

        public Assignment Assignment;

        public bool IsFirst { get; set; }

        public Quiz page;

        public QuizDTO quizDTO { get; set; }

        public string QuizTime
        {
            get
            {

                try
                {
                    string student_id = IsParent ? SelectedChild.STUDENT_ID : UserData.USER_ID;

                    string quiz_id = quizDTO.QUIZ.ID;

                    string timer_id = "QUIZ_TIMER_" + quiz_id + "_" + student_id;

                    if (CrossSecureStorage.Current.HasKey(timer_id))
                    {
                        var time = Convert.ToDouble(CrossSecureStorage.Current.GetValue(timer_id));
                        var span = TimeSpan.FromSeconds(time);

                        return span.Hours + ":" + span.Minutes + ":" + span.Seconds;

                    }

                    return "";
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }

        public bool ShowQuizTime
        {
            get
            {
                try
                {
                    return !String.IsNullOrEmpty(QuizTime);
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(ExecuteBackCommand));

        private async void ExecuteBackCommand()
        {
            await this.PopPageAsync<QuizViewModel>();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();


            if (IsFirst)
            {
                IsFirst = false;

                if (quizDTO?.QUESTIONS?.Count > 0)
                {
                    page.InitQuiz(quizDTO.QUESTIONS);
                }

            }

            StartTimer = true;
            StartQuizTimer();
            OnPropertyChanged(nameof(ShowQuizTime));

        }

        public override void OnDisappearing()
        {
            StartTimer = false;
        }

        private void StartQuizTimer()
        {

            try
            {
                string student_id = IsParent ? SelectedChild.STUDENT_ID : UserData.USER_ID;

                string quiz_id = quizDTO.QUIZ.ID;

                string timer_id = "QUIZ_TIMER_" + quiz_id + "_" + student_id;

                //CrossSecureStorage.Current.DeleteKey(timer_id);

                if (!CrossSecureStorage.Current.HasKey(timer_id))
                {

                    var hours = string.IsNullOrEmpty(quizDTO.HOURS) ? 0 : Convert.ToInt32(quizDTO.HOURS);
                    var minutes = string.IsNullOrEmpty(quizDTO.MINUTES) ? 0 : Convert.ToInt32(quizDTO.MINUTES);

                    int newTime = (hours * 60 * 60) + (minutes * 60);

                    if(newTime > 0)
                    {
                        CrossSecureStorage.Current.SetValue(timer_id, newTime.ToString());
                    }
                    else
                    {
                        return;
                    }
                }

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {

                    if (StartTimer)
                    {

                        var previousTime = Convert.ToInt32(CrossSecureStorage.Current.GetValue(timer_id, "0"));

                        var newTime = previousTime - 1;

                        CrossSecureStorage.Current.SetValue(timer_id, newTime.ToString());
                        OnPropertyChanged(nameof(QuizTime));
                        OnPropertyChanged(nameof(ShowQuizTime));

                        if (newTime <= 0)
                        {
                            // submit the quiz
                            page.SubmitQuiz();
                            return false;
                        }

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                });

            }
            catch (Exception ex)
            {

            }

        }

        //string GetQuizTimeFromStorage()
        //{
        //    string student_id = IsParent ? SelectedChild.STUDENT_ID : UserData.USER_ID;

        //    string quiz_id = quizDTO.QUIZ.ID;

        //    string timer_id = "QUIZ_TIMER_" + quiz_id + "_" + student_id;

        //    if (CrossSecureStorage.Current.HasKey(timer_id))
        //        return CrossSecureStorage.Current.GetValue(timer_id);

        //    return null;

        //}

        public async void SubmitQuiz(List<Question> questions)
        {

            var request = new QuizDTO()
            {
                QUIZ = new DataStore.Customization.Stores.Quiz
                {
                    ID = quizDTO.QUIZ.ID,
                },
                ASSIGNMENT_ID = quizDTO.ASSIGNMENT_ID,
                STUDENT_ID = (IsParent) ? SelectedChild.STUDENT_ID : "0",
                QUESTIONS = questions,
            };

            var result = await PerformAPICall(async () => await new QuizStore().SubmitQuiz(request));

            if (result != null && result.SUCCESS)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Quiz submitted.", "OK");
                await this.PopPageAsync();
            }
        }

    }

}
