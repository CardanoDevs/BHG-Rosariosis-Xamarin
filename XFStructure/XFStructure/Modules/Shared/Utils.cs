using Plugin.FilePicker;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;
using XFStructure.ViewModels;

namespace XFStructure.Modules.Shared
{
    public class Utils
    {
        public static async Task<PermissionStatus> CheckAndRequestStoragePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            try
            {               
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
                return status;
            }
            catch (Exception)
            {
                return PermissionStatus.Unknown;
            }           
        }
    }
}
