using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeetupLibrary.Models
{
    public class Member : INotifyPropertyChanged
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("topics")]
        public Topic[] Topics { get; set; }
        [JsonProperty("joined")]
        public long Joined { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("photo")]
        public Photo Photo { get; set; }
        [JsonProperty("lon")]
        public float Longitude { get; set; }
        [JsonProperty("other_services")]
        public Other_Services OtherServices { get; set; }

        private string _name = string.Empty;
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [JsonProperty("visited")]
        public long Visited { get; set; }
        [JsonProperty("self")]
        public Self Self { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        [JsonProperty("lat")]
        public float Latitude { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Other_Services
    {
    }

    public class Self
    {
        public Common common { get; set; }
    }

    public class Common
    {
    }
}
