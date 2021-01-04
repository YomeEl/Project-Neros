using Project_Neros.Engine;
using Project_Neros.Game.UI.Actors;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Project_Neros.Game.UI
{
    class Menu : Scene
    {
        private Sprite bg;
        private Sprite title;

        public Menu(RenderWindow win) : base(win)
        {
            InitializeElements();
        }

        public override void Activate()
        {
            IsActive = true;

            win.MouseButtonPressed += OnClick;
            win.MouseMoved += OnMouseMove;
        }

        public override void Draw()
        {
            win.Draw(bg);
            win.Draw(title);
            base.Draw();
        }

        public override void Step() { }

        protected override void Deactivate()
        {
            IsActive = false;
            
            win.MouseButtonPressed -= OnClick;
            win.MouseMoved -= OnMouseMove;
        }

        protected override void InitializeElements()
        {
            camera.Move((Vector2f)win.Size / 2);

            bg = SpriteAtlas.Sprites["Menu.Bg"];
            bg.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var mx = (win.Size.X - bg.Texture.Size.X * bg.Scale.X) / 2;
            var my = (win.Size.Y - bg.Texture.Size.Y * bg.Scale.Y) / 2;
            bg.Position = new Vector2f(mx, my);

            float title_top = 0.1f;
            title = SpriteAtlas.Sprites["Menu.Title"];
            title.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var tx = (win.Size.X - title.Texture.Size.X * title.Scale.X) / 2;
            var ty = (win.Size.Y - title.Texture.Size.Y * title.Scale.Y) * title_top;
            title.Position = new Vector2f(tx, ty);

            var factory = new ButtonFactory(win);
            float top = 0.85f;
            AddActor(factory.CreateNewGameButton(new Vector2f(0.2f, top)));
            AddActor(factory.CreateLoadButton(new Vector2f(0.5f, top)));
            AddActor(factory.CreateQuitButton(new Vector2f(0.8f, top)));
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                if (actorBounds.Contains(e.X, e.Y))
                {
                    switch (actor.OnClick()[0].type)
                    {
                        case CommandType.StartNew:
                            Deactivate();
                            break;

                        case CommandType.Quit:
                            win.Close();
                            break;
                    }
                }
            }
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                actor.Selected = actorBounds.Contains(e.X, e.Y);
            }
        }
    }
}