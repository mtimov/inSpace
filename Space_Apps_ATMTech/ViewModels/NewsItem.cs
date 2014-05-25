using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Space_Apps_ATMTech.ViewModels
{
    public class NewsItem : INotifyPropertyChanged
    {

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string ShortDescription
        {
            get
            {
                if (content.Length > 37)
                {
                    return content.Substring(0, 37) + "...";
                }
                else
                {
                    return content;
                }
            }
        }

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

        private string image;
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value != image)
                {
                    image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        private string missionUrl;
        public string MissionUrl
        {
            get
            {
                return missionUrl;
            }
            set
            {
                if (value != missionUrl)
                {
                    missionUrl = value;
                    NotifyPropertyChanged("MissionUrl");
                }
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
