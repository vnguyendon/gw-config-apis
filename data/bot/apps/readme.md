
In this file, we will describe our proposal for the Masterfile logic. 

## 1. Group names

**As a client, I will push userevents about sequences with the right TargetType.**

When you push user events about sequences (the `Huggy` events like "HuggySequenceStart,HuggySequenceNext,etc..."), you need to know how to categorize the event. If you are executing a sequence that is a survey, your engine don't know that it's a survey, and you can't define the TargetType="Survey" in your Huggy events.

In order to get this TargetType correctly filled, the common rule is to use the `GroupName` where the sequence belongs to as the name used for the TargetType.

Example:
```
[
  {
    "GroupName":"Start",
    "SequenceFiles" : [
      { "order" : 10, "file": ["/data/bot/sequences/experiments/HowAreYou.json"]}
    ]
  },
  {
    "GroupName":"Survey",
    "SequenceFiles" : [
      { "order" : 11, "file": ["/data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json"]},
      { "order" : 12, "file": ["/data/bot/sequences/surveys/WCatsOrDogs.json"]}
    ]
  }
]
```

For this exemple, when you send HuggySequenceStart for the HowAreYou sequence, the TargetType of the userevent should be "Start" whereas the TargetType of the WCatsOrDogs sequence will be "Survey"


## 2. Ordering

**As a client, I will display the files that have the smallest number first and the files that have the largest number last.**

Example:
```
  {
    "GroupName":"Survey",
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


##### Important note: the `GroupName` should not have an impact on the ordering

Example:

```
[
  {
    "GroupName":"Start",
    "SequenceFiles" : [
      { "order" : 10, "file": ["/data/bot/sequences/experiments/HowAreYou.json"]}
    ]
  },
  {
    "GroupName":"Survey",
    "SequenceFiles" : [
      { "order" : 9, "file": ["/data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json"]},
      { "order" : 12, "file": ["/data/bot/sequences/surveys/WCatsOrDogs.json"]}
    ]
  }
]
```

In the above example, the first file to be displayed to the user will be "/data/bot/sequences/surveys/WHaveYouTriedAChatbotYesNo.json" (order number: 9). Then, the user will see "/data/bot/sequences/experiments/HowAreYou.json" (order number: 10). Finally, the user will see "/data/bot/sequences/surveys/WCatsOrDogs.json" (order number: 11).


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

