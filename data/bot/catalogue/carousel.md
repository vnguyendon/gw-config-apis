In the chatbots, we would like to display a carousel. **A carousel is a series of specific cards that the user can swipe horizontally**. These cards add a visual aspect to the replies, as they are made of a picture and text; in contrast quick replies are made of text only.


### 1. Carousel user journey

As a user, I see a question. "Which country would you like to visit next?"

Then, I see three cards, displayed horizontally: (1) Nepal, (2) Scotland, (3) Chile. These choices are made of:
###### a. A picture
###### b. A title (example: "Nepal is a remote country built on the edge of the himalayas" in the first card)
###### c. A subtitle (example: "Fancy climbing Mt. Everest or Annapurna?" in the first card)
###### d. A label or button (example: "NEPAL" in the first card)

It looks like the following: 

![Screenshot](/data/bot/catalogue/Examples/Carousel2.PNG?rw=true "Carousel test")

When I click on a card in a carousel (on any part of it - image or button), I trigger the next step in the sequence.


### 2. Carousel json format 

In terms of json format, a carousel is very similar to quick replies.

The only differences are:
-> we will need to add some information (picture, title, subtitle)
-> we will need to tell the client that it has to display a carousel (`"DisplayHint": "Carousel"` - line 44) on the Node level


Example:

```
{
  "Type": "Node",
  "Id": "VisitNext",
  "Steps": [
    {
      "Type": "Text",
      "Label": {
        "en": "Which country would you like to visit next?"
      }
    }
  ],
  "DisplayHint": "Carousel",
  "Commands": [
    {
      "Type": "Leaf",
      "Id": "VisitNextNepal",
      "CommandLabel": {
        "en": "NEPAL"
      },
      "CarouselElements": {
        "Title": {
          "en": "Nepal is a remote country built on the edge of the himalayas"
        },
        "Picture": {
          "Source": "Web",
          "Path": "https://www.welcomenepal.com/imagecache/slider/everest-tkelly.jpeg"
        },
        "Subtitle": {
          "en": "Fancy climbing Mt. Everest or Annapurna?"
        }
      }
    },
    {
      "Type": "Leaf",
      "Id": "VisitNextChile",
      "CommandLabel": {
        "en": "CHILE"
      },
      "CarouselElements": {
        "Title": {
          "en": "Chile is a strip of land locked between the Pacific and the Andes"
        },
        "Picture": {
          "Source": "Web",
          "Path": "http://santiagotimes.cl/en/wp-content/uploads/2017/10/Santiago_Chile.jpg"
        },
        "Subtitle": {
          "en": "Patagonia is a sublime land and definitely worth visiting"
        }
      }
    }
  ]
}
```

The `CarouselElements` property will gather all the carousel-specific properties that the client needs to read in order to build the cards. We group them so as to optimise the visibility of the carousel for the reader and the writer.

#### 3. Carousel specificities

There are some specific rules that must be applied to the carousel. These rules are inherent to the Messenger client and should be respected as much as possible by the provider and the client of the carousel.

##### a. What's required and what's not

- the `Title` is a REQUIRED element of the carousel - if you forget it, it will break on Messenger
- the `Subtitle` is NOT a REQUIRED element of the carousel - you don't have to fill it
- the `Picture` is a REQUIRED element of the carousel - if you forget it, it will break on Messenger (and lose a lot of the appeal of the carousel)
- the `CommandLabel` plays the role of the Button


##### b. Characters limits

- the Button (or `CommandLabel` in the sequence) shouldn'd be longer than 20 characters, spaces included (which is also the limit for Quick replies, by the way)
- the `Subtitle` and `Title` have a character limit of 80 characters, spaces included



