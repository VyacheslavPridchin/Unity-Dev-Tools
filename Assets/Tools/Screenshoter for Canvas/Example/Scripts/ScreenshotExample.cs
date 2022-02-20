using UnityEngine;
using UnityEngine.UI;

public class ScreenshotExample : MonoBehaviour
{
    public Canvas canvas;
    public InputField inputField;
    public Image image;

    public void MakeScreenshot()
    {
        Texture2D texture2D = canvas.MakeScreenshot();
        image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }

    public void SaveScreenshot()
    {
        canvas.SaveScreenshot(inputField.text);
    }

}
