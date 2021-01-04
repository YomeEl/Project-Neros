using Project_Neros.Engine;

using SFML.Window;
using SFML.Graphics;

namespace Project_Neros.Game
{
    class Controller
    {
        public void Start()
        {
            SpriteAtlas.LoadSprites();

            var win = new RenderWindow(new VideoMode(1200, 600), "Project Neros");
            win.Position = new SFML.System.Vector2i(0, 0);
            win.SetFramerateLimit(60);

            var menu = new UI.Menu(win);
            var surface = new World.Surface(win);

            menu.Activate();
            Scene activeScene = menu;

            while (win.IsOpen)
            {
                if (!activeScene.IsActive)
                {
                    activeScene = surface;
                    surface.Activate();
                }

                win.Clear();
                activeScene.Draw();
                activeScene.Step();
                win.Display();
                win.DispatchEvents();
            }
        }
    }
}
