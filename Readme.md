#Events Lookup

Application Windows 10 UWP pour consulter facilement les prochains meetups.

![Image of EventsLookup](https://blogmedia.blob.core.windows.net/images/Lookup.png)

### Getting Started
Pour l'utiliser, suivez simplement les étapes suivantes:

1. Clonez le projet Events Lookups
2. Créer un compte sur http://www.meetup.com
3. Rendez-vous ensuite sur la page https://secure.meetup.com/fr-FR/meetup_api/key/
4. Copiez la clé pour accéder aux API de Meetup
5. Collez la clé dans le fichier Keys.cs

```
    namespace EventsLookup.Helpers
    {
        public static class Keys
        {
            public const string MeetupApiKey = @"YourApiKey";
        }
    }
```

Compilez ! Le tour est joué.