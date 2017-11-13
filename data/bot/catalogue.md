________________________________________________

________________________________________________
# "Parameters"

________________________________________________
# "Skippeable"

________________________________________________
# "SetUserProperty"


## ppSingle: enabling/disabling Single mode

###### 1. Definition

"EnableSingleMode" and "DisableSingleMode" are the two faces of the same coin: they are properties set when the user either reveals that he is single or not.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "SetUserProperty"`, we can set a property for the user's relationship status. That can then be used to personalize the experience in the app or in the bot.

To set this user property, we need to send the relevant parameters to the server. For that, we will use the `"Property": "ppSingle"` and the `"Value": "yes"` or `"no"`, depending on the answer 

###### 2. Client integration

No pre-requirements.

###### 3. Within a sequence

This `"SetUserProperty"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. It can be inserted anywhere in the `"Steps"` (first or last position - it does not matter).


###### 4. Example

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppSingle",
                   "Value": "Yes"
              }
            }
        ]


## Psychological profile: ppDecisionsAriseFrom / ppEnergeticAfter / ppOpenedOrSettled / ppReceptiveTo

###### 1. Definition

By using a set of 4 sequences, the client can ask the user 4 questions and establish their psychological profile from there.

To do that, we will need to set a property for each of the user's personality traits. Each sequence will be in charge of setting one user property and one of its two possible values. The properties and their possible values are the following:
- ppDecisionsAriseFrom: Feelings / Thinking
- ppEnergeticAfter: OnYourOwn / WithPeople
- ppOpenedOrSettled: Opened / Settled
- ppReceptiveTo: Ideas / Facts

To set these user properties, we need to send the relevant parameters to the server. We will use the pair `"Type: "Action"` triggered along the `"Name: "SetUserProperty"`. The user's psychological profile can then be used to personalize the experience in the app or in the bot.

###### 2. Client integration

No pre-requirements.

###### 3. Within a sequence

This `"SetUserProperty"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. It can be inserted anywhere in the `"Steps"` (first or last position - it does not matter).


###### 4. Example

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppReceptiveTo",
                   "Value": "Ideas"
              }
            }
        ]

Alternatively, if the user had answered `"Facts"`, we would have written: 

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppReceptiveTo",
                   "Value": "Facts"
              }
            }
        ]

________________________________________________
# "Source"

________________________________________________
# "Type" (when not defined as Leaf or Node)


## "AnimatedGif"

`"AnimatedGif"` is one of the four main media type that we can add in sequences (the others being: Sound, Video, Image).

When the property/value pair `"Type: "AnimatedGif"` is triggered, the client will show an image that is defined by some parameters. We will see:
- its source
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to use Giphy's API in order to call and display the GIFs from its library. Its library is the most complete and the only one used thus far, except for internal Gifs in exceptional circumstances.

###### 3. Within a sequence

The `"AnimatedGif"` media needs to be inserted inside a Step, in link with a `"Type"` property. It can be inserted anywhere in the `"Steps"` (first or last position - it does not matter).

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


## "Image"

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


## "Sound"

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


## "Video"

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



