using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.ClassRoom;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Login
{
    class CheckUserStatusViewModel : BasePageViewModel
    {
        public CheckUserStatusViewModel()
        {
            
        }

        public async override void OnAppearing()
        {            
            base.OnAppearing();
            CheckingUserStatus();
        }

        private async void CheckingUserStatus()
        {
            try
            {
                var result = await PerformAPICall(async () => await new LoginStore().CheckTokenLife());
                
                if (!result.IsValid)
                {
                    await this.PushPageFromCacheAsync<LoginViewModel>((vm) => { });
                }
                else
                {
                    var _data = CrossSecureStorage.Current.GetValue("UserData", null);
                    var userData = JsonConvert.DeserializeObject<USER>(_data);
                    await Navigation.RemoveAllPreviousAndNavigate<CheckUserStatusViewModel, YourClassRoomViewModel>(
                        (vm) => {
                            vm.UserData = userData;
                            vm.IsFirst = true;
                            vm.IsParent = userData.PROFILE.ToLower() == "parent";
                        });
                    //await this.PushPageFromCacheAsync<YourClassRoomViewModel>((vm) => { 
                    //    vm.UserData = userData;
                    //    vm.IsFirst = true;
                    //    vm.IsParent = userData.PROFILE.ToLower() == "parent";
                    //});
                }
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
