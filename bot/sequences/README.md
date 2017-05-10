
Sequences
=================

- A sequence is a set of conversational steps between a user and a bot
- Those steps typically consist in asking a question or providing an answer
- This guide walks you through an example
- Sequence files can be found here: https://github.com/GhostWording/gw-config-apis/tree/master/bot/sequences

## Scenario

In this sequence, as a user:
* The bot asks me a question ("A little comfort?")
* The bot suggests answers as a set of commands ("Yes" or "No")
* I pick Yes
* The bot suggests two message categories ("Poems" and "Positive thoughts")
* I pick one those message categories (we call them message Intentions)
* The bot displays a random message from the selected Intention

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

As a user, to answer a question, I  typically choose from a set of commands. 
Each command has a unique CommandId. 
    
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
This time, I don't see a question, just a set of commands. 
In that case instead of "ShowQuestion", the Action for this step is "ShowCommands"

In our example, one command will lead to a poem message, the other will lead to a positive thoughts message.

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

We now want to show a poem or a positive thoughts message as a message card. 
- A message card is made of a text + an image.
- The Action used to show a message card is "ShowCard"

To display a message card, you need to specify the source of the message.
- One way of specifying the source of a message is to specify a message "Intention". 
- An intention is a message categorie such as "GoodMorning", "ThankYou" or "Positive thoughts". 
- We need to know the relevant Intention Id.
- Alternativaly, we could specify other sources, such as message Areas, which are groups of Intentions, or message recommandations, which take into account the user personality traits, past app usage or current context.

In this example, as a user, I can choose betweeen the Intention "Poems" (Intention Id "43B296") and the Intention "Positive thougts" (Intention Id is "67CC40") 

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

## Exiting the sequence

As a user, if I choose "No" to the first question, I will terminate the sequence.
The Action for that is "Exit". 

     "CommandId": "ZenNo",
     "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
     "Action": "Exit"

## Optional content

When describing a sequence step, to make it more enticing, we can specify an extra piece of content that will be displayed above the sequence question and/or the sequence command set. 
Here is an example in the context of the ZenYes command: 

    "CommandId": "ZenYes",
    "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
    "OptionalMediaType": "AnimatedGif",
    "OptionalMediaSource": "Giphy",
    "OptionalMediaPath": "oXV6IEt10fvIQ",
         
When users clicks "Yes", before displaying further commands the bot will display:
- an animated gif (OptionalMediaType -> defines the type of the Media)
- pulled from website Giphy (OptionalMediaSource -> defines the source of the content)
- using id oXV6IEt10fvIQ (OptionalMediaPath -> define the path to access the content)
If OptionalMediaSource was "Web", OptionalMediaPath would contain a full url.
If OptionalMediaSource was "Internal", OptionalMediaPath would contain the path of one of our own images.

## Putting it all together

     [
     {
          "Action": "ShowQuestion",
          "QuestionId": "WouldYouLikeToFeelZen",
          "Label": [{ "en": "A little comfort?" },{ "fr": "Un peu de récomfort ?" },{ "es": "¿Un pequeño zen?" }],
          "Commands": 
             [
                 {
                 "CommandId": "ZenYes",
                 "OptionalMediaType": "AnimatedGif",
                 "OptionalMediaSource": "Giphy",
                 "OptionalMediaPath": "oXV6IEt10fvIQ",
                 "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
                 "Action": "ShowCommands",
                 "Commands": 
                     [
                         { 
                         "Action": "ShowCards",
                         "CommandId": "PoemSuggestions", 
                         "TargetType": "Intention", 
                         "TargetId": "43B296"
                         }
                         ,
                         { 
                         "Action": "ShowCards",  
                         "CommandId": "PositiveThoughtsSuggestions",
                         "TargetType": "Intention", 
                         "TargetId": "67CC40"
                         }
                     ]
                 }
                 ,
                 {
                 "CommandId": "ZenNo",
                 "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
                 "Action": "Exit"
                 }
             ]
     }
     ]

