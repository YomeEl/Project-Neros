using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace Project_Neros.Engine
{
    abstract class Scene
    {
        public bool IsActive { get; private set; } = true;

        protected List<IActor> actors;
        protected Camera camera;
        protected readonly RenderWindow win;

        public Scene(RenderWindow win)
        {
            this.win = win;
            Reset();
            win.Resized += WinResized;
        }

        public void Activate()
        {
            IsActive = true;

            win.MouseButtonPressed += OnClick;
            win.MouseMoved += OnMouseMove;
            win.KeyPressed += OnKeyPressed;
        }

        public void AddActor(IActor actor) => actors.Add(actor);

        public void RemoveActor(IActor actor) => actors.Remove(actor);

        public abstract void Step();

        public virtual void Draw()
        {
            var viewRect = camera.GetBorders();
            foreach (IActor actor in actors)
            {
                if (actor.GetMapBounds().Intersects(viewRect))
                {
                    actor.Draw(camera.GetRelativePosition(actor.Position), win);
                }
            }
        }

        protected abstract void InitializeElements();

        protected abstract void OnClick(object sender, MouseButtonEventArgs e);

        protected abstract void OnMouseMove(object sender, MouseMoveEventArgs e);

        protected abstract void OnKeyPressed(object sender, KeyEventArgs e);

        protected void Deactivate()
        {
            IsActive = false;

            win.MouseButtonPressed -= OnClick;
            win.MouseMoved -= OnMouseMove;
            win.KeyPressed -= OnKeyPressed;
        }

        private void Reset()
        {
            camera = new Camera(new SFML.System.Vector2f(win.Size.X, win.Size.Y));
            actors = new List<IActor>();
        }

        private void WinResized(object sender, SFML.Window.SizeEventArgs e)
        {
            win.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
            Reset();
            InitializeElements();
        }
    }
}
