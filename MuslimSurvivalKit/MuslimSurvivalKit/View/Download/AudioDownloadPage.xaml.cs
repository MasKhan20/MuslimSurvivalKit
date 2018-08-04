using MuslimSurvivalKit.ViewModel;
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
    public partial class AudioDownloadPage : ContentPage
    {
        public AudioDownloadPage()
        {
            InitializeComponent();

            var viewmodel = new AudioDownloadViewModel(Navigation);

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<AudioDownloadViewModel, (string title, string message)>(viewmodel, "Alert",
                (s, e) =>
                {
                    DisplayAlert(e.title, e.message, "OK");
                });
        }
    }
}