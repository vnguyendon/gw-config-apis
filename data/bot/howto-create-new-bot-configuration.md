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
  
  
  
