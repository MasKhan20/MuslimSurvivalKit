﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;

namespace MuslimSurvivalKit.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            Google.MobileAds.MobileAds.Configure("YOUR IOS APP ID HERE");

            global::Xamarin.Forms.Forms.Init();

            Syncfusion.SfPicker.XForms.iOS.SfPickerRenderer.Init();
            ImageCircleRenderer.Init();
            string dbPath = Renderers.FileAccessHelper.GetLocalFilePath(App.DatabaseFile);

            LoadApplication(new App());

            new Syncfusion.SfPdfViewer.XForms.iOS.SfPdfDocumentViewRenderer();

            return base.FinishedLaunching(app, options);
        }
    }
}
