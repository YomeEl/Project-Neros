using System.Collections.Generic;

using SFML.Graphics;

namespace Project_Neros.Engine
{
    abstract class Field
    {
        protected readonly List<IActor> actors;
        protected readonly Camera camera;
        protected readonly RenderWindow win;

        public Field(RenderWindow win)
        {
            this.win = win;
            camera = new Camera(win.GetView().Size);
            actors = new List<IActor>();
            win.Resized += WinResized;
            win.MouseButtonPressed += OnClick;
        }

        public void OnClick(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                if (actorBounds.Contains(e.X, e.Y))
                {
                    actor.OnClick();
                }
            }
        }

        public void OnMouseMove(object sender, SFML.Window.MouseMoveEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                actor.Selected = actorBounds.Contains(e.X, e.Y);
            }
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

        private void WinResized(object sender, SFML.Window.SizeEventArgs e)
        {
            camera.SetWinSize(new SFML.System.Vector2f(e.Width, e.Height));
        }

    }
}
