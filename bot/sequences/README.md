
Sequences
=================

Sequences are sets of conversational steps between a user and a bot. 
Those steps can consist in asking a question or providing an answer. 
This guide explains how to implement sequences stored in folder gw-config-apis/bot/sequences [mettre le lien]

## Scenario

In this sequence, as a user:
* The bot asksme a question ("A little comfort?")
* The bot suggests answers as a set of commands ("Yes" or "No")
* I pick Yes
* The bot suggests two message categories (Poems and positive thoughts)
* I pick one
* The bot displays a random message from the message categorie (also called an Intention) that I picked

## Asking a question

An sequence contains Actions. 
The most common Action is "ShowQuestion"  

The following json sample describes a question asked by the bot, with its translations

     "Action": "ShowQuestion",
     "Label": [
              { "en": "A little comfort?" },
              { "fr": "Un peu de récomfort ?" },
              { "es": "¿Un pequeño zen?" }
              ],
     
Each question has a QuestionId, so we need to add a QuestionId attribute:
     "Action": "ShowQuestion",
     "QuestionId": "WouldYouLikeToFeelZen",
The json becomes: 

     "Action": "ShowQuestion",
     "QuestionId": "WouldYouLikeToFeelZen",
     "Label": [
              { "en": "A little comfort?" },
              { "fr": "Un peu de récomfort ?" },
              { "es": "¿Un pequeño zen?" }
              ],
     
## Displaying command buttons below the question

To answer a question, users typically choose from a set of commands. 
Each command has a unique CommandId. 
A set of commands  can be described as follow:
    
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
      
## Displaying a second set of commands

As a user, after I choose Yes, I get to choose a message categorie.
This time I don't see a question, just a set of commands. 
In that case instead of "ShowQuestion", the Action for this step is "ShowCommands"

In our example, one command will lead to a poem message, the other to a positive thoughts message.

     "CommandId": "ZenYes",
     "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
     "Action": "ShowCommands",
     "Commands":         
          [
              { 
             "CommandId": "PoemSuggestions",
              }
              ,
              { 
              "CommandId": "PositiveThoughtsSuggestions",
              }
          ]

## Displaying a message card 

A message card is made of a text + an image.

The Action used to show a message card is "ShowCard"

To display a message card, you need to specify the source of the message.
One way of doing that is to specify a message "Intention" : this is a message categorie such as "GoodMorning", "ThankYou", "Positive thoughts". We could also specify a message Area, which is a group of Intentions.

This is how we offer useres to choose betweeen the Intention "Poems" (it's Intention Id is "43B296") and the Intention "Positive thougts" (it's Intention Id is "67CC40") 

       Commands:         
         [
             { 
             "CommandId": "PoemSuggestions", 
             "Action": "ShowCards",
             "TargetType": "Intention", 
             "TargetId": "43B296"
             }
             ,
             { 
             "CommandId": "PositiveThoughtsSuggestions",
             "Action": "ShowCards",  
             "TargetType": "Intention", 
             "TargetId": "67CC40" 
             }
         ]

In this example 67CC40 is the code name for Tntention: positive-thoughts (which contains a library of messages that express positive thoughts), and TargetDescription contains a human readable description of what we want to say.


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




