# Sequence Definition Format

## Definition

A sequence is a tree where basically, each node represent a new question with choices repesenting the children branches and
the leaves represent terminal actions.

Our tree, is composed by elements that can be:

* Node -> Question
* Leaf -> Action

Each element of the sequence is represented by an unique Identifier and a Type. The type will always be "Question" for the nodes
but it can be anything qualifying the action for the leaves. 

The structure of the sequences has be tuned in order to be coherent, easy to write and easy to bind to a model object within the apps.

## Question

A question is defined by the choices it provides. This is usually composed by a label containing the text of the question, and a
serie of choices representing the options for the next steps.

The overall structure of a question is the following:

Question = 

      {
        "Type" : "Question",
        "Id"   : "MyAmazingQuestion",
        "Comments" : "the question of life",
        "Label" : {"key":"value",...},
        "OptionalFeedback":{
          "Type" : "source type",
          "Source" : "source name",
          "Id" : "source id or full path"
        }
        "Choices":[ question | action, ... ],
        "DisplayMode": "Default"
      }
     
For each property:

* Type : 
  * mandatory : true
  * type : string, always "Question" for nodes. 
  * comments : For the question nodes, the Choice property is mandatory.
* Id :
  * mandatory : true
  * type : string, anything that qualifies uniquely the question
  * comments : to keep things consistent, use One word without spaces in Pascal case (= ThisIsMyIdentifier)
* Comments : 
  * mandatory : false
  * type : string, this is not supposed to be used by apps but just to facilitate the human readability of the json
* Label :
  * mandatory : false
  * type : dictionary (string,string). Key/values with the culture as key and the translated label for culture in value.
  * comments : 
     - for simplicity, the format used for culture is 2 letters (ie: use "en" for english instead of "en-GB" for exemple)
     - whereas the label is not mandatory, you should always provide labels with the choices as it will be seen by the user 
     the visible question
* OptionalFeedback:
  * mandatory : false
  * type : object =>
      * Type : string, defining the type of the feedback to show user (Image, AnimatedGif, Video, ...). This can be necessary to choose how to display the feedback
      * Source : string, defining the origin of the media used for the feedback (can be Custom, Giphy, Youtube,...)
      * Id : string, something that identifies uniquely the media to show. it can be a full Url or a unique id for the choosen source.
  * comments: this optional feedback, if present, will be something that we'll show to the users between the presentation of the question and the choices       
* Choices :
  * mandatory : true,
  * type : array of tree elements. this means that the array can contain terminal actions or other questions.
  * comments : this property is mandatory for obvious reasons but it should also be mandatory that the number of 
  elements is >= 2.
* DisplayMode :
  * mandatory : false,
  * type : string
  * comments : this is manly a hint for indicating the apps how to display the list of choices (all at the same time, one by one, etc...)
  
  
  ## Action
  
  An action is a terminal leaf in the sequence tree. It defines the action to do at the end of the sequence.
  
  The overall structure of a question is the following:

Action = 

      {
        "Type" : "AnyAction",
        "Id"   : "MyAmazingAction",
        "Comments" : "this should be 42",
        "StepValue" : 1,
        "Label" : {"key":"value",...},
        "Parameters":{ anything needed for the action in key/value form },
        "DisplayMode": "Default"
      }
     
For each Property:

* Type : 
  * mandatory : true
  * type : string, anything that defines the type of the action (ShowCards, SetUserProperty, Nothing, etc....)
  * comments : as for id,  use One word without spaces in Pascal case (= ThisIsMyType)
* Id :
  * mandatory : true
  * type : string, anything that qualifies uniquely the action
  * comments : to keep things consistent, use One word without spaces in Pascal case (= ThisIsMyIdentifier)
* Comments : 
  * mandatory : false
  * type : string, something defining the action. this is not supposed to be used by apps but just to facilitate the human readability of the json
* StepValue : 
  * mandatory : false
  * type : int. by default the value "-1" should be affected by the apps, which means "we don't know". If the value is set 
  to "0" this means "we don't care". If "1" this means, "we care". If more than 1 then it means "We care and this is the level of importance"
* Label :
  * mandatory : false
  * type : dictionary (string,string). Key/values with the culture as key and the translated label for culture in value.
  * comments : 
     - for simplicity, the format used for culture is 2 letters (ie: use "en" for english instead of "en-GB" for exemple)
     - most actions won't need a label
* OptionalFeedback:
  * mandatory : false
  * type : object =>
      * Type : string, defining the type of the feedback to show user (Image, AnimatedGif, Video, ...). This can be necessary to choose how to display the feedback
      * Source : string, defining the origin of the media used for the feedback (can be Custom, Giphy, Youtube,...)
      * Id : string, something that identifies uniquely the media to show. it can be a full Url or a unique id for the choosen source.
  * comments: this optional feedback, if present, will be something that we'll show to the users along with the action
* DisplayMode :
  * mandatory : false,
  * type : string
  * comments : this is manly a hint for indicating the apps how to display the list of choices (all at the same time, one by one, etc...)
* Parameters:
  * mandatory : false,
  * type : object. for simplicity, keep it in a dictionary form, with only one level of key/values.
  * comments: you should put here all the information needed for the execution of the action. for exemple if the action is of 
  type "ShowCards", this means we want to show cards (text+image) to the users. then you should provide the type of the source
  where you'll pick the cards (ex: Intention) and an identifier within the source (ex: the intention id, 67CC40)
  
 


