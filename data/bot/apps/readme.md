
In this file, we will describe our proposal for the Masterfile logic. 

## 1. Group names



## 2. Ordering

**As a client, I will display the files that have the smallest number first and the files that have the largest number last.**

Example:
```
  {
    "GroupName":"All",
    "SequenceFiles" : [
      { "order" : 10, "file": ["/data/bot/sequences/experiments/HowAreYou.json"]},
      { "order" : 11, "file": ["/data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json"]},
      { "order" : 12, "file": ["/data/bot/sequences/surveys/WCatsOrDogs.json"]},
      { "order" : 15, "file": ["/data/bot/sequences/surveys/WOlympicsOrFootballWorldCup.json"]},
      { "order" : 17, "file": ["/data/bot/sequences/surveys/WLoveOrFriendship.json"]},
      { "order" : 19, "file": ["/data/bot/sequences/surveys/WEarlyBirdOrNightOwl.json"]}
      ]
  }
```

Here the first sequence to be displayed will be /data/bot/sequences/experiments/HowAreYou.json because it is assigned the number 10. The last sequence to be shown to the user will be /data/bot/sequences/surveys/WEarlyBirdOrNightOwl.json, because it carries the largest number (19). 


## 3. Randomizing files

**As a client, I will display in random order the files that are attributed the same ordering number.**

Example:
```
  {
    "GroupName":"All",
    "SequenceFiles" : [
      { "order" : 10, "file": ["/data/bot/sequences/experiments/HowAreYou.json"]},
      { "order" : 10, "file": ["/data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json"]},
      { "order" : 10, "file": ["/data/bot/sequences/surveys/WCatsOrDogs.json"]},
      { "order" : 15, "file": ["/data/bot/sequences/surveys/WOlympicsOrFootballWorldCup.json"]},
      { "order" : 17, "file": ["/data/bot/sequences/surveys/WLoveOrFriendship.json"]},
      { "order" : 19, "file": ["/data/bot/sequences/surveys/WEarlyBirdOrNightOwl.json"]}
      ]
  }
```
Here, the files /experiments/HowAreYou.json, /surveys/WHaveYouTriedAChatbotYesNo.json and /surveys/WCatsOrDogs.json will be the three first files to be seen by the user. However, we can't predict their order as they are assigned a similar number.

## 4. Picking one file OR another

**As a client, I will only display one of the files contained in an array and I will choose it randomly.

Example:
```
  {
    "GroupName":"All",
    "SequenceFiles" : [
      { "order" : 10, "file": ["/data/bot/sequences/experiments/HowAreYou.json, /data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json"]},
      { "order" : 11, "file": ["/data/bot/sequences/surveys/WCatsOrDogs.json"]},
      { "order" : 15, "file": ["/data/bot/sequences/surveys/WOlympicsOrFootballWorldCup.json"]},
      { "order" : 17, "file": ["/data/bot/sequences/surveys/WLoveOrFriendship.json"]},
      { "order" : 19, "file": ["/data/bot/sequences/surveys/WEarlyBirdOrNightOwl.json"]}
      ]
  }
```
Here, the files /data/bot/sequences/experiments/HowAreYou.json and /data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json are contained in the same array. It means that we will see only one of them and we can't predict which one.

