# "Actions"

Actions trigger the use of external resources or services in or outside the bot. We will here list the most popular actions and the way they can be used.

_______________________
## "EmbedLink"

#### 1. Definition

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "EmbedLink"` in a Step, then the client will embed a link in whatever label is inserted. When the user will click on this label, he/she will be redirected to the web page/app 
The Create Animated Emoji feature invites the user to create a small gif composed of a suite of gifs.

#### 2. Client integration

No pre-requirements.

#### 3. Within a sequence

The `"EmbedLink"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
###### **VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, this `"CreateAnimatedEmoji"` action has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user 

The writer will need to provide:
- a link
- a label, with localized versions if necessary

##### 4. Example
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

_______________________
## "RedirectTo"

#### 1. Definition

`"RedirectTo"` is an action specific to the Android and iOS clients for the WaveMining apps. It allows the writer the client to redirect the user to another part of the app. 

The writer will need to provide `"Paramaters"`: namely
- a `"Type"` to identify in which part of the app we want the redirection to happen (Feature, Tab or Content category)
- a `"Path"` to locate exactly where we want to send the user within this object

There will be five possible `"Type"`:
- `"Feature"`. Its `"Path"` values will include (but not limited to): "Recipients", "CreateAnimatedEmoji", "StickerPals", "TrendingMessages",...
- `"Tab"`. Its `"Path"` values will include (but not limited to): "HelloHuggy", "SurveyBot", "Pairs", "UsefulMessages", "Recipients", "TrendingGifs", "Emoji", "DailyIdeas", "IThinkOfYou"...
- categories of content: `"GifCategory"`, `"ThemeCategory"` or `"TextCategory"`. Their `"Path"` values will be the URL of the content page. Example: "/data/common/giphycontent/hungry.json" in the case of a `"GifCategory"` or "/themes/penguins/small" in the case of a `"ThemeCategory"`

BEWARE: the difference between Feature and Tab can sometimes be very fine. One tab can be comprised of only one feature. Make surer the client knows explicitely what you are refering to.

As a rule, when I am redirected to a Feature, I should be able to complete the user journey in this feature and then land back on the bot. 
On the contrary, when I am redirected to a tab and I complete the user journey through this tab, I will land back in this tab.


#### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers, as they involve a redirection to part of the apps.

#### 3. Within a sequence

The `"RedirectTo"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
###### **VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, this `"RedirectTo"` action has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user 

#### 4. Examples

Example 1 with a Tab redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "Tab",
                  "Path": "UsefulMessages"
            }
        ]

Example 2 with a Feature redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "Feature",
                  "Path": "CreateAnimatedEmoji"
            }
        ]


Example 3 with a Content redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "ThemeCategory",
                  "Path": "/themes/emoticons/small"
            }
        ]


Example 4 with a Content redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "GifCategory",
                  "Path": "/data/common/giphycontent/bored.json"
            }
        ]

Example 5 with a Tab redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "Tab",
                  "Path": "Recipients"
            }
        ]

Example 6 with a Tab redirection:

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "RedirectTo"
              "Parameters":
                  "Type": "Feature",
                  "Path": "MessageByRecipients"
            }
        ]

_______________________
## "SetReminder"

#### 1. Definition

`"SetReminder"` is an action specific to the Android and iOS clients for the WaveMining apps.
When the property/value pair `"Type: "Action"` is triggered along the `"Name: "SetReminder"` in a Step, then the client will redirect the User to the SetReminder feature.
The Set Reminder feature invites the user to set a reminder to send a message to someone. 

#### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers.

#### 3. Within a sequence

The `"SetReminder"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
- this action will temporarily redirect the user to an action performed outside the bot ; however, once the reminder is set, the conversation will continue its natural flow
- as the client reads the steps from top to bottom, this `"SetReminder"` action should preferably be inserted towards the end of the hash of the step. Otherwise, the content appearing on the user's chatbot feed will only be visible once he/she is done with setting the reminder

#### 4. Example

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "SetReminder"
            }
        ]


_______________________
## "ShowAdvert"

#### 1. Definition

We may sometimes want to show an advert to the user, whether during a long sequence, just randomly or as a way to unlock further bot features. We will then use the action `"ShowAdvert"`.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "ShowAdvert"` in a Step, then the client will show an advert to the user. This advertisement should have the following default parameters:
- it should be an interstitial
- it should use the same ad ID and ad provider as the main interstitial placement of the app

#### 2. Client integration

Only the iOS and Android clients can parse this value.

#### 3. Within a sequence

The `"ShowAdvert"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
- this action will temporarily redirect to a full screen advert ; however, once the user has seen and/or dismissed the ad, the conversation will carry its natural flow
- as the client reads the steps from top to bottom, this `"ShowAdvert"` action should preferably be inserted towards the end of the hash of the step. Otherwise, the content appearing on the user's chatbot feed will only be visible once he/she is done with setting the reminder

IMPORTANT: we should preferably ask the user's consent before showing an ad, or at least announce it in the sequence.

#### 4. Example

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "ShowAdvert",
              "Parameters":
                  "Provider": "Facebook",
                  "ID": "1727516507493157_1972147769696695"
            }
        ]

_______________________
## "ShowCards"

#### 1. Definition

The client will sometimes want to show a card of content to the user. To do that, we can use the action `"ShowCards"` to display a card made of some text, an image, a gif or text + image. 

When using the action `"ShowCards"`, the writer should specify (in the `"Parameters"`):
- the `"Type"` of content. We have four choices here
    a. It can be `"TextImage"`, i.e. a combination of text + image
    b. It can be `"Text"` alone if we want a card with a text only
    c. It can be `"Image"` alone if we want a card with an image only
    d. It can be `"Gif"` alone if we want a card with a gif only
- the `"Id"` of the `"Intention"` or `"Theme"` from which we want the image or text, or both

#### 2. Client integration

The client will need to use WaveMining's relevant APIs to use this feature.

#### 3. Within a sequence

The `"ShowCards"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

#### 4. Example

Example 1 - calling for a card made of text + image

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowCards",
              "Parameters":
                  "Type": "TextImage",
                  "Id": "43B296"
            }
        ]

Example 2 - calling for a card made of text only

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowCards",
              "Parameters":
                  "Type": "Text",
                  "Id": "43B296"
            }
        ]


Example 3 - calling for a card made of 1 image only

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowCards",
              "Parameters":
                  "Type": "Image",
                  "Id": "themes/kittens"
            }
        ]

Example 4 - calling for a card made of 1 GIF only

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowCards",
              "Parameters":
                  "Type": "Gif",
                  "Id": "/data/common/giphycontent/animals.json"
            }
        ]


_______________________
## "ShowCardsWithMenu"

#### 1. Definition

The action `"ShowCardsWithMenu"` works exactly as `"ShowCards"`, except for one very important difference: the client will show a predefined menu after the card is displayed
- in this case, the menu has four options: "Send", "One More", "Menu", "Talk to me"
- as a consequence, the sequence should be finished after we use this action
- since the sequence opens up another menu, the master file will be exited until the user selects "Talk to me" or returns to the bot after exiting the app 

**Please refer to the section on `"ShowCards"` - definition** if you want to understand the basic logic of this action

#### 2. Client integration

The client will need to use WaveMining's relevant APIs to use this feature.

#### 3. Within a sequence

The `"ShowCardsWithMenu"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
####### **VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom,  `"ShowCardsWithMenu"` step has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user.

#### 4. Example

    ...
      "Steps": [
            {
            ...
            },
            {
              "Type": "Action",
              "Name": "ShowCardsWithMenu",
              "Parameters":
                  "Type": "TextImage",
                  "Id": "43B296"
            }
        ]

_______________________
## "ShowTrendingContent"

#### 1. Definition

We may sometimes want to show a most popular content from the database. To do that, we will use the action `"ShowTrendingContent"`.

When using this `"ShowTrendingContent"`, the writer should specify the type of content he/she wants. 
We have three choices of `"Type"`:
    a. It can be `"TextImage"`, i.e. a combination of text + image
    b. It can be `"Text"` alone if we want a card with a text only
    c. It can be `"Gif"` alone if we want a card with a gif only

#### 2. Client integration

The client will need to use WaveMining's relevant APIs to use this feature.

#### 3. Within a sequence

The `"ShowCards"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 

#### 4. Examples

Example 1 - calling for a card made of text + image

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowTrendingContent",
              "Parameters":
                  "Type": "TextImage",

            }
        ]

Example 2 - calling for a card made of text only

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowTrendingContent",
              "Parameters":
                  "Type": "Text",
            }
        ]

Example 3 - calling for a card made of 1 image only

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "ShowTrendingContent",
              "Parameters":
                  "Type": "Gif",
            }
        ]


_______________________
## "ShowTrendingContentWithMenu"

#### 1. Definition

The action `"ShowTrendingContentWithMenu"` works exactly as `"ShowTrendingContent"`, except for one very important difference: the client will show a predefined menu after the card is displayed
- in this case, the menu has four options: "Send", "One More", "Menu", "Talk to me"
- as a consequence, the sequence should be finished after we use this action
- since the sequence opens up another menu, the master file will be exited until the user selects "Talk to me" or returns to the bot after exiting the app 

**Please refer to the section on `"ShowTrendingContent"` - definition** if you want to understand the basic logic of this action

#### 2. Client integration

The client will need to use WaveMining's relevant APIs to use this feature.

#### 3. Within a sequence

The `"ShowTrendingContentWithMenu"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
####### **VERY IMPORTANT**: 
- since it redirects to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, `"ShowTrendingContentWithMenu"` step has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user.

#### 4. Example

    ...
      "Steps": [
            {
            ...
            },
            {
              "Type": "Action",
              "Name": "ShowTrendingContentWithMenu",
              "Parameters":
                  "Type": "TextImage",
            }
        ]

_______________________
## "ShowUsers"

#### 1. Definition

`"ShowUsers"` is an action specific to the Android and iOS clients for the WaveMining apps.
When the property/value pair `"Type: "Action"` is triggered along the `"Name: "ShowUsers"` in a Step, then the client will show another user's profile in the chatbot feed and the conversation.

#### 2. Client integration

Only the iOS and Android clients can parse this value and integrate the feature it triggers.

#### 3. Within a sequence

The `"ShowUsers"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. 
- this action will offer some further hardcoded commands to the user ("Preview", "Another" and "Menu")
- as the user will potentially click on them and he/she will be redirected to an action performed outside the bot, it can only be contained in a `"Type": "Leaf"` element.
- as the client reads the steps from top to bottom, this `"ShowUsers"` action has to be the last hash of the step. Otherwise, the content appearing after will not be shown to the user 

#### 4. Example
    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "ShowUsers"
            }
        ]



