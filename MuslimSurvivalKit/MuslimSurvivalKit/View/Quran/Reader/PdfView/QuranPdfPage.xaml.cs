using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran.Reader.PdfView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuranPdfPage : ContentPage
	{
		public QuranPdfPage(string path)
		{
			InitializeComponent();
            Title = "Tajweed Quran Pdf";
            pdfViewer.Toolbar.Enabled = false;

            var viewmodel = new QuranPdfViewModel(Navigation, path);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<QuranPdfViewModel, (string title, string message)>(viewmodel, "Alert",
                (s, e) =>
                {
                    DisplayAlert(e.title, e.message, "OK");
                });
		}

        private async void ToolbarDownload_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(App.TajweedPdfPath))
            {
                var confirm = await DisplayAlert("File Exists", "The Pdf file already exists, do you want to over write the file?", "Yes", "No");
                if (!confirm)
                    return;
                File.Delete(App.TajweedPdfPath);
            }

            DependencyService.Get<IMethodHelper>().ShowToast("Downloading Pdf");
            await Navigation.PushPopupAsync(new View.Download.DownloadFilePopup(App.TajweedPdfDownloadLink, App.TajweedPdfPath));
        }
    }
}