using Project_Neros.Engine;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Project_Neros.Game.World
{
    class Surface : Scene
    {
        Sprite ground;

        public Surface(RenderWindow win) : base(win)
        {
            InitializeElements();
        }

        public override void Draw()
        {
            DrawTiledBackground();

            base.Draw();
        }

        protected override void InitializeElements()
        {
            camera.Move((Vector2f)win.Size / 2);
            ground = SpriteAtlas.Sprites["Ground.Ground"];
        }

        protected override void OnClick(object sender, MouseButtonEventArgs e)
        {
        }

        protected override void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
        }

        private void DrawTiledBackground()
        {
            var cameraBorders = camera.GetBorders();
            var shiftLeft = cameraBorders.Left % ground.Texture.Size.X;
            var shiftTop = cameraBorders.Top % ground.Texture.Size.Y;
            uint countX = win.Size.X / ground.Texture.Size.X + 2;
            uint countY = win.Size.Y / ground.Texture.Size.Y + 2;
            
            for (int j = -1; j < countY - 1; j++)
            {
                for (int i = -1; i < countX - 1; i++)
                {
                    ground.Position = new Vector2f(i * ground.Texture.Size.X - shiftLeft, j * ground.Texture.Size.Y - shiftTop);
                    win.Draw(ground);
                }
            }
        }
    }
}
