using MuslimSurvivalKit.Model;
//using Plugin.MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.View.Quran.Reader.SurahView
{
    public class QuranReaderMasterPage : MasterDetailPage
    {
        SurahListPage masterPage;
        public QuranReaderMasterPage(int surahId = 1, int ayahId = 1, bool jumpTo = false)
        {
            //NavigationPage.SetHasBackButton(this, true);
            // Why is button not visible ???

            BuildMaster(surahId);
            Master = masterPage;
            Detail = new QuranSurahPage(surahId, ayahId, jumpTo);
        }

        private async void BuildMaster(int surahId)
        {
            masterPage = new SurahListPage(false) { Title = "Surahs" };

            masterPage.SurahList.ItemSelected += SurahList_ItemSelected;

            Title = await App.Database.GetSurahFullName(surahId);
        }

        private async void SurahList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Surah surah))
                return;

            //await CrossMediaManager.Current.PlaybackController.Stop();
            //CrossMediaManager.Current.MediaQueue.Clear();

            Title = await App.Database.GetSurahFullName(surah.SurahId);
            var page = new QuranSurahPage(surah.SurahId, 1, false);

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;

            Detail = (page);
            IsPresented = false;

            masterPage.SurahList.SelectedItem = null;
        }
    }
}
