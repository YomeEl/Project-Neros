using Project_Neros.Engine;
using Project_Neros.Game.UI.Actors;

using SFML.Graphics;
using SFML.System;

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

        override public void Draw()
        {
            win.Draw(bg);
            win.Draw(title);
            base.Draw();
        }

        protected override void InitializeElements()
        {
            camera.Move((Vector2f)win.Size / 2);

            bg = SpriteAtlas.Sprites["Menu.bg"];
            bg.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var mx = (win.Size.X - bg.Texture.Size.X * bg.Scale.X) / 2;
            var my = (win.Size.Y - bg.Texture.Size.Y * bg.Scale.Y) / 2;
            bg.Position = new Vector2f(mx, my);

            float title_top = 0.1f;
            title = SpriteAtlas.Sprites["Menu.title"];
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
    }
}