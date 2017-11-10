In this file, we will list all the properties that take a set of predefined values and for which the client and the writer need to be cautious in using,

that that we use in the sequencing of our chatbots and all the most common values that they can take. 
This document has to be read in understand and parallel to the main documentation: https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/readme.md

### Note for the reader:
- we list most of the properties in alphabetical order
- we will describe each property's behaviour
- under each property, their most common values will appear in alphabetical order
- we will describe each value's behaviour
- finally, we will make an example of how the property/value pair should work in a typical context

Beware: we do not list properties that take **unique or undefined values** !
Examples: "Id", "Title", "Command", "CommandLabel", "Name", "Steps", "Paths", "ElementValue", "Randomize"

These values' principles are described and examplified in the main documentation.

________________________________________________
## "Actions"


### "CreateAnimatedEmoji

###### 1. Definition

`"CreateAnimatedEmoji"` is an action specific to the Android and iOS clients for the WaveMining apps.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "CreateAnimatedEmoji"` in a Step, then the client will redirect the User to the Create Animated Emoji feature.

The Create Animated Emoji feature invites the user to create a small gif composed of a suite of gifs.

###### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers.

###### 3. Within a sequence

The `"CreateAnimatedEmoji"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

**VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, this `"CreateAnimatedEmoji"` action has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user 

###### 4. Example

    ...
      "Steps": [
        {
        ...
        },
        {
          "Type": "Action",
          "Name": "CreateAnimatedEmoji"
        },

### "DisableSingleMode"


### "EmbedLink"


### "EnableSingleMode" 


### "MessagesByRecipient"


### "RedirectTo"


### "RateUs"


### "SetReminder"


### "ShowAdvert"


### "ShowCards"


### "ShowDailyIdeas"


### "ShowImages"


### "ShowTrendingGif"


### "ShowTrendingMessages"


### "ShowOtherCommands"


### "ShowUsers"



________________________________________________
## "Parameters"

________________________________________________
## "Skippeable"

________________________________________________
## "Source"

________________________________________________
## "Type"


### "AnimatedGif"

`"AnimatedGif"` is one of the four main media type that we can add in sequences (the others being: Sound, Video, Image).

When the property/value pair `"Type: "AnimatedGif"` is triggered, the client will show an image that is defined by some parameters. We will see:
- its source
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to use Giphy's API in order to call and display the GIFs from its library. Its library is the most complete and the only one used thus far, except for internal Gifs in exceptional circumstances.

###### 3. Within a sequence

The `"AnimatedGif"` media needs to be inserted inside a Step, in link with a `"Type"` property. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

###### 4. Example

    ...
      "Steps": [
        {
          "Type": "AnimatedGif",
          "Parameters": {
            "Source": "Giphy",
            "Path": "RHdoPmZgiJlzW"
          }
        },
     ...


### "Image"

###### 1. Definition

`"Image"` is one of the four main media type that we can add in sequences (the others being: Sound, Video, AnimatedGif).

When the property/value pair `"Type: "Image"` is triggered, the client will show an image that is defined by some parameters. We will see:
- its source
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to reconstruct URLs coming from WaveMining's internal database when the source is turned on `"Internal"`

###### 3. Within a sequence

The `"Image"` media needs to be inserted inside a Step, in link with a `"Type"` property. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

###### 4. Example

    ...
      "Steps": [
        {
          "Type": "Image",
          "Parameters": {
            "Source": "Web",
            "Path": "https://assets3.thrillist.com/v1/image/2508887/size/tmg-article_tall.jpg"
          }
        },
     ...


### "Sound"

###### 1. Definition

`"Sound"` is one of the four main media type that we can add in sequences (the others being: Image, Video, AnimatedGif).

When the property/value pair `"Type: "Sound"` is triggered, the client will play a sound that is defined by some parameters. We will see:
- its source
- a title for each language
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to integrate an external service to play sounds from some sources (example: FreeSound).

###### 3. Within a sequence

The `"Sound"` media needs to be inserted inside a Step, in link with a `"Type"` property. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

###### 4. Example

    ...
      "Steps": [
        {
           "Type": "Sound",
           "Parameters": {
             "Title": {
               "en": "Owl at night",
               "fr": "Un hiboux la nuit",
             },
             "Source": "FreeSound",
             "Path": "98941"
           }
         }
     ...


### "Video"

###### 1. Definition

`"Video"` is one of the four main media type that we can add in sequences (the others being: Image, Sound, AnimatedGif).

When the property/value pair `"Type: "Video"` is triggered, the client will play a sound that is defined by some parameters. We will see:
- its source
- a title (common for all languages)
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to:
- integrate a video player inside the bot to have it played in its environment.
- use an external website service to grab the video's thumbail image. For Youtube videos (most common videos), one can get a thumbnail by inserted the end URL of the video in this link (replace the "XXX" with the end URL of the video): https://i.ytimg.com/vi/XXX/hqdefault.jpg

###### 3. Within a sequence

The `"Video"` media needs to be inserted inside a Step, in link with a `"Type"` property. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

###### 4. Example

    ...
      "Steps": [
         {
            "Type": "Video",
            "Parameters": {
              "Title": "Labrador Facts",
              "Source": "Youtube",
              "Path": "6zTOINeXQcc"
            }
          }
     ...



