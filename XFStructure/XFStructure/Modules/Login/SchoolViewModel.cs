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

        #endregion

        #region Command

        private ICommand _schoolSelected;
        public ICommand SchoolSelected => _schoolSelected ?? (_schoolSelected = new Command(SelectedSchool));

        #endregion

        #region Method

        public SchoolViewModel()
        {

        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadSchools();
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
            catch (Exception ex)
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
            catch (Exception ex)
            {

            }
            
        }

        #endregion
    }
}
