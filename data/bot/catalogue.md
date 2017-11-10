In this file, we will list all the properties that we use in the sequencing of our chatbots and all the most common values that they can take. 
This document has to be read in understand and parallel to the main documentation: https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/readme.md

##### Note for the reader:
- we list below the properties in alphabetical order
- we will describe each property's behaviour
- under each property, their most common values will appear as a dependency
- we will describe each value's behaviour
- finally, we will make an example of how the property/value pair should work in a typical context


### "Actions"

#### "CreateAnimatedEmoji

#### "DisableSingleMode"

#### "EmbedLink"

#### "EnableSingleMode" 

#### "MessagesByRecipient"

#### "RedirectTo"

#### "RateUs"

#### "SetReminder"

#### "ShowAdvert"

#### "ShowCards"

#### "ShowDailyIdeas"

#### "ShowImages"

#### "ShowTrendingGif"

#### "ShowTrendingMessages"

#### "ShowOtherCommands"

#### "ShowUsers"

### "CommandLabel"

### "Commands"

### "ElementValue"

### "Id"

### "isCorrect"

### "Label"

### "LinksTo"

### "Name"

### "Options"

### "Parameters"

### "Path"

### "Randomize"

### "Steps"

### "Skippeable"

### "Source"

### "Title"

### "Type"

#### "AnimatedGif"

#### "Image"

###### 1. Definition

When the action `"PlaySound"` is triggered, the client will play a sound that is defined by some parameters:
- its source
- the path that leads to this source

###### 2. Client integration

The client may have to integrate an external service to play sounds from some sources (example: FreeSound).

###### 3. Within a sequence

The `"PlaySound"` action needs to be written inside a Step, within a hash of `"Type": "Action"`. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

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


#### "Sound"

###### 1. Definition

`"Sound"` is one of the four main media type that we can add in sequences (the others being: Image, Video, AnimatedGif).

When the property/value pair `"Type: "Sound"` is triggered, the client will play a sound that is defined by some parameters. We will see:
- its source
- a title for each language
- the path that leads to the specific track we want

###### 2. Client integration

The client may have to integrate an external service to play sounds from some sources (example: FreeSound).

###### 3. Within a sequence

The `"Sound"` action needs to be written inside a Step, in link with a `"Type"` property. It can be integrated anywhere in the `"Steps"` (first or last position - it does not matter).

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


#### "Video"



