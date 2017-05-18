
Sequences
=================

- A sequence is a set of conversational steps between a user and a bot
- Those steps typically consist in asking a question or providing an answer
- This guide walks you through an example
- Sequence files can be found here: https://github.com/GhostWording/gw-config-apis/tree/master/bot/sequences

## Scenario

In this sequence, as a user:
* The bot asks me a question ("A little comfort?")
* The bot suggests answers as a set of choices ("Yes" or "No"), either randomized or not
* I choose Yes
* The bot suggests two message categories ("Poems" and "Positive thoughts")
* I choose one those message categories (we call them message Intentions)
* The bot displays a random message from the selected Intention

## Asking a question

Question steps are identified by the presence of a "QuestionId"

The following json sample describes a question asked by the bot, with its translations

     "QuestionId": "WouldYouLikeToFeelZen",
     "Label": [
              { "en": "A little comfort?" },
              { "fr": "Un peu de récomfort ?" },
              { "es": "¿Un pequeño zen?" }
              ]
     
## Displaying choices buttons below the question

As a user, to answer a question, I  typically choose from a set of choices. 
Each choice has a unique Id. 
    
    "Choices": 
      [
          {    
            "Id": "ZenYes",
            "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
           }
           ,
           {
            "Id": "ZenNo",
            "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
           }
      ]      

## Randomizing choices

I may sometimes want to randomize the choices I offer to my users, so that they do not always see the same choices first and choose them by default.

We can do so by introducing the key "Randomize", just before the "Choices". We need to turn it to "true". In our example, that would mean:

    "Randomize": "true",
    "Choices": 
      [
          {    
            "Id": "ZenYes",
            "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
           }
           ,
           {
            "Id": "ZenNo",
            "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
           }
      ]    

In this case, users will see Yes or No first, and the other choice in second, depending on the randomization.

## Displaying a second set of choices

After the user makes a choice, an Action will take place. The Action will be described below the choice.

For instance, if I were to choose "yes" to the previous question, the sequence could then be:

    Choices: 
      [
          {    
            "Id": "ZenYes",
            "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
            "Action": "ShowChoices",
           }
           ,
           ...
           
- In this case, we want to display a second set of choices, 
- There is no question, just a set of choices offered directly to the user as a consequence of the first choice

In our example, one choice will lead to a poem message, the other will lead to a positive thoughts message.

     "Choices":
     [
          {
          "Id": "ZenYes",
          "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
          "Action": "ShowChoices",
          "Choices":         
          [
              { 
             "Id": "PoemSuggestions",
              }
              ,
              { 
              "Id": "PositiveThoughtsSuggestions",
              }
          ]
          }
          ,
          ...

## Displaying a message card 

We now want to show a message of type "Poems" or "Positive thoughts"
- To make messages more enticing we can display them along with an image
- We can do that with message cards
- A message card is made of a text + an image.
- The Action used to show a message card is "ShowCard"

To display a message card, we need to specify the source of the text using the key "CardSource"
- One way to fill "CardSource" is to specify a message "Intention", its "Id" and its "Description"
- An Intention is a message category such as "GoodMorning", "ThankYou" or "Positive thoughts". 
- We need to know the relevant Intention Id.
- Alternativaly, we could specify other sources, such as message Areas, which are groups of Intentions, or message recommandations, which take into account the user personality traits, past app usage or current context.

In this example, as a user, I can choose betweeen Intention "Poems" (Intention Id "43B296") and Intention "Positive thougts" (Intention Id "67CC40")

       "Choices":         
         [
             { 
             "Id": "PoemSuggestions", 
             "Action": "ShowCards",
             "CardSource": {
               "Type": "Intention",
               "Id": "43B296",
               "Description": "poems"
               }
             }
             ,
             { 
             "Id": "PositiveThoughtsSuggestions",
             "Action": "ShowCards",
             "CardSource": {
               "Type": "Intention",
               "Id": "67CC40",
               "Description": "positive-thoughts"
               }
             }
         ]

## When there is no action

As a user, if I choose "No" to the first question, I will de facto terminate the sequence.
The Action for that is "Nothing". 

     "Action": "Nothing"
     "CommandId": "ZenNo",
     "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],

Eventually, it should redirect the user to a generic menu.

## Optional content

When describing a sequence step, we can specify an extra piece of optional content that will be displayed above the sequence question and/or the sequence command set. We name this piece of optional content an Optional Feedback.
Here is an example which can provide fun Optional Feedback when the ZenYes command has been chosen: 

    "Id": "ZenYes",
    "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
    "OptionalFeedback": {
         "Type": "AnimatedGif",
         "Source": "Giphy",
         "Path": "oXV6IEt10fvIQ"
     },
     "Action": "ShowChoices",
     ...
         
When users clicks "Yes", before displaying further choices, the bot will display:
- an animated gif (Type -> defines the type of the Media)
- pulled from the website Giphy (Source -> defines the source of the content)
- using id oXV6IEt10fvIQ (Path -> define the path to access the content)
- In this case, we assume the client is using Giphy's API to call for gifs via URL code
- If the Source was "Web", Path would contain a full url. If Source was "Internal", Path would contain the path of one of our own images.

## Assessing the value of each step

For analytical purposes, you may want to distinguish the steps that are the most valuable to your user from the steps that are not valuable to the user.
To do so, you can add the field "StepValue":
- turn it to "1" if you think this brings value to your user
- turn it to "0" if you do not think this step is valuable to your user

In our example, we think we asked a valuable question to the user if he or she answers yes - alternatively, if the answer is "no", we deem the question was not of any value to him or her. Therefore:

     "Choices":
     [
          {
         "Id": "ZenYes",
         "StepValue": "1",
         "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }]
         }
         ,
         {
         "Id": "ZenNo",
         "StepValue": "0",
         "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }]
         }
     ]

## Putting it all together

     [
          {
          "QuestionId": "WouldYouLikeToFeelZen",
          "Label": [
               { "en": "A little comfort?" },
               { "fr": "Un peu de récomfort ?" },
               { "es": "¿Un pequeño zen?" }
           ]
          "Randomize": "true",
          "Choices": 
             [
                 {
                 "Id": "ZenYes",
                 "StepValue": "1",
                 "Label": [{ "en": "Yes" },{ "fr": "Oui" },{ "es": "Si" }],
                 "OptionalFeeback": {
                      "Type": "AnimatedGif",
                      "Source": "Giphy",
                      "Path": "oXV6IEt10fvIQ"
                      },
                 "Action": "ShowChoices",
                 "Choices": 
                     [
                         {
                         "Action": "ShowCards",
                         "StepValue": "1",
                         "Id": "PoemSuggestions",
                         "CardSource": {
                              "Type": "Intention",
                              "Id": "43B296",
                              "Description": "positive-thoughts"
                              }
                         }
                         ,
                         {
                         "Action": "ShowCards",
                         "StepValue": "1",
                         "Id": "PositiveThoughtsSuggestions",
                         "CardSource": {
                              "Type": "Intention",
                              "Id": "67CC40",
                              "Description": "positive-thoughts"
                              }
                         }
                     ]
                 }
                 ,
                 {
                 "Id": "ZenNo",
                 "StepValue": "0",
                 "Label": [{ "en": "No" },{ "fr": "Non" },{ "es": "No" }],
                 "Action": "Nothing"
                 }
             ]
     }
     ]
