using Project_Neros.Engine;
using Project_Neros.Game.World.Actors;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Project_Neros.Game.World
{
    class Surface : Scene
    {
        private Sprite ground;
        private Player player;

        private bool moveUp = false;
        private bool moveRight = false;
        private bool moveDown = false;
        private bool moveLeft = false;

        public Surface(RenderWindow win) : base(win)
        {
            InitializeElements();
        }

        public override void Activate()
        {
            IsActive = true;

            win.KeyPressed += OnKeyPressed;
            win.KeyReleased += OnKeyReleased;
        }

        public override void Draw()
        {
            DrawTiledBackground();

            base.Draw();
        }

        public override void Step()
        {
            int dirBin = 0;
            if (moveUp)
            {
                dirBin += 0b0001;
            }
            if (moveRight)
            {
                dirBin += 0b0010;
            }
            if (moveDown)
            {
                dirBin += 0b0100;
            }
            if (moveLeft)
            {
                dirBin += 0b1000;
            }

            if ((dirBin & 0b0101) == 0b0101)
            {
                dirBin &= 0b1010;
            }
            if ((dirBin & 0b1010) == 0b1010)
            {
                dirBin &= 0b0101;
            }

            Direction? dir = null;
            switch (dirBin)
            {
                case 0b0001:
                    dir = Direction.Up;
                    player.Position += new Vector2f(0, -1) * player.Speed;
                    break;
                case 0b0011:
                    dir = Direction.UpRight;
                    player.Position += new Vector2f(1, -1) * player.Speed / 2;
                    break;
                case 0b0010:
                    dir = Direction.Right;
                    player.Position += new Vector2f(1, 0) * player.Speed;
                    break;
                case 0b0110:
                    dir = Direction.DownRight;
                    player.Position += new Vector2f(1, 1) * player.Speed / 2;
                    break;
                case 0b0100:
                    dir = Direction.Down;
                    player.Position += new Vector2f(0, 1) * player.Speed;
                    break;
                case 0b1100:
                    dir = Direction.DownLeft;
                    player.Position += new Vector2f(-1, 1) * player.Speed / 2;
                    break;
                case 0b1000:
                    dir = Direction.Left;
                    player.Position += new Vector2f(-1, 0) * player.Speed;
                    break;
                case 0b1001:
                    dir = Direction.UpLeft;
                    player.Position += new Vector2f(-1, -1) * player.Speed / 2;
                    break;
                default:
                    dir = null;
                    break;

            }
            player.SetDirection(dir);

            camera.SetTarget(player.Position);
        }

        protected override void InitializeElements()
        {
            camera.Move((Vector2f)win.Size / 2);
            ground = SpriteAtlas.Sprites["Ground.Ground"];
            player = new Player();
            AddActor(player);
        }

        protected override void Deactivate()
        {
            IsActive = false;

            win.KeyPressed -= OnKeyPressed;
            win.KeyReleased -= OnKeyReleased;
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    moveUp = true;
                    break;
                case Keyboard.Key.D:
                    moveRight = true;
                    break;
                case Keyboard.Key.S:
                    moveDown = true;
                    break;
                case Keyboard.Key.A:
                    moveLeft = true;
                    break;
            }
        }

        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    moveUp = false;
                    break;
                case Keyboard.Key.D:
                    moveRight = false;
                    break;
                case Keyboard.Key.S:
                    moveDown = false;
                    break;
                case Keyboard.Key.A:
                    moveLeft = false;
                    break;
            }
        }

        private void DrawTiledBackground()
        {
            var cameraBorders = camera.GetBorders();
            var shiftLeft = cameraBorders.Left % ground.Texture.Size.X;
            var shiftTop = cameraBorders.Top % ground.Texture.Size.Y;
            uint countX = win.Size.X / ground.Texture.Size.X + 2;
            uint countY = win.Size.Y / ground.Texture.Size.Y + 2;
            
            for (int j = -1; j < countY; j++)
            {
                for (int i = -1; i < countX; i++)
                {
                    ground.Position = new Vector2f(i * ground.Texture.Size.X - shiftLeft, j * ground.Texture.Size.Y - shiftTop);
                    win.Draw(ground);
                }
            }
        }
    }
}
