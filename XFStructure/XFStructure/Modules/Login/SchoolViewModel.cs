using DataStore.Customization.Paths;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Login
{
    class SchoolViewModel : BasePageViewModel
    {
        #region Properties
        private List<School> _schoolList;
        public List<School> SchoolList
        {
            get { return _schoolList; }
            set {SetField(ref _schoolList , value); }
        }

        private bool _isSchoolVisible;
        public bool IsSchoolVisible
        {
            get { return _isSchoolVisible; }
            set {SetField(ref _isSchoolVisible , value); }
        }
        private List<School> _filteredSchoolList;
        public List<School> FilteredSchoolList
        {
            get { return _filteredSchoolList; }
            set { SetField(ref _filteredSchoolList, value); }
        }
        #endregion
        private string _keyword;
        public string keyword
        {
            get { return _keyword; }
            set
            {
                SetField(ref _keyword, value);
            }
        }
        #region Command

        private ICommand _schoolSelected;
        public ICommand SchoolSelected => _schoolSelected ?? (_schoolSelected = new Command(SelectedSchool));

        public ICommand FilterSchool => (new Command(SchoolFilter));

        #endregion

        #region Method

        public SchoolViewModel()
        {
            FilteredSchoolList = new List<School>();
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            //FilteredSchoolList = SchoolList;
        }
        public async void SchoolFilter(object obj)
        {
            await LoadSchools();
            List<School> filter;
            filter = new List<School>();
            if (keyword != null && keyword.Length != 0)
            {
                foreach (School school in SchoolList)
                {
                    if (school.Name.ToLower().StartsWith(keyword.ToLower()))
                    {
                        filter.Add(school as School);
                    }
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter School Name.", "OK");
            }
            FilteredSchoolList = filter;
        }
        private async Task LoadSchools()
        {
            try
            {
                var result = await PerformAPICall(async () => await new LoginStore().GetSchools());
                if (result != null && result.Schools != null)
                {
                    SchoolList = result.Schools;
                }
            }
            catch (Exception)
            {

            }           
        }       

        private async void SelectedSchool(object obj)
        {
            try
            {
                var selectschool = obj as School;
                //ApiResources.BaseUrlPath = selectschool.BaseURL;
                CrossSecureStorage.Current.SetValue("BaseUrl", selectschool.BaseURL);
                if (CrossSecureStorage.Current.HasKey("BaseUrl"))
                {
                    await Navigation.PushPageAsync<SchoolViewModel,LoginViewModel>((vm) => { });
                }
            }
            catch (Exception)
            {

            }
            
        }
        #endregion
    }
}
