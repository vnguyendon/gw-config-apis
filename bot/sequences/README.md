
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
* The bot replies, either by another question, other possible answers or by providing the human with cards
* The chooses a card
* The human exits the conversation



## Asking a question

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
     
## Providing answer options to the user

To answer a question, users typically choose from a set of commands. As with questions, each command has a command Id. It can be described as follows:
    
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
      

Each of these commands needs to be followed by an Action. Apart from ShowQuestion, the threee most common Actions are ShowCommands, ShowCards and Exit.

## Providing new commands to the user

The Action "ShowCommands" means that for a given Command selected, other Command options will be offered to the user.

Here is an example following on "ZenYes" Command:

     "Action": "ShowCommands"
     "CommandId": "ZenYes",
     "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
     "Commands":         
     [
        { 
        "CommandId": "SuggestionsPoems",
        "TargetDescription": "poems"
        }
        ,
        { 
        "CommandId": "SuggestionsPositiveThoughts",
        "TargetDescription": "positive-thoughts" 
        }
     ]

As the user has answered "Yes" to the question "A little comfort?", he/she is offered two new commands: "poems" and "positive thoughts"


## Providing cards to the user

The Action "ShowCards"  picks a random card (made of a text plus an image). We need to know where this message comes from. One way of doing that is by pointing the Intention we wish to express ("GoodMorning", "ThankYou",...), the Area (with a number) or the Theme (theme/dogs).

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


## Exit the conversation

The user can also choose to select the Command "No" to the bot's question, leading the conversation to a dead-end. This sequence needs to be followed by an Action as well.

A common Action for this type of interaction is "Exit". Here is an example:

     "Action": "Exit",
     "CommandId": "ZenNo",
     "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],

The "Exit" Action will redirect the user to the main menu. 

## Reacting to the user's choice

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

## Summary of the sequence

The sequence we just built bit by bit through the examples is here:

     [
     {
          "Action": "ShowQuestion",
          "QuestionId": "WouldYouLikeToFeelZen",
          "Label": [{ "en": "A little comfort?" },{ "fr": "Un peu de récomfort ?" },{ "es": "¿Un pequeño zen?" }],
          "Commands": 
             [
                 {
                 "Action": "ShowCommands",
                 "CommandId": "ZenYes",
                 "OptionalMediaType": "AnimatedGif",
                 "OptionalMediaSource": "Giphy",
                 "OptionalMediaPath": "oXV6IEt10fvIQ",
                 "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
                 "Commands": 
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
                 }
                 ,
                 {
                 "Action": "Exit",
                 "CommandId": "ZenNo",
                 "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }]
                 }
             ]
     }
     ]




