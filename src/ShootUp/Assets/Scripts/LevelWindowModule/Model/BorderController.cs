using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class BorderController
    {
        private readonly GameSettings _gameSettings;
        private readonly BorderProvider _borderProvider;
        private readonly CameraProvider _cameraProvider;

        public float MinX { get; private set; }
        public float MaxX { get; private set; }
        public float MinY { get; private set; }
        public float MaxY { get; private set; }

        public BorderController(GameSettings gameSettings, CameraProvider cameraProvider, TickInvoker tickInvoker)
        {
            _cameraProvider = cameraProvider;
            _gameSettings = gameSettings;
            _borderProvider = Object.FindObjectOfType<BorderProvider>();

            tickInvoker.Updated += UpdateBorders;
        }

        public void UpdateBorders()
        {
            float cameraPixelHeight = _cameraProvider.CameraPixelHeight;
            float cameraPixelWidth = _cameraProvider.CameraPixelWidth;
            float pixelToMetr = _cameraProvider.PixelToMetr;

            float sideScale = cameraPixelHeight * pixelToMetr * _gameSettings.BorderScaleFactor;

            float leftBorderShift = _gameSettings.LeftBorderShift;
            float leftCoord = (leftBorderShift - cameraPixelWidth / 2) * pixelToMetr;
            Transform leftBorderTransform = _borderProvider.LeftBorder.transform;
            leftBorderTransform.position = new Vector3(leftCoord, 0, 0);
            leftBorderTransform.localScale = new Vector3(1, sideScale, 1);
            MinX = _borderProvider.LeftBorder.bounds.max.x;

            float rightBorderShift = _gameSettings.RightBorderShift;
            float rightCoord = (rightBorderShift + cameraPixelWidth / 2) * pixelToMetr;
            Transform rightBorderTransform = _borderProvider.RightBorder.transform;
            rightBorderTransform.position = new Vector3(rightCoord, 0, 0);
            rightBorderTransform.localScale = new Vector3(1, sideScale, 1);
            MaxX = _borderProvider.RightBorder.bounds.min.x;

            float bottomBorderShift = _gameSettings.BottomBorderShift;
            float bottomCoord = (bottomBorderShift - cameraPixelHeight / 2) * pixelToMetr;
            Transform bottomBorderTransform = _borderProvider.BottomBorder.transform;
            bottomBorderTransform.position = new Vector3(0, bottomCoord, 0);
            float bottomScale = cameraPixelWidth * pixelToMetr * _gameSettings.BorderScaleFactor;
            bottomBorderTransform.localScale = new Vector3(bottomScale, 1, 1);
            MinY = _borderProvider.BottomBorder.bounds.max.y;
            MaxY = (_gameSettings.UpLimitPixelShift + cameraPixelHeight / 2) * pixelToMetr;
        }
    }
}