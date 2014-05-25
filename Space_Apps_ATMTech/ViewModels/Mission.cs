using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Space_Apps_ATMTech.ViewModels
{
    public class Mission : INotifyPropertyChanged
    {

        private string imageUrl;
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                if (value != imageUrl)
                {
                    imageUrl = value;
                    NotifyPropertyChanged("ImageUrl");
                }
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public bool Following;
        

        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != content)
                {
                    content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        private string videoUrl;
        public string VideoUrl
        {
            get
            {
                return videoUrl;
            }
            set
            {
                if (value != videoUrl)
                {
                    videoUrl = value;
                    NotifyPropertyChanged("Video");
                }
            }
        }


        private bool hasLiveStream;
        public bool HasLiveStream
        {
            get
            {
                return hasLiveStream;
            }
            set
            {
                if (value != hasLiveStream)
                {
                    hasLiveStream = value;
                    NotifyPropertyChanged("HasLiveStream");
                }
            }
        }


        private string liveStreamUrl;
        public string LiveStreamUrl
        {
            get
            {
                return liveStreamUrl;
            }
            set
            {
                if (value != liveStreamUrl)
                {
                    liveStreamUrl = value;
                    NotifyPropertyChanged("LiveStreamUrl");
                }
            }
        }

        private DateTime launchDate;
        public DateTime LaunchDate
        {
            get
            {
                return launchDate;
            }
            set
            {
                if (value != launchDate)
                {
                    launchDate = value;
                    NotifyPropertyChanged("DateLaunch");
                }
            }
        }


        public string LaunchDateString
        {
            get
            {
                return LaunchDate.ToShortDateString();
            }
        }

        public List<NewsItem> MissionNews { get; set; }


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