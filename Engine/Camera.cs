using SFML.System;
using SFML.Graphics;

namespace Project_Neros.Engine
{
    class Camera
    {
        private float scale = 1f;
        private Vector2f target;
        private Vector2f winSize;

        public Camera(Vector2f winSize, Vector2f? target = null)
        {
            this.winSize = winSize;
            this.target = target.GetValueOrDefault();
        }

        public void Move(Vector2f shift)
        {
            target += shift;
        }

        public void SetWinSize(Vector2f size)
        {
            winSize = size;
        }

        public FloatRect GetBorders()
        {
            var topleft = (target - winSize / 2) * scale;
            return new FloatRect(topleft, winSize * scale);
        }

        public Vector2f GetRelativePosition(Vector2f absPos)
        {
            return (absPos - target + winSize / 2) * scale;
        }

        public void SetTarget(Vector2f position)
        {
            target = position;
        }
    }
}
