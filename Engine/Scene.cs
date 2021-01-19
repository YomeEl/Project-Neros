using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace Project_Neros.Engine
{
    abstract class Scene
    {
        public bool IsActive { get; protected set; } = true;

        protected List<IActor> actors;
        protected Camera camera;
        protected readonly RenderWindow win;

        public Scene(RenderWindow win)
        {
            this.win = win;
            camera = new Camera(new SFML.System.Vector2f(win.Size.X, win.Size.Y));
            actors = new List<IActor>();
            win.Resized += WinResized;
        }

        /// <summary>
        /// Set IsActive to true and subscribe to events
        /// </summary>
        public abstract void Activate();

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
                    actor.Draw(camera.GetRelativePosition(actor.Position), camera.Scale, win);
                }
            }
        }

        protected abstract void InitializeElements();

        /// <summary>
        /// Set IsActive to false and unsubscribe from events
        /// </summary>
        protected abstract void Deactivate();

        private void WinResized(object sender, SizeEventArgs e)
        {
            var screenRect = new FloatRect(0, 0, e.Width, e.Height);
            win.SetView(new View(screenRect));
            camera.WinSize = new SFML.System.Vector2f(screenRect.Width, screenRect.Height);
        }
    }
}
