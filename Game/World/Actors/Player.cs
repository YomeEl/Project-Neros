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

        public Player()
        {
            Position = new Vector2f(0, 0);
            Speed = 5f;
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
            var topLeft = Position - (Vector2f)activeSprite.Texture.Size / 2;
            var bounds = new FloatRect(topLeft, (Vector2f)activeSprite.Texture.Size);
            return bounds;
        }

        public void Draw(Vector2f position, RenderWindow win)
        {
            activeSprite.Position = position - (Vector2f)activeSprite.Texture.Size / 2;
            win.Draw(activeSprite, RenderStates.Default);
        }
    }
}
