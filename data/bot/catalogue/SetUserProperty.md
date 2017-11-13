# "SetUserProperty"


## ppSingle: enabling/disabling Single mode

###### 1. Definition

"EnableSingleMode" and "DisableSingleMode" are the two faces of the same coin: they are properties set when the user either reveals that he is single or not.

When the property/value pair `"Type: "Action"` is triggered along the `"Name: "SetUserProperty"`, we can set a property for the user's relationship status. That can then be used to personalize the experience in the app or in the bot.

To set this user property, we need to send the relevant parameters to the server. For that, we will use the `"Property": "ppSingle"` and the `"Value": "yes"` or `"no"`, depending on the answer 

###### 2. Client integration

No pre-requirements.

###### 3. Within a sequence

This `"SetUserProperty"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. It can be inserted anywhere in the `"Steps"` (first or last position - it does not matter).


###### 4. Example

    ...
      "Steps": [
            {
                ...
            },
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppSingle",
                   "Value": "Yes"
              }
            }
        ]


## Psychological profile: ppDecisionsAriseFrom / ppEnergeticAfter / ppOpenedOrSettled / ppReceptiveTo

###### 1. Definition

By using a set of 4 sequences, the client can ask the user 4 questions and establish their psychological profile from there.

To do that, we will need to set a property for each of the user's personality traits. Each sequence will be in charge of setting one user property and one of its two possible values. The properties and their possible values are the following:
- ppDecisionsAriseFrom: Feelings / Thinking
- ppEnergeticAfter: OnYourOwn / WithPeople
- ppOpenedOrSettled: Opened / Settled
- ppReceptiveTo: Ideas / Facts

To set these user properties, we need to send the relevant parameters to the server. We will use the pair `"Type: "Action"` triggered along the `"Name: "SetUserProperty"`. The user's psychological profile can then be used to personalize the experience in the app or in the bot.

###### 2. Client integration

No pre-requirements.

###### 3. Within a sequence

This `"SetUserProperty"` action needs to be inserted inside a Step hash, in link with a `"Type": "Action"` pair. It can be inserted anywhere in the `"Steps"` (first or last position - it does not matter).


###### 4. Example

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppReceptiveTo",
                   "Value": "Ideas"
              }
            }
        ]

Alternatively, if the user had answered `"Facts"`, we would have written: 

    ...
      "Steps": [
            {
              "Type": "Action",
              "Name": "SetUserProperty",
              "Parameters": {
                   "Property": "ppReceptiveTo",
                   "Value": "Facts"
              }
            }
        ]
