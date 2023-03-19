using UnityEngine;

namespace LevelWindowModule
{
    public class CameraProvider
    {
        public Camera Camera { get; }
        public float CameraPixelWidth => Camera.pixelWidth;
        public float CameraPixelHeight => Camera.pixelHeight;
        public float MetrToPixel => Camera.pixelHeight / Camera.orthographicSize / 2;
        public float PixelToMetr => 2 * Camera.orthographicSize / Camera.pixelHeight;

        public CameraProvider()
        {
            Camera = Camera.main;
        }
    }
}