# Sequence Definition Format

## Definition

A sequence is a tree where basically, each node represent a new question with choices repesenting the children branches and
the leaves represent terminal actions.

Our tree, is composed by Sequence Elements that can be:

* Node -> Question With Commands
* Leaf -> Terminal Action

Each element of the sequence is represented by an unique Identifier and a Type. The type will always be "Question" for the nodes
but it can be anything qualifying the action for the leaves. 

The structure of the sequences has be tuned in order to be coherent, easy to write and easy to bind to a model object within the apps.

You can see a complete working exemple of the structure in [AreYouOftenLate.json](AreYouOftenLate.json)

## Sequence Element

A Sequence element represents any element in the tree (node or leaf) and share common properties.

Element = 

     {
        "Type" : "Anything",
        "Id"   : "mynodeid",
        "Comments" : "the question of life",
        "Label" : {"key":"value",...},
        "OptionalPrompt":{
          "Type" : "source type",
          "Source" : "source name",
          "Id" : "source id or full path",
          "AdditionalLabels" :[
               {
                    "en":"would you like to continue?",
                    "fr":"veut tu continuer?",
                    "es":"Quieres continuar?"
          ]
        }
        "DisplayMode": "Default"
      }

For each property:

* Type : 
  * mandatory : true
  * type : string, always "Question" for nodes. For commands, it anything that defines the type of the action (ShowCards, SetUserProperty, Nothing, etc....)
  * comments : For the question nodes, the commands property listing the available choices is mandatory.
  
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
     - if the question is a child question present in a choice list, then this label is used as the label shown in choice list not as a separe question for the next step.
     
* OptionalPrompt:
  * mandatory : false
  * type : object =>
      * Type : string, defining the type of the feedback to show user (Image, AnimatedGif, Video, ...). This can be necessary to choose how to display the feedback
      * Source : string, defining the origin of the media used for the feedback (can be Custom, Giphy, Youtube,...)
      * Id : string, something that identifies uniquely the media to show. it can be a full Url or a unique id for the choosen source.
      * AdditionalLabels: Array of labels dictionary(string,string). this contains additional labels that will printed before the question as an optional prompt
  * comments: this optional prompt, if present, will be something that we'll show to the users between the presentation of the question and the choices   
  
  
## Question

A question is a special element defined by the commands it provides. This is usually composed by a label containing the text of the question, and a serie of choices of commands representing the options for the next steps. The question is a node that defines the branching for the next steps.

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
        "Commands":[ question | action, ... ],
        "DisplayMode": "Default",
        "Randomize": true,
        "Skippable":"true"
      }
     
For each property specific to questions:
    
* Commands :
  * mandatory : true,
  * type : array of tree elements. this means that the array can contain terminal actions or other questions.
  * comments : this property is mandatory for obvious reasons but it should also be mandatory that the number of 
  elements is >= 2.
* DisplayMode :
  * mandatory : false,
  * type : string
  * comments : this is manly a hint for indicating the apps how to display the list of choices (all at the same time, one by one, etc...)
* Randomize:
  * mandatory : false,
  * type : boolean. if property is missing, it will consided as _false_
  * commments : if true, it means the choices presented to the user have to be present each time in a random order.
* Skippable:
  * mandatory : false,
  * type : boolean. if property is missing, it will consided as _false_
  * commments : if true, it means that the app will add a skip button at the end of the choices to allow user to skip question and return to normal conversation.
 
  
  ## Action
  
  An action is a terminal leaf in the sequence tree. It defines the action to do at the end of the sequence. The information needed to execute the command is defined within the LinksTo property. As some commands don't need any parameter this property is still optional. When Type=Question then this property contains the body of the next question.
  
  The overall structure of a question is the following:

Action = 

      {
        "Type" : "AnyAction",
        "Id"   : "MyAmazingAction",
        "Comments" : "this should be 42",
        "StepValue" : 1,
        "Label" : {"key":"value",...},
        "LinksTo":{ next action information },
        "Parameters": { additional information for the realization of the action}
        "DisplayMode": "Default",
        "OptionalFeedback":{}
      }
     
For each Property:

* StepValue : 
  * mandatory : false
  * type : int. by default the value "-1" should be affected by the apps, which means "we don't know". If the value is set 
  to "0" this means "we don't care". If "1" this means, "we care". If more than 1 then it means "We care and this is the level of importance"
* LinksTo:
  * mandatory : false,
  * type : object. Contains information about the next step. Can be the body of a question if type=question of the type & id of an intention if Type=ShowCards.
  * comments: you should put here all the information needed to link to the next action. for exemple if the action is of 
  type "ShowCards", this means we want to show cards (text+image) to the users. then you should provide the type of the source
  where you'll pick the cards (ex: Intention) and an identifier within the source (ex: the intention id, 67CC40)
 * Parameters:
  * mandatory : false,
  * type : object. for simplicity, keep it in a dictionary form, with only one level of key/values.
  * comments: you should put here all the information needed for the execution of the action. For exemple if action is "SetUserProperty" you can add  `"Parameters" = {"Property":"propName", "value":"123"}`.
* OptionalFeedback:
  * mandatory : false
  * type : object =>
      * Type : string, defining the type of the feedback to show user (Image, AnimatedGif, Video, ...). This can be necessary to choose how to display the feedback
      * Source : string, defining the origin of the media used for the feedback (can be Custom, Giphy, Youtube,...)
      * Id : string, something that identifies uniquely the media to show. it can be a full Url or a unique id for the choosen source.
      * AdditionalLabels: Array of labels dictionary(string,string). this contains additional labels that will printed before the question as an optional prompt
  * comments: this optional feedback, if present, will be presented to the user just after it's command choice and before continuing to the next step. You can put here a conclusion phrase for the sequence or something that could anounce some other...
    
 
 
 
 
## Additional notes

**Action Labels**, 

some labels should be implicitely provided by the apps. If I'm in a _ShowCard_ action, and I provided the _Type=Intention_ and _Id=67CC40_ then it means that the app is able to get the label for this intention from another service.

for the exact same case, if the _Label_ property is present, then the app is supposed to use this one even if it can guess the values from other sources.

