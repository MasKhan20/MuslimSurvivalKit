using MuslimSurvivalKit.ViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Download
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DownloadFilePopup : PopupPage
	{
		public DownloadFilePopup(string link, string path)
		{
			InitializeComponent();

            var viewmodel = new DownloadFileViewModel(Navigation, link, path);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<DownloadFileViewModel, (string title, string message)>(viewmodel, "Alert",
                (s, e) =>
                {
                    DisplayAlert(e.title, e.message, "OK");
                });
		}
	}
}