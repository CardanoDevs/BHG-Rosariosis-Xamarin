//using Android.App;
//using Android.Util;
//using Firebase.Iid;
//using Plugin.SecureStorage;
//using System;

//namespace XFStructure.Droid.Helpers
//{
//    [Service]
//    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
//    public class FCMTokenClass : FirebaseInstanceIdService
//    {
//        const string TAG = "MyFirebaseIIDService";
//        public override void OnTokenRefresh()
//        {
//            try
//            {
//                Log.Debug(TAG, "Refreshed token: 1234");
//                if (FirebaseInstanceId.Instance != null)
//                {
//                    var refreshedToken = FirebaseInstanceId.Instance.Token;
//                    if (!string.IsNullOrEmpty(refreshedToken))
//                    {
//                        CrossSecureStorage.Current.SetValue("FCMToken", refreshedToken);
//                    }

//                    SendRegistrationToServer(refreshedToken);
//                }
//            }
//            catch (Exception)
//            {

//            }
//        }
//        void SendRegistrationToServer(string token)
//        {
//            // Add custom implementation, as needed.
//        }
//    }
//}