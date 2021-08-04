using DataStore.Customization.Requests.Login;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Shared.XFStructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.BasePages;
using XFStructure.Helpers;
using XFStructure.Modules.ClassRoom;
using XFStructure.Modules.Signup;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Login
{
    public class LoginViewModel : BasePageViewModel
    {
        #region Properties

        public string CLIENT_TOKEN { get; set; }
        public USER USER { get; set; }

        //private string _username = "waleed";
        private string _username = "atcartwright";
        //private string _username = "";

        public string Username
        {
            get { return _username; }
            set { SetField(ref _username, value); }
        }

        //private string _password = "superman2020#";
        //private string _password = "tabernacle";
        private string _password = "Gba2019";
        //private string _password = "";

        public string Password
        {
            get { return _password; }
            set { SetField(ref _password, value); }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetField(ref _rememberMe, value); }
        }

        #endregion


        #region Commands
        private ICommand _navigateToSignup;
        public ICommand NavigateToSignup => _navigateToSignup ?? (_navigateToSignup = new Command(ExecuteNavigateToSignupCommand));

        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(ExecuteLogin));

        //private ICommand _invokeAPICalls;
        //public ICommand InvokeAPICalls => _invokeAPICalls ?? (_invokeAPICalls = new Command(ExecuteInvokeAPICallsCommand));
        #endregion


        #region Methods
        public LoginViewModel()
        {

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter User Name.", "OK");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter Password.", "OK");
                return false;
            }
            return true;
        }

        private async void ExecuteLogin()
        {
            try
            {
                if (!Validate()) return;
                var requestData = new LoginRequest { UserName = Username, Password = Password };
                var result = await PerformAPICall(async () => await new LoginStore().PostLoginRequest(requestData));

                if (result != null && !string.IsNullOrEmpty(result.CLIENT_TOKEN))
                {
                    if (RememberMe)
                    {
                        CrossSecureStorage.Current.SetValue("RememberMe", "UserSave");
                        var userData = JsonConvert.SerializeObject(result.USER);
                        CrossSecureStorage.Current.SetValue("UserData", userData);
                    }
                    
                    CrossSecureStorage.Current.SetValue("clientToken", result.CLIENT_TOKEN);

                    var isParent = result.USER.PROFILE.ToLower() == "parent";

                    await Navigation.RemoveAllPreviousAndNavigate<LoginViewModel, YourClassRoomViewModel>((vm) =>
                    {
                        vm.IsParent = isParent;
                        vm.UserData = result.USER;
                        vm.IsFirst = true;
                        vm.SelectedChild = null;
                        vm.CourseList = new List<DataStore.Customization.Responses.ClassRoom.COURS>();
                        vm.FilteredCourseListToShow = new List<DataStore.Customization.Responses.ClassRoom.COURS>();
                        vm.AnnoucementList = new List<DataStore.Customization.Responses.ClassRoom.Annoucement.AnnouncementDetail>();
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void ExecuteNavigateToSignupCommand(object obj)
        {
            var param = obj as string;
            switch (param)
            {
                case "NavigateToSignup":

                    await this.PushPageFromCacheAsync<SignupViewModel>(c => c.Name = "Test Navigation");
                    break;
            }
        }

        #endregion

    }

}
