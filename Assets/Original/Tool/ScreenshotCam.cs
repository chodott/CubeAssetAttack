using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PortraitScreenshot : MonoBehaviour
{
    public Camera portraitCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CapturePortrait();
            Debug.Log("Done");
        }
    }

    public void CapturePortrait()
    {
        int width = 192;
        int height = 256;

        // RenderTexture 설정
        RenderTexture rt = new RenderTexture(width, height, 16);
        portraitCamera.targetTexture = rt;
        portraitCamera.Render();

        // Texture2D로 캡처
        RenderTexture.active = rt;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGBA32, false);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        // PNG 파일로 저장 (선택 사항)
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/Portrait.png", bytes);

        // 리소스 정리
        RenderTexture.active = null;
        portraitCamera.targetTexture = null;
        Destroy(rt);
    }
}