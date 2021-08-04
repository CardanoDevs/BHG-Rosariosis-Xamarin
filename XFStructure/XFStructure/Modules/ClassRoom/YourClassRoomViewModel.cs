using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.Login;
using XFStructure.ViewModels;
using static DataStore.Customization.Responses.ClassRoom.Annoucement;

namespace XFStructure.Modules.ClassRoom
{
    public class YourClassRoomViewModel : BasePageViewModel
    {
        #region Properties

        private bool _isClassRoom;
        public bool IsClassRoom
        {
            get { return _isClassRoom; }
            set
            {
                SetField(ref _isClassRoom, value);
                if (value)
                {
                    IsAnnoucement = false;
                    IsGrades = false;
                }
            }
        }

        private bool _isAnnoucement;
        public bool IsAnnoucement
        {
            get { return _isAnnoucement; }
            set
            {
                SetField(ref _isAnnoucement, value);
                if (value)
                {
                    IsClassRoom = false;
                    IsGrades = false;
                }
            }
        }

        private bool _isGrades;
        public bool IsGrades
        {
            get { return _isGrades; }
            set
            {
                SetField(ref _isGrades, value);
                if (value)
                {
                    IsAnnoucement = false;
                    IsClassRoom = false;
                }
            }
        }

        private List<COURS> _courselist;
        public List<COURS> CourseList
        {
            get { return _courselist; }
            set { SetField(ref _courselist, value); }
        }

        private List<COURS> _filteredCourselist = new List<COURS>();
        public List<COURS> FilteredCourseListToShow
        {
            get { return _filteredCourselist; }
            set { SetField(ref _filteredCourselist, value); }
        }

        private List<AnnouncementDetail> _annoucementList;
        public List<AnnouncementDetail> AnnoucementList
        {
            get { return _annoucementList; }
            set { SetField(ref _annoucementList, value); }
        }

        private USER _userData;
        public USER UserData
        {
            get { return _userData; }
            set { SetField(ref _userData, value); }
        }

        private List<STUDENT> _studentList;
        public List<STUDENT> StudentList
        {
            get { return _studentList; }
            set { SetField(ref _studentList, value); }
        }

        private STUDENT _selectedChild;
        public STUDENT SelectedChild
        {
            get { return _selectedChild; }
            set { _selectedChild = value; }
        }

        //private List<Grade> _gradesList;
        //public List<Grade> GradesList
        //{
        //    get { return _gradesList; }
        //    set { SetField(ref _gradesList, value); }
        //}

        private bool _isGradeEmpty;
        public bool IsGradeEmpty
        {
            get { return _isGradeEmpty; }
            set { SetField(ref _isGradeEmpty, value); }
        }

        private bool _isParent;
        public bool IsParent
        {
            get { return _isParent; }
            set { SetField(ref _isParent, value); }
        }

        private string _childName;
        public string ChildName
        {
            get { return _childName; }
            set { SetField(ref _childName, value); }
        }

        private bool _showChild;
        public bool ShowChild
        {
            get { return _showChild; }
            set { SetField(ref _showChild, value); }
        }

        private int _listHeight;
        public int ListHeight
        {
            get { return _listHeight; }
            set { SetField(ref _listHeight, value); }
        }

        public bool IsFirst { get; set; }

        #endregion

        #region Command

        private ICommand _navigateToQuizesAssignment;
        public ICommand NavigateToQuizesAssignment => _navigateToQuizesAssignment ?? (_navigateToQuizesAssignment = new Command(ExecuteNavigateToQuizesAssignments));

        private ICommand _navigateToClassandAnnoucement;
        public ICommand NavigateToClassandAnnoucement => _navigateToClassandAnnoucement ?? (_navigateToClassandAnnoucement = new Command(ExecuteToClassandAnnoucement));

        private ICommand _selectStudent;
        public ICommand NavigateSelectStudent => _selectStudent ?? (_selectStudent = new Command(ExecuteSelectedStudent));

        private ICommand _showStudentList;
        public ICommand NavigateShowStudent => _showStudentList ?? (_showStudentList = new Command(ExecuteShowChildren));

        private ICommand _navigateToGradeDetail;
        public ICommand NavigateToGradeDetail => _navigateToGradeDetail ?? (_navigateToGradeDetail = new Command(ExecuteNavigateToGradeDetail));

        #endregion

        #region Method

        public YourClassRoomViewModel()
        {
            try
            {
                IsClassRoom = true;
                IsFirst = true;
            }
            catch (Exception)
            {

            }
        }

        public override async void OnAppearing()
        {
            try
            {
                if (IsFirst)
                {

                    IsFirst = false;

                    await LoadClasses();
                    await LoadAnnouncements();

                    TokenFCM();

                    if (IsParent)
                    {
                        SelectedChild = UserData.STUDENTS.FirstOrDefault();
                        ChildName = SelectedChild.FIRST_NAME + " " + SelectedChild.LAST_NAME;
                        StudentList = UserData.STUDENTS;
                        //SetStudentListHeight();
                        ExecuteSelectedStudent(SelectedChild);
                    }
                    else
                    {
                        FilteredCourseListToShow = CourseList;
                    }

                    //await LoadGrades();

                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {

            }
        }

        private void SetStudentListHeight()
        {
            try
            {
                if (StudentList.Count > 0)
                {
                    ListHeight = (StudentList.Count * 25) + 25;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LoadClasses()
        {
            ClassRoomResponse result = await PerformAPICall(async () => await new ClassStore().GetClassData());
            if (result != null)
            {
                CourseList = result.COURSES;
            }
        }

        private async void ExecuteNavigateToQuizesAssignments(object obj)
        {
            var courseData = obj as COURS;
            await this.PushPageFromCacheAsync<QuizeAssignmentViewModel>((vm) =>
            {
                vm.CourseData = courseData;
                vm.UserData = UserData;
                vm.IsFirst = true;
                vm.IsListEmpty = false;
                vm.IsParent = IsParent;
                vm.SelectedChild = SelectedChild;
            });
        }

        private async Task TokenFCM()
        {
            try
            {
                var result = await PerformAPICall(async () => await new ClassStore().SendToken());
            }
            catch (Exception)
            {

            }
        }

        private async Task LoadAnnouncements()
        {
            try
            {
                var result = await PerformAPICall(async () => await new ClassStore().GetAnnoucement());
                if (result != null && result.Announcements != null)
                {
                    AnnoucementList = new List<AnnouncementDetail>();
                    foreach (var item in result.Announcements)
                    {
                        item.BgColor = Color.FromHex("#1e91fb");
                        //if (item.Type == "General")
                        //{
                        //    item.BgColor = Color.FromHex("#138CCE");
                        //}
                        //else if (item.Type == "Grade")
                        //{
                        //    item.BgColor = Color.FromHex("#13CE66");
                        //}
                    }
                    AnnoucementList = result.Announcements;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void ExecuteToClassandAnnoucement(object obj)
        {

            IsLoading = true;

            await Task.Delay(1000);

            IsLoading = false;

            var val = obj as string;
            if (val == "classroom")
            {
                IsClassRoom = true;
            }
            else if (val == "annoucement")
            {
                IsAnnoucement = true;
            }
            else if (val == "grades")
            {
                IsGrades = true;
            }
            else if (val == "logout")
            {
                CrossSecureStorage.Current.DeleteKey("RememberMe");
                CrossSecureStorage.Current.DeleteKey("UserData");
                CrossSecureStorage.Current.DeleteKey("clientToken");
                await Navigation.RemoveCurrentAndNavigate<YourClassRoomViewModel, SchoolViewModel>();
            }

        }

        private void ExecuteSelectedStudent(object obj)
        {
            try
            {

                SelectedChild = obj as STUDENT;
                ChildName = SelectedChild.FIRST_NAME + " " + SelectedChild.LAST_NAME;

                var courses = CourseList.Where(x => x.STUDENT_ID == SelectedChild.STUDENT_ID).ToList();

                FilteredCourseListToShow = courses;

            }
            catch (Exception ex)
            {

            }
        }

        private void ExecuteShowChildren()
        {
            ShowChild = true;
            SetStudentListHeight();
        }

        private async void ExecuteNavigateToGradeDetail(object obj)
        {
            try
            {
                var val = obj as COURS;
                await Navigation.PushPageAsync<YourClassRoomViewModel, GradeViewModel>(vm =>
                {
                    vm.IsParent = IsParent;
                    vm.SelectedChild = SelectedChild;
                    vm.UserData = UserData;
                    vm.CourseData = val;
                });
            }
            catch (Exception)
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
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        #endregion
    }

}
