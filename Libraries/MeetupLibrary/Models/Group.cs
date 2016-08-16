namespace MeetupLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a Meetup group.
    /// </summary>
    public class Group : INotifyPropertyChanged
    {
        private List<Event> allEvents = null;
        private bool isEventsVisible = false;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets group's identifier.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets group's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets URL to the group's home page on meetup.com.
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; internal set; }

        /// <summary>
        /// Gets unique group name as it appears in the URL, no slashes
        /// </summary>
        [JsonProperty("urlname")]
        public string UrlName { get; internal set; }

        /// <summary>
        /// Gets group description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets date and time that the group was founded, in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("created")]
        public object Created { get; internal set; }

        /// <summary>
        /// Gets score.
        /// </summary>
        [JsonProperty("score")]
        public double Score { get; internal set; }

        /// <summary>
        /// Gets city of the group
        /// </summary>
        [JsonProperty("city")]
        public string City { get; internal set; }

        /// <summary>
        /// Gets country of the group
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets localized country name of the group.
        /// </summary>
        [JsonProperty("localized_country_name")]
        public string LocalizedCountryName { get; internal set; }

        /// <summary>
        /// Gets state of the group, if in US or Canada
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }

        /// <summary>
        /// Gets join mode. "open", "closed", or "approval".
        /// </summary>
        [JsonProperty("join_mode")]
        public string JoinMode { get; internal set; }

        /// <summary>
        /// Gets visibility. "public", "public_limited", or "members" only.
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; internal set; }

        /// <summary>
        /// Gets Latitude.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; internal set; }

        /// <summary>
        /// Gets Longitude.
        /// </summary>
        [JsonProperty("lon")]
        public double Longitude { get; internal set; }

        /// <summary>
        /// Gets current number of members in the group.
        /// </summary>
        [JsonProperty("members")]
        public int Members { get; internal set; }

        /// <summary>
        /// Gets group's primary organizer.
        /// </summary>
        [JsonProperty("organizer")]
        public Organizer Organizer { get; internal set; }

        /// <summary>
        /// Gets what the group calls its members.
        /// </summary>
        [JsonProperty("who")]
        public string Who { get; internal set; }

        /// <summary>
        /// Gets main photo associated with the group.
        /// </summary>
        [JsonProperty("group_photo")]
        public Photo GroupPhoto { get; internal set; }

        /// <summary>
        /// Gets key photo associated with the group.
        /// </summary>
        [JsonProperty("key_photo")]
        public Photo KeyPhoto { get; internal set; }

        /// <summary>
        /// Gets universal timezone identifier for the group.
        /// </summary>
        [JsonProperty("timezone")]
        public string TimeZone { get; internal set; }

        /// <summary>
        /// Gets the next upcoming event, if the group has one.
        /// </summary>
        [JsonProperty("next_event")]
        public Event NextEvent { get; internal set; }

        /// <summary>
        /// Gets category associated with this group.
        /// </summary>
        [JsonProperty("category")]
        public Category Category { get; internal set; }

        /// <summary>
        /// Gets a small set of photos from the group.
        /// </summary>
        [JsonProperty("photos")]
        public List<Photo> Photos { get; internal set; }

        /// <summary>
        /// Gets all upcoming events.
        /// </summary>
        public List<Event> AllEvents
        {
            get
            {
                return this.allEvents;
            }

            internal set
            {
                this.allEvents = value;
                this.NotifyPropertyChanged("AllEvents");
                this.NotifyPropertyChanged("EventsCount");
            }
        }

        /// <summary>
        /// Gets current number of upcoming events.
        /// </summary>
        public int EventsCount
        {
            get
            {
                return this.allEvents?.Count ?? 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether upcoming events are visible or not.
        /// </summary>
        public bool IsEventsVisible
        {
            get
            {
                return this.isEventsVisible;
            }

            internal set
            {
                this.isEventsVisible = value;
                this.NotifyPropertyChanged("IsEventsVisible");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
