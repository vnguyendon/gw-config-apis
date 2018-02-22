# Master Sequence file

## Definitions

This file defines the flow of conversation with sequences.

the structure of the file is the following:

```
Array of [
  - group
     - group name
     - Array of sequences with order [
        - ordered sequence for a position 
          - order
          - array of possible sequence files
     ]
]
```

This defines the list of available sequences of conversation and their order.

## Get Next Sequence workflow

We'll try to define here the rules that define the way we pick the next sequence for a user.

When we try to get the next sequencee for a user we'll need (in parenthesis, the name of the objects for further reference):

* the master file of the sequences (master) for the named bot (botname)
* the user context with the user profile (userprofile) - we need that to test the conditions within sequences
* the information of the last sequence (sequenceid)
* the list of the previously show sequences (previousSequences) - in order to not show a sequence twice.

