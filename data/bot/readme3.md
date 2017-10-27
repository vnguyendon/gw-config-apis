
In this file, we will describe the new format for the chatbot sequences. This format was written in October 2017 and aims at broadening the scope of possible actions in the chatbot, whilst simplying the format and making more flexible

## 1. Scenario

In this following sequence example:
* A chatbot will ask us a question ("Do you like puppies?")
* The chatbot will suggest answers as a set of choices ("Yes" or "No")
* We will select an answer
* The chatbot will react to our answer and ask a second question
* We will answer it using a carousel
* The bot will execute an action as a result of this choice

## 2. Asking a question

To start an interaction with a user, we need three ingredients:
1. a `Type`: this can either be `node` or a `leaf`. A `node` needs to be followed by something ; a `leaf` is an end to the sequence. If you want to allow the user to react, you need to use a `node` (see Section on The Skeleton of a sequence)
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
               "Path": "RHdoPmZgiJlzW"
               }
          }
     },
     {
          "Type": "Text",
          "Id": "20",
          "Label": {
               "en": "Do you like puppies?",
               "fr": "Est-ce que tu aimes les chiots?"
               }
          }     
     }
     ]
     ...

In this example, we have opened a `node`, defined an `Id` and used `Steps` to fill in the content that we want to show users. In this case, it will first be a `Gif`, followed by `Text` (a question).

**Note**: each `Id` within the `Steps` defines the order of these sequences. In this examples, the `GIF` will be displayed before the `Text` because it has a smaller `Id`

Let's now see how we can offer the user to answer this question.

## 3. Showing choices to the user

To show command choices to the user, we need to use `Command`:
* It store an array of choices that the user can select
* An `Id` will need to be defined for each choice
* `CommandLabel` defines the content of these commands

Example:

     ...
     "Commands": [
     {
          "Type": "node"
          "Id": "LikePuppiesYes"
          "CommandLabel": {
               "en": "yes",
               "fr": "oui"
          }
     },
     {
          "Type": "leaf"
          "Id": "LikePuppiesNo"
          "CommandLabel": {
               "en": "no",
               "fr": "non"
          }
     }
     ]
     
The first command contains the `Type`: `node` - this means it will be followed by something. The second command contains the `Type`: `leaf` - it means the sequence will end here if the user selects this choice.


## 4. Reacting to the user's choice

We will want to react to the user's choice. We can do this simply by adding `Steps` to our command.

Example:

     ... 
    "Commands": [
     {
          "Type": "node"
          "Id": "LikePuppiesYes"
          "CommandLabel": {
               "en": "yes",
               "fr": "oui"
          }
     },
     {
          "Type": "leaf"
          "Id": "LikePuppiesNo"
          "CommandLabel": {
               "en": "no",
               "fr": "non"
          },
          "Steps": [
               {
                    "Type": "Image",
                    "Id": "10",
                    "Source": {
                         "Type": "Picture",
                         "Source": "Web",
                         "Path": "https://assets3.thrillist.com/v1/image/2508887/size/tmg-article_tall.jpg"
                         }
                    }
               },
               {
                    "Type": "Text",
                    "Id": "20",
                    "Label": {
                         "en": "I prefer cats as well :-)",
                         "fr": "Je préfère aussi les chats :-)"
                         }
                    }     
               }
          ] 
     }
     ]


Here we react to the user's choice by adding a picture and some text

## 5. Asking another question

As we've seen, our `Type`: `node` needs to be followed by another `Type`. In this case, we will want to ask another question to the user. To do that, we need to use the connector `LinksTo`.

     ...
     "Commands": [
     {
          "Type": "node"
          "Id": "LikePuppiesYes"
          "CommandLabel": {
               "en": "yes",
               "fr": "oui"
          },
          "LinksTo": {
               "Type": "Node",
               "Id": "PuppiesYesWhichPuppie",
               "Steps": [
                    {
                         "Type": "Text",
                         "Id": "10"
                         "Label": {
                              "en": "Which of these do you prefer?",
                              "fr": "Lequel préfères-tu?"
                         }
                    }
               ]
          }
     },
     ...

## 6. Showing a carousel of choices (picture + text)

Again, we will want to show choices to our user, so we can interact with her/him. 
This time however, we will want to take advantage of the carousel format offered by messaging clients. They are usually made of an image and text.

     ...
     "LinksTo": {
          "Type": "Node",
          "Id": "PuppiesYesWhichPuppie",
          "Steps": [
               {
                    "Type": "Text",
                    "Id": "10"
                    "CommandLabel": {
                         "en": "Which of these do you prefer?",
                         "fr": "Lequel préfères-tu?"
                    }
               }
          ],
          "Commands": [
               {
                    "Type": "leaf"
                    "Id": "PreferPuppyLabrador"
                    "CommandLabel": {
                         "en": "Labrador",
                         "fr": "Labrador"
                    },
                    "CommandPicture": {
                         "Type": "Image",
                         "Source": "Web",
                         "Path": "https://www.thelabradorsite.com/wp-content/uploads/2015/07/yellow-labrador-puppy-garden.jpg"
                    }
               },
               {
                    "Type": "leaf"
                    "Id": "PreferPuppyCorgi"
                    "CommandLabel": {
                         "en": "Corgi",
                         "fr": "Corgi"
                    },
                    "CommandPicture": {
                         "Type": "Image",
                         "Source": "Web",
                         "Path": "https://www.thelabradorsite.com/wp-content/uploads/2015/07/yellow-labrador-puppy-garden.jpg"
                    }
               }
               ]
         }
     ...

To create a carousel, simply add the property `CommandPicture` just after `CommandLabel` and fill it with the relevant details. 

## 7. Executing an action

In some cases, we might want to execute an action as a result of this choice. Actions can be defined by the client or external services. 
For instance, we might want to count the number of people who prefered Labradors or Cogis. 

     ...
     "Commands": [
          {
               "Type": "leaf"
               "Id": "PreferPuppyLabrador"
               "CommandLabel": {
                    "en": "Labrador",
                    "fr": "Labrador"
               },
               "CommandPicture": {
                    "Type": "Image",
                    "Source": "Web",
                    "Path": "https://www.thelabradorsite.com/wp-content/uploads/2015/07/yellow-labrador-puppy-garden.jpg"
               },
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
                         "Id": "20"
                         "Source": {
                              "Type": "Gif",
                              "Source": "Giphy",
                              "Path": "6Umkh0GwRYhfG"
                          }
                    }
                    {
                         "Type": "Text",
                         "Id": "30"
                         "Label": {
                              "en": "Sooo cute",
                              "fr": "Tellement mignon"
                         }
                    }
               ]
          },
     ...


Using this specific action Dovote, we can call the relevant API and count the number of user who voted for one choice or the other. There are many more actions - they will be defined in this file [to be created]


## 8. Taking a break

Steps can be very long arrays of content. We may want to show a GIF, text, an image, another text, another GIF and so on. To prevent overwhelming the user with content and space the display of content in time, we can use a `Break` within the Steps.

It will look as follows: 

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
               "Id": "20"
               "Source": {
                    "Type": "Gif",
                    "Source": "Giphy",
                    "Path": "6Umkh0GwRYhfG"
                }
          },
          {
               "Type": "Break",
               "Id": "25",
               "Mode": "Wait",
               "Parameters": {
                   "ms": 4000
                }
          },
          {
               "Type": "Text",
               "Id": "30"
               "Label": {
                    "en": "Sooo cute",
                    "fr": "Tellement mignon"
               }
          }
     ...

Here, we have introduced a break of 4 seconds (or 4000 milliseconds) between the GIF and the text. It means that the client will need to wait 4 seconds after it displays the Gif and before it displays the next step to the user - in this case a text.


## 9. Appendix: further details on the sequences

### 9.1 The skeleton of a sequence

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


### 9.2 Breaks and Stops


### 9.3 The order of the Steps


### 9.4 A catalogue of actions


### 9.5 Quiz properties for client animation


### 9.6 Type: Story

