using UnityEngine;

public static class TextureResizer
{
    /// <summary>
    /// ћетод ResizeCorrect измен€ет размер изображени€ без обрезаний
    /// </summary>
    /// <param name="targetX">Ўирина текстуры</param>
    /// <param name="targetY">¬ысота текстуры</param>
    /// <returns>Texture2D с обработанным изображением</returns>
    public static Texture2D ResizeCorrect(this Texture2D texture2D, int targetX, int targetY)
    {
        if (targetX < 0) targetX = 1;
        if (targetY < 0) targetY = 1;
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}
