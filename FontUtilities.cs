using UnityEngine.UIElements;

public static class FontUtilities
{
    public static void SetFontSize40( Label label )
    {
        int size = 40;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }

    public static void SetFontSize42( Label label )
    {
        int size = 42;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }

    public static void SetFontSize50( Label label )
    {
        int size = 50;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }
}
