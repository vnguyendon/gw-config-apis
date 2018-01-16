# Conditional execution of sequences

## Short Introduction

In order to decide if a sequence can be presented to a user we can add to sequences a property `Condition="expression"`.

This expression will be dynamically evaluated for the user context (variables are based on the information we have for users)

exemples of supported expressions:

* show only if you are a young french male `"ppAge=='less18' && u.Gender=='male' && u.Country=='France'"`
* show only if your profile is INTP or (INTJ and woman) : 
`"u.PsychologicalProfile=='INTP' || (u.PsychologicalProfile=='INTJ'" && u.Gender=='female')"`
* show only if you're in south america (by using timezones) : `"u.Timezone >= -6 && u.Timezone < -2"`

## Supported operators

The expression should be a simple boolean expression using the basic following operators:

* `(` open parentheses
* `)` closed parentheses
* `+` addition (for numbers and strings)
* `-` subtration
* `*` mutliplication
* `/` division
* `%` modulus
* `+` unary plus
* `-` unary minus
* `==` equal (for numbers and strings)
* `!=` not equal (for numbers and strings)
* `<` less than (for numbers and strings)
* `<=` less than or equal (for numbers and strings)
* `>` greater than (for numbers and strings)
* `>=` greater than or equal (for numbers and strings)
* `&&` boolean and

## Supported Variables

### Real User properties

A user property is something that qualifies directly the user and is known (something that comes from it's facebook profile for exemple).

this is the list of actual known user properties:

* `FirstName` (string)
* `LastName` (string)
* `Gender` (string), can be 'male|female' or empty
* `TimeZone` (int) (the +/- hours from GMT)
* `Country` (string)
* `Locale` (string) (default en-EN)
* `PsychologicalProfile` (string) (should contain MBTI letters when known)

User properties are attached to a User entity and can be used in expressions with a `u.` prefix: `u.FirstName`, `u.Gender`

### Dynamic Properties

These properties are set dynamically by the apps and are only stored as key/value pairs, apps don't know what to do specifically with them.
they can be set and defined by the `SetUserProperty` steps in sequences (or directly as events through the apps).

These dynamic properties always have their name starting with `pp`. 
They can by used in expressions directly with their names, ex: `ppAge == '18-39'`

## A word on Null

In order to simply test the existence of a value, we also support the comparison with null (for exemple, _I want to execute this sequence only if ppAge property is set_).

If you want to do that compare the property with null:

```
{ 
   ...
   "Comment": "execute this sequence is age is already set"
   "Condition": "ppAge != null"
   ...
}
