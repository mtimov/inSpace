using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Space_Apps_ATMTech.Resources;
using Space_Apps_ATMTech.ViewModels;
using Newtonsoft.Json;
using System.Text;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;
using System.Windows.Controls.Primitives;

namespace Space_Apps_ATMTech
{
    public partial class MainPage : PhoneApplicationPage
    {
        public bool isInFeatured {get;set;}
        public Mission mission { get; set; }
        public Space_Apps_ATMTech.TempModels.MissionFull MissionFull { get; set; }
        public NewsItem newsItem { get; set; }
        public ApplicationBarIconButton refreshAppBar { get; set; }
        public ApplicationBarMenuItem nasaMissions { get; set; }
        public ApplicationBarMenuItem esaMissions { get; set; }
        public ApplicationBarMenuItem jaxaMissions { get; set; }
        public ApplicationBarMenuItem allMissions { get; set; }

        public ProgressBar myProgressObj { get; set; }
        public Popup myPopup { get; set; }

        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            mission = null;
            MissionFull = null;
            BuildLocalizedApplicationBar();
            myProgressObj = myProgress;
            
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            refreshAppBar = new ApplicationBarIconButton();
            refreshAppBar.IconUri = new Uri("/Assets/search.png", UriKind.Relative);
            refreshAppBar.Text = AppResources.AppBarRefreshButton;
            refreshAppBar.Click += refreshAppBar_Click;

            nasaMissions = new ApplicationBarMenuItem();
            nasaMissions.Text = AppResources.AppBarNasaMissions;
            nasaMissions.Click += nasaMissions_Click;

            esaMissions = new ApplicationBarMenuItem();
            esaMissions.Text = AppResources.AppBarEsaMissions;
            esaMissions.Click +=esaMissions_Click;

            jaxaMissions = new ApplicationBarMenuItem();
            jaxaMissions.Text = AppResources.AppBarJaxaMissions;
            jaxaMissions.Click +=jaxaMissions_Click;

            allMissions = new ApplicationBarMenuItem();
            allMissions.Text = AppResources.AppBarAllMissions;
            allMissions.Click +=allMissions_Click;
            
            ApplicationBar.Buttons.Add(refreshAppBar);
            ApplicationBar.MenuItems.Add(nasaMissions);
            ApplicationBar.MenuItems.Add(esaMissions);
            ApplicationBar.MenuItems.Add(jaxaMissions);
            ApplicationBar.MenuItems.Add(allMissions);
        }

       

        private void refreshAppBar_Click(object sender, EventArgs e)
        {
            if (isInFeatured)
            {
                if (!App.ViewModel.IsDataLoaded)
                {
                    App.ViewModel.LoadDataSecond(myProgressObj);
                    App.ViewModel.LoadData();
                }
            }
            else
            {
                my_popup_xaml.Width = LayoutRoot.ActualWidth;
                searchTb.Width = my_popup_xaml.Width * 0.9;
                this.Opacity = 0.3;
                my_popup_xaml.IsOpen = true;
            }
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (!App.ViewModel.IsDataLoaded)
            {
                
                
                App.ViewModel.LoadDataSecond(myProgressObj);
                App.ViewModel.LoadData();
                    
                
                listNews.ItemsSource = App.ViewModel.News;
            }

            list.ItemsSource = App.ViewModel.TempMissions;

            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader sr = null;
            List<String> names = new List<string>();
            try
            {
                sr = new StreamReader(new IsolatedStorageFileStream("Favourites.txt", FileMode.OpenOrCreate, fileStorage));
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    names.Add(line);
                }
                sr.Close();
            }
            catch
            {
                MessageBox.Show("File not created");
            }
            App.ViewModel.Favourites.Clear();
            foreach (String st in names)
            {
                //Mission m = App.ViewModel.Missions.FirstOrDefault(x=>x.Name == st);
                Space_Apps_ATMTech.TempModels.MissionFull mf = App.ViewModel.TempMissions.Where(x => x.Mission.Title == st).FirstOrDefault();
                if (mf != null) { 
                    App.ViewModel.TempMissions.FirstOrDefault(x => x.Mission.Title == st).Mission.Following = true;
                    App.ViewModel.Favourites.Add(mf);
                }
            }
                     
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MissionPage obj = e.Content as MissionPage;
            if (obj != null)
            {
                obj.MissionFull = MissionFull;
            }

            NewsItemPage nobj = e.Content as NewsItemPage;
            if (nobj != null)
            {
                nobj.newsItem = newsItem;
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                MissionFull = list.SelectedItem as Space_Apps_ATMTech.TempModels.MissionFull;                
                NavigationService.Navigate(new Uri("/MissionPage.xaml", UriKind.Relative));
                list.SelectedItem = null;
            }
        }

        private void listNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listNews.SelectedItem != null)
            {
                newsItem = listNews.SelectedItem as NewsItem;
               
                NavigationService.Navigate(new Uri("/NewsItemPage.xaml", UriKind.Relative));
                listNews.SelectedItem = null;
            }
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listFavourites.SelectedItem != null)
            {
                MissionFull = listFavourites.SelectedItem as Space_Apps_ATMTech.TempModels.MissionFull;
                NavigationService.Navigate(new Uri("/MissionPage.xaml", UriKind.Relative));
                list.SelectedItem = null;
            }
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == missions)
            {
                ApplicationBar.IsVisible = true;
                refreshAppBar.IconUri = new Uri("/Assets/search.png", UriKind.Relative);
                isInFeatured = false;
                if (ApplicationBar.MenuItems.Count == 0)
                {
                    ApplicationBar.MenuItems.Add(nasaMissions);
                    ApplicationBar.MenuItems.Add(esaMissions);
                    ApplicationBar.MenuItems.Add(jaxaMissions);
                    ApplicationBar.MenuItems.Add(allMissions);
                   
                }
                    
                list.ItemsSource = App.ViewModel.TempMissions;
            }
            else if (e.Item == featured)
            {
                ApplicationBar.IsVisible = true;
                refreshAppBar.IconUri = new Uri("/Assets/refresh.png", UriKind.Relative);
                ApplicationBar.MenuItems.Clear();
                isInFeatured = true;
            } 
            else
            {
                ApplicationBar.IsVisible = false;
            }

            if (e.Item == favourites)
            {
                IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                StreamReader sr = null;
                List<String> names = new List<string>();
                try
                {
                    sr = new StreamReader(new IsolatedStorageFileStream("Favourites.txt", FileMode.OpenOrCreate, fileStorage));
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        names.Add(line);
                    }
                    sr.Close();
                }
                catch
                {
                    MessageBox.Show("File not created");
                }
                App.ViewModel.Favourites.Clear();
                foreach (String st in names)
                {
                    //Mission m = App.ViewModel.Missions.FirstOrDefault(x=>x.Name == st);
                    Space_Apps_ATMTech.TempModels.MissionFull mf = App.ViewModel.TempMissions.Where(x => x.Mission.Title == st).FirstOrDefault();
                    if (mf != null)
                    {
                        App.ViewModel.TempMissions.FirstOrDefault(x => x.Mission.Title == st).Mission.Following = true;
                        App.ViewModel.Favourites.Add(mf);
                    }
                }
            }
        }

        private void btn_continue_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            this.Opacity = 1;
            my_popup_xaml.IsOpen = false;
            string keyword = searchTb.Text;
            searchTb.Text = "";
            var items = App.ViewModel.TempMissions.Where(x => x.Mission.Title.ToLower().Contains(keyword.ToLower()));
            list.ItemsSource = items.ToList();
        }

        private void allMissions_Click(object sender, EventArgs e)
        {
            list.ItemsSource = App.ViewModel.TempMissions;
        }

        private void jaxaMissions_Click(object sender, EventArgs e)
        {
            string jaxastr = "JAXA";
            var jaxa = App.ViewModel.TempMissions.Where(x => x.Mission.Agency.ToUpper() == jaxastr);
            list.ItemsSource = jaxa.ToList();
        }

        private void esaMissions_Click(object sender, EventArgs e)
        {
            string esastr = "ESA";
            var esa = App.ViewModel.TempMissions.Where(x => x.Mission.Agency.ToUpper() == esastr);
            list.ItemsSource = esa.ToList();
        }

        private void nasaMissions_Click(object sender, EventArgs e)
        {
            string nasastr = "NASA";
            var nasa = App.ViewModel.TempMissions.Where(x => x.Mission.Agency.ToUpper() == nasastr);
            list.ItemsSource = nasa.ToList();
        }

        private void btn_cancel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            my_popup_xaml.IsOpen = false;
            searchTb.Text = "";
            this.Opacity = 1;
        }

        private void listNews_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

      

      
    }
}