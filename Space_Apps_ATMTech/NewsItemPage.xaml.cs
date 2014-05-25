using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Space_Apps_ATMTech.ViewModels;
using Microsoft.Phone.Tasks;

namespace Space_Apps_ATMTech
{
    public partial class NewsItemPage : PhoneApplicationPage
    {
        public NewsItem newsItem { get; set; }
        public NewsItemPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           title.Text = newsItem.Title;
           content.Text = newsItem.Content;
        }

        private void linkButton_Click(object sender, RoutedEventArgs e)
        {   
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(newsItem.MissionUrl, UriKind.Absolute);
            webBrowserTask.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.Title = newsItem.Title;
            shareLinkTask.LinkUri = new Uri(newsItem.MissionUrl, UriKind.Absolute);
            shareLinkTask.Message = newsItem.ShortDescription;
            shareLinkTask.Show();
            
        }

       
        

      
    }
}