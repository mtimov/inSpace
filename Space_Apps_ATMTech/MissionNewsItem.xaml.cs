using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Space_Apps_ATMTech.TempModels;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace Space_Apps_ATMTech
{
    public partial class MissionNewsItem : PhoneApplicationPage
    {
        public News newsItem { get; set; }
        public MissionNewsItem()
        {
            InitializeComponent();
            newsItem = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           title.Text = newsItem.Title;
           content.Text = newsItem.Description;
           NewsImage.Source = new BitmapImage(new Uri(newsItem.ImageUrl));
        }
    }
}