using Project_Neros.Engine;
using SFML.Graphics;
using SFML.System;

namespace Project_Neros.Game.World.Actors
{
    class Player : IActor
    {
        public Vector2f Position { get; set; }
        public float Speed { get; set; }
        public bool Selected { get; set; }

        private Direction direction;
        private Sprite activeSprite;
        private readonly Vector2f size = new Vector2f(100, 100);

        public Player()
        {
            Position = new Vector2f(0, 0);
            Speed = 10f;
            SetDirection(Direction.Up);
        }

        public void SetDirection(Direction? direction)
        {
            if (direction != null)
            {
                this.direction = direction.GetValueOrDefault();
                switch (this.direction)
                {
                    case Direction.Up:
                        activeSprite = SpriteAtlas.Sprites["Player.U"];
                        break;
                    case Direction.UpRight:
                        activeSprite = SpriteAtlas.Sprites["Player.UR"];
                        break;
                    case Direction.Right:
                        activeSprite = SpriteAtlas.Sprites["Player.R"];
                        break;
                    case Direction.DownRight:
                        activeSprite = SpriteAtlas.Sprites["Player.DR"];
                        break;
                    case Direction.Down:
                        activeSprite = SpriteAtlas.Sprites["Player.D"];
                        break;
                    case Direction.DownLeft:
                        activeSprite = SpriteAtlas.Sprites["Player.DL"];
                        break;
                    case Direction.Left:
                        activeSprite = SpriteAtlas.Sprites["Player.L"];
                        break;
                    case Direction.UpLeft:
                        activeSprite = SpriteAtlas.Sprites["Player.UL"];
                        break;
                }
            }
            
        }

        public Command[] OnClick() => new Command[] { };

        public FloatRect GetMapBounds()
        {
            var topLeft = Position - size / 2;
            var bounds = new FloatRect(topLeft, size);
            return bounds;
        }

        public void Draw(Vector2f position, float scale, RenderWindow win)
        {
            activeSprite.Position = position - size * scale / 2;
            var sSize = activeSprite.Texture.Size;
            var sizeScale = new Vector2f(size.X / sSize.X, size.Y / sSize.Y);
            activeSprite.Scale = sizeScale * scale;
            win.Draw(activeSprite);
        }
    }
}
