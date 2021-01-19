using Project_Neros.Engine;

using SFML.Graphics;
using SFML.System;

namespace Project_Neros.Game.UI.Actors
{
    class Button : IActor
    {
        public Vector2f Position { get; set; }
        public Vector2f RelativePosition { get; set; }
        public bool Selected { get; set; } = false;

        private readonly RenderWindow win;
        private readonly Sprite sprite;
        private readonly Sprite hoverSprite;
        private readonly Command[] commands;

        public Button(RenderWindow win, Sprite sprite, Sprite hoverSprite, Command[] commands)
        {
            this.win = win;
            this.sprite = sprite;
            this.hoverSprite = hoverSprite;
            this.commands = commands;
        }

        public void Draw(Vector2f position, float scale, RenderWindow win)
        {
            var actSprite = Selected ? hoverSprite : sprite;
            var topLeftPos = new Vector2f(win.Size.X * RelativePosition.X, win.Size.Y * RelativePosition.Y);
            var shift = (Vector2f)actSprite.Texture.Size / 2;
            actSprite.Position = topLeftPos - shift;
            actSprite.Draw(win, RenderStates.Default);
        }

        public FloatRect GetMapBounds()
        { 
            var topLeftPos = new Vector2f(win.Size.X * RelativePosition.X, win.Size.Y * RelativePosition.Y);
            var shift = (Vector2f)sprite.Texture.Size / 2;
            var bounds = new FloatRect(topLeftPos - shift, (Vector2f)sprite.Texture.Size);
            return bounds;
        }

        public Command[] OnClick()
        {
            return commands;
        }
    }
}