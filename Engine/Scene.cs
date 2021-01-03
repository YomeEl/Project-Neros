using System.Collections.Generic;

using SFML.Graphics;

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
        }

        public void AddActor(IActor actor) => actors.Add(actor);

        public void RemoveActor(IActor actor) => actors.Remove(actor);

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

        protected abstract void OnClick(object sender, SFML.Window.MouseButtonEventArgs e);

        protected abstract void OnMouseMove(object sender, SFML.Window.MouseMoveEventArgs e);

        protected void Deactivate()
        {
            IsActive = false;

            win.MouseButtonPressed -= OnClick;
            win.MouseMoved -= OnMouseMove;
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
