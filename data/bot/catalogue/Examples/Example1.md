In this file, we will describe a simple json file and how it should be read by the client.

Our example is the following:
```
{
  "Steps": [
    {
      "Type": "AnimatedGif",
      "Parameters": {
        "Source": "Giphy",
        "Path": "9AeRnRRNQokeI"
      }
    },
    {
      "Type": "Text",
      "Label": {
        "en": "My name is Marvin, I am a bot",
        "fr": "Je m'appelle Marvin, je suis un robot",
        "es": "Mi nombre es Marvin, soy un robot"
      }
    },
    {
      "Type": "Image",
      "Parameters": {
        "Source": "Internal",
        "Path": "/specialoccasions/jokes/default/small/000022011350.jpg"
      }
    }
  ]
}
```

Here's how to read it:

### a. What is a Step?

A `"Steps"` is a container of content that will be shown to a user
-> Its value will always be an array

```
{
  "Steps": [
  ]
}
```

-> This array will contain several hashes 

```
{
  "Steps": [
    {},
    {},
    {}
  ]
}
```

-> These hashes should be read in order of appearance


### b. How should I read a hash contained in a Steps?

-> Each hash defines a bit of content or an action to perform
-> The nature of each hash will be defined by its `"Type"`

#### i. Example with a Gif

If we want to show a Gif, we will use the following pair `"Type": "AnimatedGif`. 
Example: 

```
{
  "Steps": [
    {
      "Type": "AnimatedGif",
      "Parameters": {
        "Source": "Giphy",
        "Path": "9AeRnRRNQokeI"
      }
    },
  ]
}
```

-> We define the source of the media and its path in the `"Parameters"`
-> In this case, the Gif will come from Giphy 
-> We will be able to retrieve this GIF by using the path `"9AeRnRRNQokeI"` in its API or directly via a web link by concatenating  https://giphy,com/gifs/ and 9AeRnRRNQokeI. Like that: https://giphy.com/gifs/9AeRnRRNQokeI

#### ii. Example with text

If we want to show some text, we will use the following pair `"Type": "Text`. 
Example: 

```
{
  "Steps": [
    {
      "Type": "Text",
      "Label": {
        "en": "My name is Marvin, I am a bot",
        "fr": "Je m'appelle Marvin, je suis un robot",
        "es": "Mi nombre es Marvin, soy un robot"
      }
    },
  ]
}
```

-> We define the text in the `"Label"`
-> In this case, we provide three languages. ("en" for English / "fr" for French / "es" for Spanish)
-> We will send the text to the user in the language that the app is set into (English/French/Spanish)

#### iii. Example with an image

If we want to show an image, we will use the following pair `"Type": "Image`. 
Example: 

```
{
  "Steps": [
    {
      "Type": "Image",
      "Parameters": {
        "Source": "Internal",
        "Path": "/specialoccasions/jokes/default/small/000022011350.jpg"
      }
    },
  ]
}
```

-> Again, we define the source of the media and its path in the `"Parameters"`
-> In this case, the Gif will come from an "Internal" source, meaning from WaveMining's database
-> We will be able to retrieve this image by using recomposing its path. To do so, we will concatenate "http://gw-static.azurewebsites.net" and `"/specialoccasions/jokes/default/small/000022011350.jpg"`
-> We will then get the image http://gw-static.azurewebsites.net/specialoccasions/jokes/default/small/000022011350.jpg
