using Project_Neros.Engine;
using Project_Neros.Game.UI.Actors;

using SFML.Graphics;
using SFML.System;

namespace Project_Neros.Game.UI
{
    class Menu : Scene
    {
        private readonly Sprite bg;

        public Menu(RenderWindow win) : base(win)
        {
            camera.Move((Vector2f)win.Size / 2);

            bg = SpriteAtlas.Sprites["Menu.bg"];
            bg.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var x = (win.Size.X - bg.Texture.Size.X * bg.Scale.X) / 2;
            var y = (win.Size.Y - bg.Texture.Size.Y * bg.Scale.Y) / 2;
            bg.Position = new Vector2f(x, y);

            var factory = new ButtonFactory(win);
            float top = 0.78f + (win.Size.Y - 600) / 1000f;
            AddActor(factory.CreateNewGameButton(new Vector2f(0.2f, top)));
            AddActor(factory.CreateLoadButton(new Vector2f(0.5f, top)));
            AddActor(factory.CreateQuitButton(new Vector2f(0.8f, top)));
        }

        override public void Draw()
        {
            win.Draw(bg);
            base.Draw();
        }
    }
}
