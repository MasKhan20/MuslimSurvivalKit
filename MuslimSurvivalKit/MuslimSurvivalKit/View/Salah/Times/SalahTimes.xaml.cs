using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Salah.Times
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalahTimesPage : ContentPage
	{
		public SalahTimesPage()
		{
			InitializeComponent();
            Title = "Salah Times";

            var viewmodel = new SalahTimesViewModel(Navigation);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<SalahTimesViewModel, (string title, string message)>(viewmodel, "Alert",
                (s, e) =>
                {
                    DisplayAlert(e.title, e.message, "OK");
                });
		}
	}
}