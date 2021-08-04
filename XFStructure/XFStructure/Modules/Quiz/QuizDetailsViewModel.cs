using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.Assignments;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Quiz
{
    class QuizDetailsViewModel : BasePageViewModel
    {
        public Assignment Assignment
        {
            get
            {
                return this.GetField<Assignment>();
            }
            set
            {
                SetField(value);
            }
        }

        public QuizDTO Quiz
        {
            get
            {
                return this.GetField<QuizDTO>();
            }
            set
            {
                SetField(value);
            }
        }

        private bool _isFirst;
        public bool IsFirst
        {
            get { return _isFirst; }
            set { SetField(ref _isFirst, value); }
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

        private COURS _courseData;
        public COURS CourseData
        {
            get { return _courseData; }
            set { SetField(ref _courseData, value); }
        }

        private USER _userData;
        public USER UserData
        {
            get { return _userData; }
            set { SetField(ref _userData, value); }
        }

        public bool IsAnswered
        {
            get
            {
                return Quiz?.QUIZ?.ANSWERED?.ToString() == "1";
            }
        }

        public int QuestionsCount
        {
            get
            {
                return this.Quiz?.QUESTIONS?.Count ?? 0;
            }
        }

        private ICommand _startQuizCommand;
        public ICommand StartQuiz => _startQuizCommand ?? (_startQuizCommand = new Command(ExecuteStartQuiz));

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(ExecuteBackCommand));

        private async void ExecuteBackCommand()
        {
            await this.PopPageAsync<QuizDetailsViewModel>();
        }

        private async void ExecuteStartQuiz(object obj)
        {
            await this.PushPageFromCacheAsync<QuizViewModel>((vm) =>
            {
                vm.Assignment = Assignment;
                vm.quizDTO = Quiz;
                vm.UserData = UserData;
                vm.IsParent = IsParent;
                vm.SelectedChild = SelectedChild;
                vm.IsFirst = true;
            });

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            var student_id = IsParent ? SelectedChild.STUDENT_ID : UserData.USER_ID;

            var result = await PerformAPICall(async () => await new QuizStore()
            .GetQuiz(this.Assignment.ASSIGNMENT_ID, student_id));

            if (result != null)
            {
                Quiz = result;
                OnPropertyChanged("IsAnswered");
                OnPropertyChanged("QuestionsCount");
            }

        }


    }
}
