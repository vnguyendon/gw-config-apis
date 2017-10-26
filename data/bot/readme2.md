In this file, we will describe the new format for the chatbot sequences. This format was written in October 2017 and aims at broadening the scope of possible actions in the chatbot, whilst simplying the format and making more flexible


## The skeleton of a sequence

The structure of a sequence is similar to that of a tree: each level is either made of nodes ("`Node`") or leaves ("`Leaf`"). A `node` needs to be followed by something ; a `leaf` is an end to the sequence. 

Example:

     {
     "Type": "Node",
     "Command": {
        "Type": "Leaf"
          },
          {
        "Type": "Node",
        "LinksTo": {
          "Type": "Leaf"
          }
     }
        

Think of them as binary: each sequence step has to have a `Type` ; each `Type` needs to be either a "`Node`" or "`Leaf`"

`Command` and `LinksTo` are used to transition from one `Type` to another. `Command` points to commands that the user will see on his/her screen. 

Let's use a concrete example to illustrate how we can build a sequence from here

## Scenario

In this following sequence example:
* A chatbot will ask us a question ("Are you often late?")
* The chatbot will suggest answers as a set of choices ("Yes" or "No")
* We can select an answer
* The bot executes an action as a result of this choice

## Asking a question

To start an interaction with a user, we need three ingredients:
1. a `Type` which should be `node` if you want the user to react
2. `Steps` : an array of content defining what you want to send to the user
3. An `Id` for analytical purpose

Example:

     "Type": "Node",
     "Id": "LikePuppies"
     "Steps": [
     {
          "Type": "Image",
          "Id": "10",
          "Source": {
               "Type": "Gif",
               "Source": "Giphy",
               "Path": "FC5aPFWCo7adG"
               }
          }
     },
     {
          "Type": "Text",
          "Id": "20",
          "Label": {
               "en": "Do you like puppies?",
               "fr": "Est-ce que tu aimes les chiots"               }
          }     
     }
     ]

## Showing choices to the user


## Reacting to the user's choice


## Asking another question


## Showing a carousel of choices (picture + text)


## Executing an action


## Randomizing the feedback


## Type: Story

A `"Type" : "story"` is just a special sequence with only one leaf: it's a row of steps.


## Steps

Steps are a series of things to do, `before showing the command buttons` in a node and after the selection of a command within a leaf. 
The principle of the steps is to simplify the former logic by describing a series of things to do after and before an interaction with a user.

Steps don't have a unique Id like a node or a leaf, they are part of it and executed sequentially, that's why we'll use the id as something do identify the step within the steps and to order them. It is recommended to use the ten by ten notation to be able to add a new step in between without renumbering everything (10,20,30...)


## Label vs Steps

The `Label` should disappear.
Within questions, the label is just a special form of steps with only one step. In order to simplify the format of the construct we should keep only the steps and remove the Label. this means that a label in question (with no other steps) will become:
```
     "Steps": [{
       "Type": "Text",
       "Id": "1",
       "Label": {
         "en": "Are you more",
         "fr": "Est-ce que tu préfères",
         "es": "Te gustan mas"
       }
    }]
```
Within commands, the `Label` signification is different, as it refers to the label shown inside a button (not a text with a question). That's why it makes sense to keep it appart the Steps. But in order to clarify things, we'll call it `CommandLabel` :
```
    "CommandLabel": {
        "en": "a cat person 🐱",
        "fr": "les chats 🐱",
        "es": "los gatos 🐱"
      },
```

## Carousel

The Carousel is a special `command` composed of a picture and a short text (we usually display several cards like this in order for the user to choose one, thus the name Carousel).
A carousel can be displayed with a combination of `CommandPicture` and `CommandLabel`:
```
"Type": "Leaf",
      "Id": "MostPopularTextImage1",
      "StepValue": "1",
      "isCorrect": "true",
      "CommandPicture": {
        "Type": "Image",
        "Source": "Internal",
        "Path": "/specialoccasions/I-love-you/default/small/530773_10150757735645255_150787610254_11952370_462269272_n.jpg"
      },
      "CommandLabel": {
        "en": "I want all of you, forever, you and me, everyday.",
        "fr": "Je veux tout de toi, pour toujours, toi et moi, tous les jours.",
        "es": "Quiero todo de ti, para siempre, tu y yo, todos los días."
      }
```
When `CommandPicture` and `CommandLabel` come together, it means we want to display a card, but both of them can be used independently to display a picture or a text as a command.


## Actions

The simplest thing to do, is to add the actions to do as steps. Thus we can ensure the coherence of the order of actions, we are able to show something before the action (showing a gif before showing the survey results for exemple) and because it's not just a string but an object we can add extra parameters:
     
```
    ...
     "Steps": [
        {
            "Type":"Action",
            "Id":"10",
            "Name":"DoVote",
            "Params": {
              "additionalCounter" : "surveyCatsAndDogsCounterWhatever"
            }
        },
        {
          "Type": "Image",
          "Id": "20",
          "Source": {
            "Type": "AnimatedGif",
            "Source": "Giphy",
            "Path": "LKRtMj7xvviUg"
          }
        }, ...
```

## Action: ShowCards

The action `"ShowCards"` displays a card (text+image) from a specific intention or theme of our database.
```
            "Steps": [
              {
                "Type": "Action",
                "Id": "10",
                "Name": "ShowCards",
                "Params": {
                  "Type": "Intention",
                  "Id": "9B2C8B"
                }
              }
            ]
```

## Breaks

Breaks are special steps that introduce a delay between 2 other steps or display a button allowing the user to stop/continue.

```
    ...
      "Steps": [
    {
      "Type": "Break",
      "Id": "40",
      "Mode": "Wait",
      "Parameters": {
        "ms": 4000
      }
    },
    ...
    {
      "Type": "Break",
      "Id": "60",
      "Mode": "Stop"
    },
    ...
```

In the Stop mode, without no other parameters, it's on client responsibility to show the right toolbar (for example "would you like to continue?" + buttons yes/no). This can be defined in bot resources file for exemple.

If we want a specific question/button, we should be able to add them within step's parameters:
```
    {
      "Type": "Break",
      "Id": "20",
      "Mode": "Stop",
      "Label" : {
         "en" : "go on?", "fr": "on continue?", "es":"continuar?"
      },
     "Options": {
        "yes" : {  "en" : "yes", "fr": "oui", "es":"si" },
        "no" : {  "en" : "no", "fr": "non", "es":"no" },
     }
    }
```

## LinksTo

In the new format, the LinksTo is simplified.
It can links to another row of steps, to a node or a leaf.
For example, at the end of a story it can be used to distinguish the story in itself and the few lines/actions that follow the end of the story: 


    "LinksTo": {
        "Id": "StorySherlockGoesCamping",
        "Steps" : [
           { 
               "Type" : "Text",
               "Id" : 10,
               "Label": {
                     "en": "Thanks for reading my story!",
                     "fr": "Merci d'avoir lu mon histoire !",
                     "es": "¡Gracias por leer mi historia!"
                }
          },
          { 
               "Type" : "Action",
               "Id" : 20,
               "Name":"SetUserProperty",
              "Parameters": {
                   "property": "StorySherlockGoesCampingDone",
                   "value": "true"
              }
     }


## Quiz

A quiz is a question (node) with a right and a wrong answer.
In the commands, we use the boolean `"isCorrect": "true"/"false"` to bear the information.
We can also use a specific action to notify the user of the correctness of his response.

```
"Type": "Leaf",
  "Id": "FastestDolphin",
  "StepValue": "1",
  "isCorrect": "false",
  "CommandLabel": {
    "en": "dolphin"
  },
  "Steps": [
   {
      "type":"action",
      "id": "1",
      "name": "notifyCorrectness"
   },
    {
      "Type": "Text",
      "Id": "10",
      "Label": {
        "en": "A blue whale can swim as fast as 50km/h, whereas most dolphins don't reach 40km/h"
      }
    }
  ]
```

Then this `notifyCorrectness` action can take its data from a resource file to introduce some salt in the responses.
If we want a custom thing, we just need to add a step (text, image or both) to tell user he's right.

## Order

If the id of different steps (used for ordering) is the same, we'll randomize the steps with the same order. That means in the previous example, that if id=10 in the notifyCorrectness action and text, we'll randomly show one before the other. That is a solution to introduce more natural conversation in steps (if order is not important obviously).
