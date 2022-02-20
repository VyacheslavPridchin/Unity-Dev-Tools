using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public static class Translator
{
    public enum Languages
    {
        Russian, Ukrainian, English, German, Polish, Kazakh, Spanish, Chinese, Japanese, Korean
    }
    public static Dictionary<Languages, string> languagesCodes = new Dictionary<Languages, string>()
    {
        { Languages.Russian, "ru" },
        { Languages.Ukrainian, "uk" },
        { Languages.English, "en" },
        { Languages.German, "de" },
        { Languages.Polish, "pl" },
        { Languages.Kazakh, "kk" },
        { Languages.Spanish, "es" },
        { Languages.Chinese, "zh" },
        { Languages.Japanese, "ja" },
        { Languages.Korean, "ko" },
    };
    public static Languages GlobalLanguage = Languages.English;

    public static string Translate(this string text)
    {
        string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={languagesCodes[GlobalLanguage]}&dt=t&q={text}";
        string result = GetResponse(url);
        return ParseResult(result);
    }
    public static string Translate(this string text, Languages language)
    {
        string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={languagesCodes[language]}&dt=t&q={text}";
        string result = GetResponse(url);
        return ParseResult(result);
    }
    public static string Translate(this string text, string languageCode)
    {
        string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={languageCode}&dt=t&q={text}";
        string result = GetResponse(url);
        return ParseResult(result);
    }

    private static string GetResponse(string url)
    {
        HttpClient httpClient = new HttpClient();
        return httpClient.GetStringAsync(url).Result;
    }
    private static string ParseResult(string json)
    {
        var jsonData = JArray.Parse(json);

        string translation = "";

        foreach (var translate in jsonData[0])
        {
            translation = translation + translate[0].ToString();
        }

        return translation;
    }
}
