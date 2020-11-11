# Label Retriever
Have you ever ask yourself how to easily introduce support for translation during the development cycle? I have, obviously, and I decided that I should make a stand-alone solution for it.

## Background
In previous Kata, exercises, I have relied on hard coded strings for communication to the user, this works for smaller projects but I wanted to have a look at how I could support something far more complicated in the future. Enter the resource file. Using a business layer I can create resource files that contain my labels, I can also specify which language these labels are supporting. Once you build the project containing the resource file a designer file is automatically generated.

This is super lovely but to support English and Spanish it will require referencing both designer files. To make this system more maintainable I created a utility called Label Retriever, whose purpose is to automatically determine which resource file I should use based on the culture of the current thread running the application. This utility follows the Singleton Design Pattern and enables me to create one and only one instance of this object. For this utility the singleton approach made sense as I was not planning on supporting the language to change while the application was running.

An important caveat is that this is really just a proof of concept for me, as I have only translated one of the labels into Spanish. Currently when building the necessary artifacts are generated to run this application in English or Spanish. So the remaining work for translation is simply the translation itself, update the resource files, and generate a new build.

## Source sample
How to determine culture:

```csharp
/// <summary>
///     Constructor.
/// </summary>
/// <param name="language">The language that will be used for translation purposes.</param>
/// <remarks>If language is left null, will default to English United States.</remarks>
private LabelRetriever(Languages language)
{
	var languageValue = (language ?? Languages.EnglishUnitedStates).Value;
    var cultureInfo = CultureInfo.CreateSpecificCulture(languageValue);
    SetCultureTo(cultureInfo);
}
How I setup getters for the labels stored in the designer files:
#region Application labels
public string ApplicationStart => Labels.ApplicationStart;
public string GameStart => Labels.GameStart;
public string GameOver => Labels.GameOver;
#endregion	
```

## How to get
You can either download the source to include in your project or you can download the NuGet package, https://www.nuget.org/packages/Kabatra.Common.LabelRetriever/.