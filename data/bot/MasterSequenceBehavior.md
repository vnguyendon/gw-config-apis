# Master Sequence file

## Master File Structure

This file defines the flow of conversation with sequences.

the structure of the file is the following:

```
Array of [
  - group
     - group name
     - Array of sequences with order [
        - orderedSequence for a position 
          - order
          - array of possible sequence files
     ]
]
```

This defines the list of available sequences of conversation and their order.

just to be clear on the names after that:

* a [sequence] = `"/path/to/sequence.json"` => reference to the json file of a sequence
* an [orderedSequence] = sequences list for a same position and order = `{ "order" = 10, files=["/path/seq1.json","/path/seq2.json"]}`

## Definitions and variables

We'll try to define here the rules that define the way we pick the next sequence for a user.

When we try to get the next sequencee for a user we'll need (in parenthesis, the name of the objects for further reference):

* the master file of the sequences (master) for the named bot (botname)
* the user context with the user profile (userprofile) - we need it to test the conditions within sequences and see if we can pick or not the sequence
* the information of the last sequence (sequenceid)
* the list of the previously shown sequences (previousSequences) - in order to not show a sequence twice.


## Workflow

* get current sequence information ([sequenceid], [order])
* if [previousSequences] do not contain current [sequenceid], add it in the list
* we filter the complete list of master file sequences in order to keep only the ones that are not shown yet and valid:
    * Valid = test sequence "Condition" property with current [userprofile] of all sequences in a orderedSequence, if at least one is valid the orderedSequence is considered valid.
    * Not shown = for an orderedSequence in a position, if at least one of the sequences in it's array is in the [previousSequences] list we consider that position shown
* order the remaining orderedSequence
* get the lowest order of this list
* take all elements in the list for that order (= all orderedSequences not shown with that order)
* pick one random orderedSequence in the remaining list
* in the selected orderedSequence, pick one random sequence not shown.


## Challenges

* we have to see if it makes sense to be able to show new sequences added in a new release even if the current user order is greater? should it be an option ?
* do we have solutions to be able to use and repeat one sequence many times? 
* does it make sense to have arrays of sequences along with multiple other arrays of sequences with the same order? (or should we authorize only one dimension array => many sequences in an array for a position or many sequences for an order but many sequences arrays for 1 order) ?

