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
            win.MouseWheelScrolled += OnMouseWheelScrolled;
        }

        public override void Draw()
        {
            DrawTiledBackground();

            base.Draw();
        }

        public override void Step()
        {
            MovePlayer();
            camera.Target = player.Position;
        }

        protected override void InitializeElements()
        {
            ground = SpriteAtlas.Sprites["Ground.Ground"];
            player = new Player();
            AddActor(player);
            camera.Target = player.Position;
            var sp = new Building("SolarPanel");
            sp.Position = new Vector2f(300, 300);
            AddActor(sp);
        }

        protected override void Deactivate()
        {
            IsActive = false;

            win.KeyPressed -= OnKeyPressed;
            win.KeyReleased -= OnKeyReleased;
            win.MouseWheelScrolled -= OnMouseWheelScrolled;
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
                case Keyboard.Key.Add:
                    camera.Rescale(1);
                    break;
                case Keyboard.Key.Subtract:
                    camera.Rescale(-1);
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
        
        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            camera.Rescale(e.Delta);
        }

        private void DrawTiledBackground()
        {
            var cameraBorders = camera.GetBorders();
            ground.Scale = new Vector2f(1, 1) * camera.Scale;
            var groundSize = (Vector2f)ground.Texture.Size * camera.Scale;
            var shiftLeft = cameraBorders.Left % groundSize.X;
            var shiftTop = cameraBorders.Top % groundSize.Y;
            uint countX = win.Size.X / (uint)groundSize.X + 2;
            uint countY = win.Size.Y / (uint)groundSize.Y + 2;
            
            for (int j = -1; j < countY; j++)
            {
                for (int i = -1; i < countX; i++)
                {
                    ground.Position = new Vector2f(i * groundSize.X - shiftLeft, j * groundSize.Y - shiftTop);
                    win.Draw(ground);
                }
            }
        }

        private void MovePlayer()
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

            var movement = BinaryDirectionToVectorAndDirection(dirBin);
            player.SetDirection(movement.direction);
            player.Position += movement.vector;
            if (CheckCollisionsFor(player))
            {
                player.Position -= movement.vector;
            }
        }

        private (Vector2f vector, Direction? direction)  BinaryDirectionToVectorAndDirection(int dirBin)
        {
            Direction? dir;
            Vector2f vec = new Vector2f(0, 0);
            switch (dirBin)
            {
                case 0b0001:
                    dir = Direction.Up;
                    vec = new Vector2f(0, -1) * player.Speed;
                    break;
                case 0b0011:
                    dir = Direction.UpRight;
                    vec = new Vector2f(1, -1) * player.Speed / 2;
                    break;
                case 0b0010:
                    dir = Direction.Right;
                    vec = new Vector2f(1, 0) * player.Speed;
                    break;
                case 0b0110:
                    dir = Direction.DownRight;
                    vec = new Vector2f(1, 1) * player.Speed / 2;
                    break;
                case 0b0100:
                    dir = Direction.Down;
                    vec = new Vector2f(0, 1) * player.Speed;
                    break;
                case 0b1100:
                    dir = Direction.DownLeft;
                    vec = new Vector2f(-1, 1) * player.Speed / 2;
                    break;
                case 0b1000:
                    dir = Direction.Left;
                    vec = new Vector2f(-1, 0) * player.Speed;
                    break;
                case 0b1001:
                    dir = Direction.UpLeft;
                    vec = new Vector2f(-1, -1) * player.Speed / 2;
                    break;
                default:
                    dir = null;
                    break;
            }
            return (vec, dir);
        }

        private bool IsActorsCollide(IActor actor1, IActor actor2)
        {
            return actor1 != actor2 && actor1.GetMapBounds().Intersects(actor2.GetMapBounds());
        }

        private bool CheckCollisionsFor(IActor actor)
        {
            bool res = false;
            foreach (IActor act in actors)
            {
                res = IsActorsCollide(act, actor);
            }
            return res;
        }
    }
}