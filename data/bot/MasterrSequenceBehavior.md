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

