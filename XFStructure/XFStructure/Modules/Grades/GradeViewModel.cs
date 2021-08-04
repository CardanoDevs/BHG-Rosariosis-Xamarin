using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.ViewModels;

namespace XFStructure.Modules.ClassRoom
{
    public class GradeViewModel : BasePageViewModel
    {
        #region Properties

        private List<DETAIL> _gradeDetailList;
        public List<DETAIL> GradeDetailList
        {
            get { return _gradeDetailList; }
            set { SetField(ref _gradeDetailList, value); }
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

        #endregion

        #region Command

        private ICommand _navigateToClassRoom;
        public ICommand NavigateToClassRoom => _navigateToClassRoom ?? (_navigateToClassRoom = new Command(ExecuteNavigateToClass));

        #endregion

        #region Method

        private async void ExecuteNavigateToClass()
        {
            await this.PopPageAsync<GradeViewModel>();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            this.GetCourseGrades();

        }

        private async void GetCourseGrades()
        {
            try
            {
                GradesResponse response;

                if (IsParent) {
                    response = await PerformAPICall(async () => await new ClassStore().GetGradesData(CourseData.COURSE_PERIOD_ID, SelectedChild.STUDENT_ID));
                }
                else
                {
                    response = await PerformAPICall(async () => await new ClassStore().GetGradesData(CourseData.COURSE_PERIOD_ID));
                }

                if (response != null)
                {

                    GradeDetailList = response.Grades;

                    //GradesList = new List<Grade>();
                    //GradesList = result.Grades;
                    //if (GradesList.Count() > 0)
                    //{
                    //    //IsGradeEmpty = false;
                    //}
                    //else
                    //{
                    //    //IsGradeEmpty = true;
                    //}
                }
                else
                {
                    //IsGradeEmpty = true;
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


        #endregion
    }
}
