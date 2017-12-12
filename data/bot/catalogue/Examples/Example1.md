In this file, we will describe a simple json file and how it should be read by the client.

Our example is the following:

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
