We would like to introduce a new action to perform in the chatbots: displaying a non-clickable carousel made of cards coming from the database

As a user, I will see a series of cards displayed horizontally, but when i click on them, it doesn't trigger anything.

In our JSON format, the non-clickable carousel will be called as an action in a set of steps. There will be two types of non-clickable carousels:
1. Non-clickable carousels made of cards coming from the server
2. Non-clickable carousels made of cards pre-defined in the JSON (or in Github)
_____________________________________________

## 1. Non-clickable carousels with content from the server

In the JSON code, it will look as follows (inside a Step):
 
```
{
  "Type": "Action",
  "Name": "ShowCarouselFromIntention",
  "Parameters": {
    "IntentionId": "43B296",
    "NumberOfCards": "3"
  }
}
```

When we see this action, we should use the following API (http://gw-ideas.azurewebsites.net/bot/IdeasOfTheDay/forRecipient/all/andIntention/43B296?isquote=false&amp;nbcards=50&amp;mode=pickrandom&amp;nbrand=20), use as many cards as "NumberOfCards" asks for (3 in this case) and show a carousel of 3 cards from the intention 43B296

-> if you receive more than 3 cards, then take 3 at random
-> if you receive less than 3 cards, then show as many as possible
-> if you receive none, print none

When we receive this card (text + image) from the server, we should:
- fill the "Picture" field with the image we receive
- fill the "Title" field with the text that we receive. We usually limit the title to 80 characters. If the text you receive is longer than 80 characters, then we should show 77 characters and 3 dots (...)


## 2. Non-clickable carousels with cards defined Github

In the JSON code, it will look as follows (inside a Step):
 
```
{ 
"Type": "Action",
 "Name": "ShowCarouselFromList", 
"Parameters": { 
    "Path": "/data/common/carousel/nutty/parcs.json"} 
}
```
When we see this action, we should go on GitHub and look in the "/data/common/carousel/" folder and pick the file "BillMurrayMovies.json"

This file looks as follows: 

```
{
  "Id": "BillMurrayMovies",
  "Elements": [
    {
      "Title": {
        "en": "GroundHog Day",
        "fr": "Un jour sans fin",
        "es": "GroundHog Day"
      },
      "Subtitle": {
        "en": "1993",
        "fr": "1993",
        "es": "1993"
      },
      "Image": {
        "Source": "Web",
        "Path": "http://www.filmmattersmagazine.com/wp-content/uploads/2017/11/batten-1.jpg"
      }
    },
    {
      "Title": {
        "en": "Lost in translation",
        "fr": "Lost in translation",
        "es": "Lost in translation"
      },
      "Subtitle": {
        "en": "2003",
        "fr": "2003",
        "es": "2003"
      },
      "Image": {
        "Source": "Web",
        "Path": "http://wwwcdn.scriptmag.com/wp-content/uploads/o-LOST-IN-TRANSLATION-facebook.jpg"
      }
    },
    {
      "Title": {
        "en": "Ghostbusters",
        "fr": "SOS Fantomes",
        "es": "Ghostbusters"
      },
      "Subtitle": {
        "en": "1984",
        "fr": "1984",
        "es": "1984"
      },
      "Image": {
        "Source": "Web",
        "Path": "http://metrograph.com/uploads/films/Ghostbusters_FOR_BOOK-1486579951-726x388.jpg"
      }
    },
    {
      "Title": {
        "en": "Life acquatics with Steve Zissou",
        "fr": "La vie aquatique",
        "es": "Life acquatics with Steve Zissou"
      },
      "Subtitle": {
        "en": "2004",
        "fr": "2004",
        "es": "2004"
      },
      "Image": {
        "Source": "Web",
        "Path": "http://ourgoldenage.com.au/wp-content/uploads/2014/12/GAC_LifeAquatic.jpg"
      }
    }
  ]
}
```

The `"Elements"` contains an array of cards. These are the cards we should print and show to the user.

When we get this sequence from Github, we should:
- show the picture that's defined in the property "Image"
- fill the "Title" field with the text written under the property "Title" that we receive. We usually limit the title to 80 characters. If the text you receive is longer than 80 characters, then we should show 77 characters and 3 dots (...)
- fill the "Subtitle" field with the written under the property "Subtitle" that we receive. Again, we usually limit the subtitle to 80 characters. If the text you receive is longer than 80 characters, then we should show 77 characters and 3 dots (...)

**Please note: the `Subtitle` field is not mandatory, whereas `Title` and `Image` are.**


