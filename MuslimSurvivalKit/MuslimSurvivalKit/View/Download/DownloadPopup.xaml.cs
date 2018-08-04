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
	public partial class DownloadPopup : PopupPage
	{
		public DownloadPopup()
		{
			InitializeComponent();

            var viewmodel = new DownloadPopupViewModel(Navigation);

            BindingContext = viewmodel;
		}
	}
}