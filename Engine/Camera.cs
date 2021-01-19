using SFML.System;
using SFML.Graphics;

namespace Project_Neros.Engine
{
    class Camera
    {
        public Vector2f Target { get; set; }
        public Vector2f WinSize { get; set; }
        public float Scale { get; set; } = 1f;

        public Camera(Vector2f winSize, Vector2f? target = null)
        {
            WinSize = winSize;
            Target = target.GetValueOrDefault();
        }

        public void Move(Vector2f shift)
        {
            Target += shift;
        }

        public void SetWinSize(Vector2f size)
        {
            WinSize = size;
        }

        public FloatRect GetBorders()
        {
            var topleft = (Target - WinSize * Scale / 2);
            return new FloatRect(topleft, WinSize * Scale);
        }

        public Vector2f GetRelativePosition(Vector2f absPos)
        {
            return (absPos - Target + WinSize / 2);
        }

        public void Rescale(float delta)
        {
            Scale *= (float)System.Math.Pow(1.1d, delta);
        }
    }
}