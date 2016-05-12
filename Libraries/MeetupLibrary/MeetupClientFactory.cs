using System;

namespace MeetupLibrary
{
    public static class MeetupClientFactory
    {
        /// <summary>
        /// Create The Meetup Platform client
        /// </summary>
        /// <param name="apiKey">Echnoest API Key</param>
        /// <returns></returns>
        public static IMeetupClient CreateMeetupClient(string apiKey)
        {
            return new MeetupClient(apiKey);
        }
    }
}
