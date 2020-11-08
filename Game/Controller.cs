using SFML.Window;
using SFML.Graphics;

namespace Project_Neros.Game
{
    class Controller
    {
        public void Start()
        {
            SpriteAtlas.LoadSprites();

            var win = new RenderWindow(new VideoMode(1300, 700), "Project Neros");
            win.Position = new SFML.System.Vector2i(0, 0);
            win.SetFramerateLimit(60);

            var menu = new UI.Menu(win);
            win.MouseButtonPressed += menu.OnClick;
            win.MouseMoved += menu.OnMouseMove;

            while (win.IsOpen)
            {
                win.Clear();
                menu.Draw();
                win.Display();
                win.WaitAndDispatchEvents();
            }
        }
    }
}
