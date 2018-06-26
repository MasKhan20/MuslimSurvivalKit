using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.View.Quran.Reader
{
    public class QuranReaderMasterPage : MasterDetailPage
    {
        SurahListPage masterPage;
        public QuranReaderMasterPage(int surahId = 1)
        {
            //NavigationPage.SetHasBackButton(this, true);
            // Why is button not visible ???

            BuildMaster(surahId);

            Master = masterPage;
            Detail = new QuranSurahPage(surahId);
        }

        private async void BuildMaster(int surahId)
        {
            masterPage = new SurahListPage() { Title = "Al Quran - Surahs" };

            masterPage.SurahList.ItemSelected += SurahList_ItemSelected;

            Title = await App.Database.GetSurahFullName(surahId);
        }

        private async void SurahList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Surah surah))
                return;

            Title = await App.Database.GetSurahFullName(surah.SurahId);
            var page = new QuranSurahPage(surah.SurahId);

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;

            Detail = (page);
            IsPresented = false;

            masterPage.SurahList.SelectedItem = null;
        }
    }
}
