namespace DataStore.Customization.Helpers
{
    public static class AppConstants
    {
        public struct StringKeys
        {
            public const string Username = "Username";
            public const string Password = "Password";
            public const string DeviceId = "DeviceId";
        }

        public struct StoreLinks
        {
            public const string Playstore = "https://play.google.com/store/apps/myapp";
            public const string Appstore = "https://itunes.apple.com/pk/app/myapp";
        }

        public static string PushToken;
    }
}
