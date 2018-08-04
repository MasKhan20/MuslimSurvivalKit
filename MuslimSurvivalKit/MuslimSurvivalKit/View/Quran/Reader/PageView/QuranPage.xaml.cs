using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran.Reader.PageView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuranPage : ContentPage
	{
        public QuranPageViewModel _viewmodel;
		public QuranPage(QuranPages parent, string imagePath, int pageNumber, List<Bookmark> bookmarks)
		{
			InitializeComponent();

            _viewmodel = new QuranPageViewModel(parent, imagePath, pageNumber, bookmarks);

            BindingContext = _viewmodel;

            MessagingCenter.Subscribe<QuranPageViewModel, (string title, string message)>(_viewmodel, "Alert",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }
	}
}