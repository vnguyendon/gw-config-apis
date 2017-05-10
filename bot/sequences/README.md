
Sequences
=================

The sequences are a set of pre-defined interactions, questions and answers between a user and a bot.
This is a guide to follow for the implementation of the sequences stored in the folder gw-config-apis/bot/sequences.

Our goal is 
- To describe clearly what each key/value pair does and how to implement it
- To provide bits of sequence templates subsequently


## Scenario

We want to describe dialogue sequences between a bot and a human:
* The sequence starts with the bot asking a question
* The human is then offered a selection of possible answers
* The human selects an answer
* The bot replies, either by another questions or by providing the human with cards

### Asking a question

An interaction contains actions.
The most common action is "ShowQuestion"  

The following json sample describes a question asked by the bot, with its translations

     "Action": "ShowQuestion",
     "Label": [
              { "en": "A little comfort?" },
              { "fr": "Un peu de récomfort ?" },
              { "es": "¿Un pequeño zen?" }
              ],
     
Each question has a question Id, so we need to add a QuestionId attribute:
     "Action": "ShowQuestion",
     "QuestionId": "WouldYouLikeToFeelZen",

Adding this key, the json becomes: 

     "Action": "ShowQuestion",
     "QuestionId": "WouldYouLikeToFeelZen",
     "Label": [
              { "en": "A little comfort?" },
              { "fr": "Un peu de récomfort ?" },
              { "es": "¿Un pequeño zen?" }
              ],
     

### Providing answer options to the user

To answer a question, users typically choose from a set of commands. It can be described as follows:
    
    Commands: 
      [
          {    
            "CommandId": "ZenYes",
            "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
           }
           ,
           {
            "CommandId": "ZenNo",
            "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
           }
      ]      
      

### Reacting to the user's choice

When the user interacts with the bot, we want the bot be able to react with a direct pre-defined piece of content. We will call this direct reaction an 'instant feedback'
This instant feedback usually appears after the user makes a choice and before the bot's next message.

Here is an example of this, applied to the ZenYes command: 

    "CommandId": "ZenYes",
    "OptionalMediaType": "AnimatedGif",
    "OptionalMediaSource": "Giphy",
    "OptionalMediaPath": "oXV6IEt10fvIQ",
    "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
         
When the user clicks "Yes" to the question "A little comfort?", the bot should then send:
- an Animated Gif (OptionalMediaType -> defines the type of the Media)
- pulled from the website Giphy (OptionalMediaSource -> defines the source of the content)
- with the URL path: oXV6IEt10fvIQ (OptionalMediaPath -> define the path to access the content)


### Providing cards to the user

Another common action is "ShowCards" which picks a random message (made of a text plus an image). We need to know where this message comes from. One way of doing that is by pointing the Intention we wish to express ("GoodMorning", "ThankYou",...), the Area (with a number) or the Theme (theme/dogs).

Here is an example of two choices of cards: 

       Commands:         
         [
             { 
             "Action": "ShowCards",
             "CommandId": "SuggestionsPoems", 
             "TargetType": "Intention", 
             "TargetId": "43B296", 
             "TargetDescription": "poems" 
             }
             ,
             { 
             "Action": "ShowCards",  
             "CommandId": "SuggestionsPositiveThoughts",
             "TargetType": "Intention", 
             "TargetId": "67CC40", 
             "TargetDescription": "positive-thoughts" 
             }
         ]

In this example 67CC40 is the code name for the intention: positive-thoughts (which contains a library of messages that express positive thoughts), and TargetDescription contains a human readable description of what we want to say.


### Exit the conversation





1. "Action" 

    Principle: Action pairs define what action the block will perform, either on the user side or on the server side
    E.g. "Action": "ShowQuestion" means a question should be asked to the user

    List of "Action" values:
      i. "ShowQuestion" -> means a question will be displayed
      ii. "ShowCommands" -> means a command will be displayed
      iii. "ShowCards" -> means a card will be displayed
      iv. "EnableSingleMode" -> sends an event to the server, describing the user as single
      v. "DisableSingleMode" -> sends an event to the server, describing the user as in couple
      vi. "ShowAdvert" -> calls an ad to be displayed
      vii. "ShowCommandsLater" -> means the user will be provided with three commands:
        - "in 1 hour" =>
        - "in 12 hours" =>
        - "in 1 day" =>
        - "customize" =>
      viii. "Exit" -> means the user exits the sequence and is redirected to the menu commands


2. "Commands" 

    Definition: a command is a call-to-action presented to the user in a bar.
    Principle: "Commands" pairs define the different commands that will be offered to the user.
      - "Commands" keys are usually followed by an array 
      - This array contains from one to infinite numbers of hashes
      - Each one of those hashes provide one command available to the users
    
    Example:   "Commands": [
                  {"Action":"ShowCards"}
                  {"Action":"ShowCommandsLater"}
                  {"Action":"ShowAdvert"}
                ]
    => Following this pair, three commands will be offered to the user.
   

3. "Ids"

    Principle: 
    - Ids are usually attached as a suffixe to "Command" and "Question". 
    - They define the way we call the sequence, both technically (as events on the server) and internally
    - Therefore, the Ids functions in mainly informative: they help identify the bit of sequence and the action it performs


4. "OptionnalMedia"

    Principle:
    - "OptionnalMedia" defines, through its keys/values the immediate feedback the bot should send to the user after an event occurs
    - This event can be a question (defined by QuestionId) or a command that is pressed by the user (defined by CommandId)
    
    
    - There are three "OptionnalMedia" keys. They usually come together:
      - "OptionnalMediaType" defines the type of the media 
            - Examples include: AnimatedGif / Video / Audio / Image / Text
      - "OptionnalMediaSource" defines the source of the media,
            - Examples include: Internal / Web / Giphy / Youtube / Spotify / Deezer
      - "OptionnalMediaPath" defines the path of media:
            - Examples include: Internal path to Dropbox, URL path to the media, simplified URL if it can be read by the client
  

5. "Label"

    Principle: 
    - "Label" defines what the user can see for an event, 
        - The event can be a question, command or optionnal media feedback (in the case of text feeback)
    - "Label" usually delivers content in accordance to the user language (French, English, Spanish most of the time)
    - When "Label" is present within an event, it overrides the other rules for displaying content within that event
    
    - in the absence of a "Label" during an event, content can be delivered according to the following rule:
        - if the event is a "ShowCards" Action that contains an Intention as "TargetType", 
            - then the "TargetDescription" value should provide the content in the three main languages
        - else, we should display the "CommandId" to the user
    
    



II. Templates 



