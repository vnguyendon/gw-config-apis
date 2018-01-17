There may sometimes be differences in format integration between clients. A client X may have implemented all the actions, whilst another client Y only needs part of them.
Therefore, issues may arise when client Y is suggested to read a file for which he hasn't implemented all the necessary properties.


#### 1. Choosing which client can read a file

To avoid this problem from occuring, the writer can set the clients he wants to be able to parse the files.
Example: for one specific file, I know that only the Android and Messenger clients can execute the action *read a video*. 
=> In this case, at the first level of the json file, I should write `"Channels": ["Messenger", "Android"]`

At the moment, we only have three clients. We will grossly distinguish them as follows:
- `"Messenger"`
- `"iOS"`
- `"Android"`

We are not bound to these names and we could even distinguish between different apps/bots within each of these clients. We should use the naming convention **app/bot-platform**. Example: 
- `"Quoteo-Messenger"`
- `"SurveyPoney-Messenger"`
- `"PopularStickers-iOS"`
...

The point here is that there may even be differences of integration between different bots or apps on the same platform, because they do not necessarily share the same code.

#### 2. Sequence example

If only the iOS and Android have implemented the `"DoVote"` action so far and I want to restrict access to this file to these two (meaning: not Messenger clients), we would have the following file:
```
{
  "Type": "Node",
  "Id": "BoxOrYoga",
  "Steps": [
      {
          "Type": "Text",
          "Label": {
            "en": "You have to do some exercise. You choose"
          }
      }
  ],
  "Channels": ["Android", "iOS"],
  "Randomize": "true",
  "Commands": [
      {
          "Type": "Leaf",
          "Id": "PreferBox",
          "ElementValue": "1",
          "CommandLabel": {
            "en": "boxing"
          },
          "Steps": [
              {
                  "Type": "Action",
                  "Name": "DoVote"
              },
              {
                  "Type": "Action",
                  "Name": "ShowSurveyResults"
              }
          ]
      },
      {
          "Type": "Leaf",
          "Id": "PreferYoga",
          "ElementValue": "1",
          "CommandLabel": {
            "en": "yoga"
          },
          "Steps": [
              {
                  "Type": "Action",
                  "Name": "DoVote"
              },
              {
                  "Type": "Action",
                  "Name": "ShowSurveyResults"
              }
          ]
      }
  ]
}
```


#### 3. Default behaviour if `"Channels"` isn't set

What if there is no `"Channels"` property in my file? In this case, we should consider that all clients are able to read, by default.

If this isn't the case, then we should change it or alert the writer.
