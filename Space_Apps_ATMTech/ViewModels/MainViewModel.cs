using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Space_Apps_ATMTech.Resources;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Controls;
namespace Space_Apps_ATMTech.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NewsItem> News { get; private set; }
        public ObservableCollection<Mission> Missions { get; private set; }
        public ObservableCollection<Space_Apps_ATMTech.TempModels.MissionFull> Favourites { get; set; }
        public ObservableCollection<Space_Apps_ATMTech.TempModels.MissionFull> TempMissions { get; set; }
        public bool IsDataLoaded { get; private set; }
        public string dateString { get; set; }
        public List<NewsItem> lista { get; set; }
        public ProgressBar bar { get; set; }
        public MainViewModel()
        {
            this.News = new ObservableCollection<NewsItem>();
            this.Missions = new ObservableCollection<Mission>();
            this.Favourites = new ObservableCollection<Space_Apps_ATMTech.TempModels.MissionFull>();
            this.TempMissions = new ObservableCollection<TempModels.MissionFull>();

        }

        public void LoadDataSecond(ProgressBar bar)
        {
            this.bar = bar;
        }
        public void LoadData()
        {


                            
            try
            {
                string url = "http://nasamissions.azurewebsites.net/api/values";
                WebClient client2 = new WebClient();
                
                client2.Headers["Accept"] = "application/json";
                client2.DownloadStringAsync(new Uri(url));
                client2.DownloadStringCompleted += (s1, e1) =>
                    {
                        if (e1.Error != null)
                        {
                            News.Clear();
                            Console.WriteLine("Error.");
                            bar.Visibility = System.Windows.Visibility.Collapsed;
                            NewsItem ni = new NewsItem();
                            ni.Title = "Connection failed";
                            ni.Content = "Please check your internet";

                            News.Add(ni);
                            return;
                        }

                        List<Space_Apps_ATMTech.TempModels.MissionFull> data = JsonConvert.DeserializeObject<List<Space_Apps_ATMTech.TempModels.MissionFull>>(e1.Result.ToString());
                        foreach (Space_Apps_ATMTech.TempModels.MissionFull item in data)
                        {
                            //Space_Apps_ATMTech.TempModels.MissionFull mf = item as Space_Apps_ATMTech.TempModels.MissionFull;
                            this.TempMissions.Add(item);
                        }                        
                    };
            }
            catch
            {

            }

            try
            {               
                
                    string uri = "http://data.nasa.gov/api/get_recent_datasets?count=15";
                    WebClient client = new WebClient();

                    client.Headers["Accept"] = "application/json";
                    
                    
                    client.DownloadStringAsync(new Uri(uri));
                    client.DownloadStringCompleted += (s1, e1) =>
                    {
                        
                        if (e1.Error != null)
                        {
                            News.Clear();
                            Console.WriteLine("Error.");
                            bar.Visibility = System.Windows.Visibility.Collapsed;
                            NewsItem ni =  new NewsItem();
                            ni.Title = "Connection failed";
                            ni.Content = "Please check your internet";
                            
                            News.Add(ni);
                            return;
                        }
                        News.Clear();
                        this.IsDataLoaded = true;
                        bar.Visibility = System.Windows.Visibility.Collapsed;
                        var data = JsonConvert.DeserializeObject<RootObject>(e1.Result.ToString());
                       
                        foreach (Post post in data.posts)
                        {
                            NewsItem it = new NewsItem();
                            String str = post.content;

                            StringBuilder sb = new StringBuilder();
                            bool canTake = true;

                            foreach (char c in str)
                            {
                                if (c == '<' || c == '&')
                                {
                                    canTake = false;
                                }
                                else if (c == '>' || c == ';')
                                {
                                    canTake = true;
                                }
                                else
                                {
                                    if (canTake)
                                    {
                                        sb.Append(c);
                                    }
                                }
                            }
                            str = sb.ToString();
                            it.Content = str;

                            it.Title = post.title;
                            it.MissionUrl = post.url;
                            News.Add(it);
                        }
                        
                    };
                
            }
            catch (Exception ee)
            {
                Console.Write(ee.ToString());
            }

         
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}