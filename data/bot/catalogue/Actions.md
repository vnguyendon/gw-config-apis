# "Actions"


## "CreateAnimatedEmoji

###### 1. Definition

`"CreateAnimatedEmoji"` is an action specific to the Android and iOS clients for the WaveMining apps.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "CreateAnimatedEmoji"` in a Step, then the client will redirect the User to the Create Animated Emoji feature.

The Create Animated Emoji feature invites the user to create a small gif composed of a suite of gifs.

###### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers.

###### 3. Within a sequence

The `"CreateAnimatedEmoji"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

##### **VERY IMPORTANT**: 
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
            }
        ]


## "EmbedLink"

###### 1. Definition

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "EmbedLink"` in a Step, then the client will embed a link in whatever label is inserted. When the user will click on this label, he/she will be redirected to the web page/app 

The Create Animated Emoji feature invites the user to create a small gif composed of a suite of gifs.

###### 2. Client integration

No pre-requirements.

###### 3. Within a sequence

The `"EmbedLink"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

##### **VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, this `"CreateAnimatedEmoji"` action has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user 

The writer will need to provide:
- a link
- a label, with localized versions if necessary

###### 4. Example

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "EmbedLink",
              "Parameters": {
                   "Path": "https://play.google.com/store/apps/details?id=com.wavemining.emoji.elite&hl=en_GB",
                    "Label": {
                        "en": "Rate us on the Play Store",
                        "fr": "Notez-nous sur le Play Store"
                    }
              }
            }
        ]



## "MessagesByRecipient"


## "RedirectTo"


## "SetReminder"

###### 1. Definition

`"CreateAnimatedEmoji"` is an action specific to the Android and iOS clients for the WaveMining apps.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "CreateAnimatedEmoji"` in a Step, then the client will redirect the User to the Create Animated Emoji feature.

The Create Animated Emoji feature invites the user to create a small gif composed of a suite of gifs.

###### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers.

###### 3. Within a sequence

The `"CreateAnimatedEmoji"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

##### **VERY IMPORTANT**: 
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
            }
        ]



## "ShowAdvert"


## "ShowCards"


## "ShowDailyIdeas"


## "ShowImages"


## "ShowTrendingGif"


## "ShowTrendingMessages"


## "ShowOtherCommands"


## "ShowUsers"


