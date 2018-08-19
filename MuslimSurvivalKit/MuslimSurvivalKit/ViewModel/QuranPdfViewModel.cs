using MuslimSurvivalKit.Data;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranPdfViewModel : BaseViewModel
    {
        #region Binding Commands
        // In code behind
        //public ICommand DownloadCommand => new Command(Download_Command);
        public ICommand RefreshCommand => new Command(LoadPdf);
        #endregion

        #region Binding Properties
        private Stream _pdfFileStream;
        public Stream PdfFileStream
        {
            get { return _pdfFileStream; }
            set
            {
                _pdfFileStream = value;
                RaisePropertyChanged(nameof(PdfFileStream));
            }
        }
        #endregion

        private string _path;
        private INavigation Navigation;
        public QuranPdfViewModel(INavigation navigation, string path)
        {
            Navigation = navigation;
            _path = path;

            LoadPdf();
        }

        private async void LoadPdf()
        {
            if (Directory.Exists(_path))
            {
                await Navigation.PushPopupAsync(new View.Download.DownloadFilePopup(App.TajweedPdfDownloadLink, App.TajweedPdfPath));
            }

            try
            {
                var stream = File.Open(App.TajweedPdfPath, FileMode.Open);
                PdfFileStream = stream;
            }
            catch (Exception exc)
            {
                MessagingCenter.Send(this, "Alert", (exc.Message, exc.ToString()));
            }
        }
        
        //private async void Download_Command()
        //{
        //    DependencyService.Get<IMethodHelper>().ShowToast("Downloading Pdf");
        //    await Navigation.PushPopupAsync(new View.Download.DownloadFilePopup(App.TajweedPdfDownloadLink, App.TajweedPdfPath));
        //}
    }
}
