using System;
using System.IO;
using UnityEngine;

public static class CanvasScreenshoter
{
    #region Extensions
    /// <summary>
    /// Метод MakeScreenshot делает скриншот Canvas
    /// </summary>
    /// <returns>Texture2D с изображением Canvas</returns>
    public static Texture2D MakeScreenshot(this Canvas canvas, int maxSize = 720)
    {
        return DoSnapshot(canvas.GetComponent<RectTransform>(), maxSize);
    }

    /// <summary>
    /// Метод SaveScreenshot делает скриншот Canvas и сохраняет его в файл
    /// </summary>
    /// <param name="path">Путь сохранения текстуры</param>
    public static void SaveScreenshot(this Canvas canvas, string path, int maxSize = 720)
    {
        var tex = DoSnapshot(canvas.GetComponent<RectTransform>(), maxSize);

        if (isFileNameValid(path))
        {
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllBytes(path, tex.EncodeToPNG());
        }
        else
            Debug.LogError("Неверный путь создания файла!");

        static bool isFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;

            if (Path.GetExtension(fileName) != String.Empty)
                return true;
            else
                return false;

        }
    }
    #endregion
    #region Screenshot moment
    private static Texture2D DoSnapshot(RectTransform canvasRect, int maxSize)
    {
        Canvas canvas = canvasRect.GetComponent<Canvas>();
        RenderMode myRenderMode = canvas.renderMode;
        canvas.renderMode = RenderMode.WorldSpace;

        Vector2 myPivot = canvasRect.pivot;
        canvasRect.pivot = new Vector2(0.5f, 0.5f);

        GameObject camera = new GameObject("Temp Camera", new Type[] { typeof(Camera) });
        Vector2 size = canvasRect.rect.size;
        size.x = size.x * canvasRect.lossyScale.x;
        size.y = size.y * canvasRect.lossyScale.y;

        Camera cam = camera.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.orthographic = true;
        cam.cullingMask = (1 << LayerMask.NameToLayer("UI"));
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.aspect = size.x / size.y;
        cam.orthographicSize = size.y / 2;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 5;
        cam.transform.position = canvasRect.position + -canvasRect.forward;
        cam.transform.rotation = Quaternion.LookRotation(canvasRect.position - cam.transform.position);
        cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, canvasRect.eulerAngles.z);


        var renderTexture = new RenderTexture((int)Math.Floor(size.x), (int)Math.Floor(size.y), 24);
        cam.targetTexture = renderTexture;
        cam.Render();

        RenderTexture.active = renderTexture;
        var tex = new Texture2D((int)Math.Floor(size.x), (int)Math.Floor(size.y));
        tex.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
        tex.Apply();

        if (tex.width > maxSize)
            tex = tex.ResizeCorrect(maxSize, (int)(maxSize / cam.aspect));

        if (tex.height > maxSize)
            tex = tex.ResizeCorrect((int)(maxSize * cam.aspect), maxSize);

        canvas.renderMode = myRenderMode;
        canvasRect.pivot = myPivot;

        RenderTexture.active = null;
        cam.targetTexture = null;
        UnityEngine.Object.Destroy(cam.gameObject);
        return tex;
    }
    #endregion
}
