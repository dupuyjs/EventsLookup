#Events Lookup

Application Windows 10 UWP pour consulter facilement les prochains meetups.

![Image of EventsLookup](https://github.com/dupuyjs/EventsLookup/tree/master/EventsLookup/EventsLookup/Assets/Commons/Design/Lookup.png)

### Getting Started
Pour l'utiliser, suivez simplement les �tapes suivantes:

1. Cr�er un compte sur http://www.meetup.com
2. Rendez-vous ensuite sur la page https://secure.meetup.com/fr-FR/meetup_api/key/
3. Copiez la cl� pour acc�der aux API de Meetup
4. Clonez le projet Events Lookups
5. Collez la cl� dans le fichier Keys.cs

```
    namespace EventsLookup.Helpers
    {
        public static class Keys
        {
            public const string MeetupApiKey = @"YourApiKey";
        }
    }
```

Compilez ! Le tour est jou�.