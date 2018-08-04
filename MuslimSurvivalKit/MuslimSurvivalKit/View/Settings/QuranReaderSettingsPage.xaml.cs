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
	public partial class QuranReaderSettingsPage : ContentPage
	{
		public QuranReaderSettingsPage()
		{
			InitializeComponent();

            var viewmodel = new QuranReaderSettingsViewModel();

            BindingContext = viewmodel;
		}
	}
}