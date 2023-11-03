using System.Threading;
using UIKit;

namespace MauiMultiWindowTest;

public class Program
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        
#if DEBUG
        Thread.Sleep(5000);
#endif
        
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
