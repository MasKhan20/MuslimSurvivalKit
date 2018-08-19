using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Root
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RootPage : ContentPage
	{
		public RootPage()
		{
			InitializeComponent();
            Title = App.AppName;

            var viewmodel = new RootViewModel(Navigation);

            BindingContext = viewmodel;
		}
	}
}