using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Space_Apps_ATMTech.ViewModels;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;
using System.Windows.Media.Imaging;

namespace Space_Apps_ATMTech
{
    public partial class MissionPage : PhoneApplicationPage
    {
        //public Mission mission { get; set; }
        public Space_Apps_ATMTech.TempModels.MissionFull MissionFull { get; set; }
        public Space_Apps_ATMTech.TempModels.News NewsSelected { get; set; }
        bool isplaying;
        public MissionPage()
        {
            InitializeComponent();
            MissionFull = null;
            isplaying = false;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            title.Title = MissionFull.Mission.Title;
            missionContent.Text = MissionFull.Mission.Content;
            NewsList.ItemsSource = MissionFull.News;
            MissionImage.Source = new BitmapImage(new Uri(MissionFull.Mission.ImageUrl));
            if (App.ViewModel.TempMissions.FirstOrDefault(x => x.Mission.Title == MissionFull.Mission.Title).Mission.Following == true)
            {
                FollowBtn.IsEnabled = false; 
                FollowBtn.Content = "Following";
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MissionNewsItem obj = e.Content as MissionNewsItem;
            if (obj != null)
            {
                obj.newsItem = NewsSelected;
            }
        }

        private async void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (App.ViewModel.TempMissions.FirstOrDefault(x => x.Mission.Title == MissionFull.Mission.Title).Mission.Following == false)
            {
                IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream("Favourites.txt", FileMode.Append, fileStorage));
                sw.WriteLine(MissionFull.Mission.Title);
                App.ViewModel.TempMissions.FirstOrDefault(x => x.Mission.Title == MissionFull.Mission.Title).Mission.Following = true;                
                sw.Close();
                FollowBtn.Content = "Following";
                FollowBtn.IsEnabled = false;
            }            
        }

        private void NewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewsList.SelectedItem != null)
            {
                NewsSelected = NewsList.SelectedItem as Space_Apps_ATMTech.TempModels.News;
                NavigationService.Navigate(new Uri("/MissionNewsItem.xaml", UriKind.Relative));
                NewsList.SelectedItem = null;
            }
        }

        private void MediaElement_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = (sender as MediaElement);
            if (isplaying)
            {
                item.Stop();
                isplaying = false;
                return;
            }
            item.Play();
            isplaying = true;
            
        }

        
    }
}