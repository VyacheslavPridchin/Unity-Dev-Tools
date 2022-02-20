using UnityEngine;
using UnityEngine.UI;

public class TranslatorExample : MonoBehaviour
{
    public InputField inputText;
    public Dropdown selectedLanguage;
    public InputField inputCode;
    public InputField result;

    public void Translate1()
    {
        result.text = inputText.text.Translate((Translator.Languages)selectedLanguage.value);
    }

    public void Translate2()
    {
        result.text = inputText.text.Translate(inputCode.text);
    }

    public void Translate3()
    {
        Translator.GlobalLanguage = Translator.Languages.Chinese;
        result.text = inputText.text.Translate();
    }
}
