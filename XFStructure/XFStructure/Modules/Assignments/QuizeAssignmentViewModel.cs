using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.Assignments;
using XFStructure.Modules.Quiz;
using XFStructure.ViewModels;

namespace XFStructure.Modules.ClassRoom
{
    public class QuizeAssignmentViewModel : BasePageViewModel
    {
        #region Properties

        private COURS _courseData;
        public COURS CourseData
        {
            get { return _courseData; }
            set { SetField(ref _courseData, value); }
        }

        private List<Assignment> _assignmentList;
        public List<Assignment> AssignmentList
        {
            get { return _assignmentList; }
            set { SetField(ref _assignmentList, value); }
        }

        private Assignment _assignmentData;
        public Assignment AssignmentDetail
        {
            get { return _assignmentData; }
            set { SetField(ref _assignmentData, value); }
        }

        //private List<Grade> _gradesList;
        //public List<Grade> GradesList
        //{
        //    get { return _gradesList; }
        //    set { SetField(ref _gradesList, value); }
        //}


        private USER _userData;
        public USER UserData
        {
            get { return _userData; }
            set { SetField(ref _userData, value); }
        }

        private bool _isFirst;
        public bool IsFirst
        {
            get { return _isFirst; }
            set { _isFirst = value; }
        }

        private bool _isListEmpty;
        public bool IsListEmpty
        {
            get { return _isListEmpty; }
            set { SetField(ref _isListEmpty, value); }
        }

        private bool _isAssignment;

        public bool IsAssignment
        {
            get { return _isAssignment; }
            set { SetField(ref _isAssignment, value); }
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

        private GradesResponse _gradeData;
        public GradesResponse GradesData
        {
            get { return _gradeData; }
            set { SetField(ref _gradeData, value); }
        }

        private bool _isGradeEmpty;
        public bool IsGradeEmpty
        {
            get { return _isGradeEmpty; }
            set { SetField(ref _isGradeEmpty, value); }
        }


        #endregion

        #region Command

        private ICommand _navigateToClassRoom;
        public ICommand NavigateToClassRoom => _navigateToClassRoom ?? (_navigateToClassRoom = new Command(ExecuteNavigateToClassRoom));

        private ICommand _navigateToAssignmentDetail;
        public ICommand NavigateToAssignmentDetail => _navigateToAssignmentDetail ?? (_navigateToAssignmentDetail = new Command(ExecuteNavigateToAssignmentDetail));               

        private ICommand _navigateToQuizeOrGrade;
        public ICommand NavigateToQuizeOrGrade => _navigateToQuizeOrGrade ?? (_navigateToQuizeOrGrade = new Command(ExecuteQuizeOrGrade));

        #endregion

        #region Methods

        public QuizeAssignmentViewModel()
        {
            IsFirst = true;
            IsListEmpty = false;
            IsAssignment = true;
            AssignmentList = new List<Assignment>();
        }

        public override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                if (IsFirst)
                {
                    IsFirst = false;
                    AssignmentList = new List<Assignment>();
                    await LoadAssignments();
                    //await LoadGrades();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LoadAssignments()
        {
            try
            {

                var student_id = IsParent ? SelectedChild.STUDENT_ID : UserData.USER_ID;

                var result = await PerformAPICall(async () => await new ClassStore().GetAssignmentData(CourseData.COURSE_PERIOD_ID, student_id));

                if (result != null && result.Assignments != null)
                {
                    foreach (var item in result.Assignments)
                    {
                        if (item.IS_CHECKED)
                        {
                            item.bgColor = Color.FromHex("#16b820");
                            item.MyAwesomeFC = Color.FromHex("#ffffff");
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(item.DUE_DATE))
                            {
                                item.bgColor = Color.FromHex("#f2f2f2");
                                item.MyAwesomeFC = Color.FromHex("#000000");
                            }
                            else
                            {
                                item.bgColor = Color.FromHex("#ff004d");
                                item.MyAwesomeFC = Color.FromHex("#ffffff");
                            }
                        }
                    }

                    AssignmentList = result.Assignments
                        .OrderBy(x => x.IS_CHECKED)
                        .ThenByDescending(x => x.DUE_DATE)
                        .ToList();

                    if (AssignmentList.Count() > 0)
                    {
                        IsListEmpty = false;
                    }
                    else
                    {
                        IsListEmpty = true;
                    }
                }
                else
                {
                    IsListEmpty = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        //private async Task LoadGrades()
        //{
        //    try
        //    {
        //        var result = await PerformAPICall(async () => await new ClassStore().GetGradesData());
        //        if (result != null)
        //        {
        //            GradesList = new List<Grade>();
        //            GradesList = result.Grades;
        //            if (GradesList.Count()>0)
        //            {
        //                IsGradeEmpty = false;
        //            }
        //            else
        //            {
        //                IsGradeEmpty = true;
        //            }                    
        //        }
        //        else
        //        {
        //            IsGradeEmpty = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        private async void ExecuteNavigateToClassRoom()
        {
            await this.PopPageAsync<QuizeAssignmentViewModel>();
        }

        private async void ExecuteNavigateToAssignmentDetail(object obj)
        {
            try
            {
                AssignmentDetail = obj as Assignment;
                //var result = await PerformAPICall(async () => await new ClassStore().GetAssignmentDetailData(AssignmentDetail.ASSIGNMENT_ID));

                if (AssignmentDetail.IS_QUIZ)
                {
                    bool v = await this.PushPageFromCacheAsync<QuizDetailsViewModel>((vm) =>
                    {
                        vm.Quiz = null;
                        vm.Assignment = AssignmentDetail;
                        vm.CourseData = CourseData;
                        vm.UserData = UserData;
                        vm.IsFirst = true;
                        vm.IsParent = IsParent;
                        vm.SelectedChild = SelectedChild;
                    });
                }
                else
                {
                    await this.PushPageFromCacheAsync<AssignmentDetailViewModel>((vm) =>
                    {
                        vm.CourseData = CourseData;
                        vm.UserData = UserData;
                        vm.AssignmentDetail = AssignmentDetail;
                        vm.IsFirst = true;
                        vm.IsParent = IsParent;
                        vm.SelectedChild = SelectedChild;
                        vm.AssignmentFileList = new ObservableCollection<AssginmentFile>();
                        vm.AssignmentsDetailResponse = null;
                        vm.IsVisibleFile = false;
                        vm.Message = string.Empty;
                        vm.ShowPreviousFileSection = false;
                    });
                }
                
            }
            catch (Exception ex)
            {

            }                       
        }

        private void ExecuteQuizeOrGrade(object obj)
        {
            var val = obj as string;
            if (val == "assignments")
            {
                IsAssignment = true;
            }           
        }

        #endregion

    }
}
