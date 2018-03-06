How to create new bot data and configuration
============================================


This document describes how to create a new bot -from server side perspective- and how to test it.

TL;DR;
------

1. you have to create a master file describing the workflow of the conversation you want to have and reference in it the sequences you need for that.
2. reference this master file with the name of your bot in [masterSequencesConfiguration.json](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/apps/botapis/masterSequencesConfiguration.json) file in /botsapi folder
3. if you just added the reference to this master in masterSequenceCondiguration you have to reload this config in bot apis by calling the admin api `/admin/sequences/reloadall?adminkey=xxx`
4. if you already did step 3 (that means botapis know about your new bot), you can refresh only your bot by calling `/admin/sequences/reload/unittestbot?adminkey=xxx`
5. do a first simple test with the apis: 
  * call get next sequence : `http://gw-bot-apis.azurewebsites.net/api/sequences/next`
  * reset the conversation (clear history): `http://gw-bot-apis.azurewebsites.net/admin/command/history/clear?botName=unittestbot&deviceId=a01&facebookId=fb01&adminkey=xxx`
6. test within the apps:
  * enter developper mode
  * enter the name of your bot in the text field, save
  * go to huggy tab and start the conversation, it should use the sequences of your bot
  * return to developper mode and click on the reset button when you want to start the conversation again from the begining
  
  
  
Create your master File
--------------------------

Each `production` bot should be created in it's own folder under [/data/bot/apps](https://github.com/GhostWording/gw-config-apis/tree/master/data/bot/apps). If you have different bots related to the same app, you can put all bot master files in the same folder. 

Each master file should be unique and be named accordingly `{mybotname}Master.json`.

While testing, you should create your master files within the [/data/bot/apps/experiments/](https://github.com/GhostWording/gw-config-apis/tree/master/data/bot/apps/experiments) folder and use a naming pattern like that `{myname}-{mybotname}-master-experiment-{number}.json`.

For exemple for this doc, I created the [/data/bot/apps/experiments/rui-docbot-master-experiments-1.json](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/apps/experiments/rui-docbot-master-experiments-1.json) master file.

You can read the [readme](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/readme.md) file describing the contents of the master file or the [behavior](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/MasterSequenceBehavior.md) to understand the workflow of how the apis choose the next sequence.

Note that the `GroupName` in sequences is important as it's used in the events sent by the apps to identify the type of the sequence.

For the test, we'll create a simple master file that:
* ask a question yes/no
* choose the next sequence accordingly to the answer 
* show a terminal message

```json
[
  {
    "GroupName":"Test",
    "SequenceFiles" : [
        { "order": 10, "file":[
            "/data/bot/sequences/experiments/rui/ShouldSetAUserPropertyCorrectly1.json"]},
        { "order": 20, "file":[
            "/data/bot/sequences/experiments/rui/ShouldExecuteThisIfPropertyWasSetToYes.json"]},
        { "order": 20, "file":[
            "/data/bot/sequences/experiments/rui/ShouldExecuteThisIfPropertyWasSetToNo.json"]},
        { "order": 100, "file":[
            "/data/bot/sequences/experiments/rui/ThisIsTheLastSequenceOfTheConversation.json"]}
      ]
  }
]
```

This testing master sequence file is located here [/data/bot/apps/experiments/rui-docbot-master-experiments-1.json](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/apps/experiments/rui-docbot-master-experiments-1.json)


Working process:

* sequence `ShouldSetAUserPropertyCorrectly`, will ask a question and show 2 choices 'yes' or 'no', then it will store the answer in a user property with a `SetUserProperty` step:

```json
{
   "Type": "Action",
   "Name": "SetUserProperty",
   "Parameters": {
     "property": "ppTestUserProperty",
     "value": "yes"
   }
 }
```
* then it follows with the sequence `ShouldExecuteThisIfPropertyWasSetToYes` that has the condition `ppTestUserProperty == 'yes'` within. the api should use this sequence if you choose 'Yes' 
* then it follows with the sequence `ShouldExecuteThisIfPropertyWasSetToNo` that has the condition `ppTestUserProperty == 'no'` within. the api should use this sequence if you choose 'No' 
* it terminates the conversation with the sequence `ThisIsTheLastSequenceOfTheConversation` just to ensure that we arrived correctly at the planned end.


Load the master file within apis
--------------------------------

if it's the first time, you should create a new entry in [masterSequencesConfiguration.json](https://github.com/GhostWording/gw-config-apis/blob/master/data/bot/apps/botapis/masterSequencesConfiguration.json) for your bot/master file:

```json
 {
   "Name": "docbot",
   "File": "data/bot/apps/experiments/rui-docbot-master-experiments-1.json"
 }
```

Once this is done, you need to do a GET ON the reload all api for the BotApis know about your new master file :

```
 GET http://gw-xxx.azurewebsites.net/admin/sequences/reloadall?adminkey=yyy
 Headers: 
    Accept : application/json
```

_(note that host + keys are removed from this documentation for security reasons)_


Test it first with the apis
---------------------------

in order to test the master file directly with the apis, you need to call the 'get next sequence' endpoint:

``` 
POST http://xxx.azurewebsites.net/api/sequences/next
headers:
  - Accept : application/json
  - Content-Type : application/json
body: raw application/json
{
  "BotName":"docbot",
  "DeviceId": "mydevice123",
  "FacebookId":"myfb123"
}
```

you can also set manually a property directly with the apis:

```
POST http://xxx.azurewebsites.net/api/user/setproperty
headers:
  - Accept : application/json
  - Content-Type : application/json
body: raw application/json
{
  "BotName":"docbot",
  "DeviceId": "mydevice123",
  "FacebookId":"myfb123",
  "PropertyName": "ppTestUserProperty", 
	 "PropertyValue" : "no" 
}

```

When you finished you can reset the conversation with the clear endpoint:

```
GET http://xxx.azurewebsites.net/admin/command/history/clear?botName=docbot&deviceId=mydevice123&facebookId=myfb123&adminkey=xxxx

```


Test is within the apps
-----------------------

In development mode, you can setup the bot name you want to interact with:

- open the app
- go in developer mode
- in the window, type in the name of the botname that you want to test
- click on "save" (will be changed to "load")
- go in Huggy and use the bot that you just loaded
- once you have used the sequence one time, you can test them again by going back in dev mode and clicking on "reset"





