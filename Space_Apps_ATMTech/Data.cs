using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Apps_ATMTech
{
    public class Category
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }
    }

    public class CustomFields
    {
        public List<string> more_info_link { get; set; }
        public List<string> acronym { get; set; }
        public List<string> datagov_id { get; set; }
        public List<string> curator_person_name { get; set; }
        public List<string> curator_person_email { get; set; }
        public List<string> curator_url { get; set; }
        public List<string> curator_center { get; set; }
        public List<string> curator_org_name { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string title_plain { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string date { get; set; }
        public string modified { get; set; }
        public List<Category> categories { get; set; }
        public List<object> tags { get; set; }
        public CustomFields custom_fields { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public int count { get; set; }
        public int count_total { get; set; }
        public int pages { get; set; }
        public List<Post> posts { get; set; }
    }
}
