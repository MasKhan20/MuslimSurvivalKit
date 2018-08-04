using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppSettingsPage : ContentPage
	{
		public AppSettingsPage()
		{
			InitializeComponent();

            Title = $"{App.AppName} - Settings";

            var viewmodel = new AppSettingsViewModel(Navigation);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<AppSettingsViewModel, (string title, string message)>(viewmodel, "Alert",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
		}
	}
}