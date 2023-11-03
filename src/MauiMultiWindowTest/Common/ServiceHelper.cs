namespace MauiMultiWindowTest.Common
{

    public static class ServiceHelper
    {
        public static TService GetService<TService>()
            => ServiceHelper.Current.GetService<TService>();

        private static IServiceProvider Current
            =>
#if WINDOWS10_0_17763_0_OR_GREATER
			        MauiWinUIApplication.Current.Services;
#elif IOS || MACCATALYST
                MauiUIApplicationDelegate.Current.Services;
#else
			        null;
#endif

    }

}
