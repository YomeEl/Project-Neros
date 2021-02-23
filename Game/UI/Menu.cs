using Project_Neros.Engine;
using Project_Neros.Game.UI.Actors;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using System.Xml;

namespace Project_Neros.Game.UI
{
    class Menu : Scene
    {
        private Sprite bg;
        private Sprite title;
        private float titleTop;

        public Menu(RenderWindow win) : base(win)
        {
            LoadFromXML();
            InitializeElements();
            win.Resized += OnWinResized;
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
            camera.Target = (Vector2f)win.Size / 2;

            bg.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var mx = (win.Size.X - bg.Texture.Size.X * bg.Scale.X) / 2;
            var my = (win.Size.Y - bg.Texture.Size.Y * bg.Scale.Y) / 2;
            bg.Position = new Vector2f(mx, my);

            title.Scale = new Vector2f(1, 1) * win.Size.X / bg.Texture.Size.X;
            var tx = (win.Size.X - title.Texture.Size.X * title.Scale.X) / 2;
            var ty = (win.Size.Y - title.Texture.Size.Y * title.Scale.Y) * titleTop;
            title.Position = new Vector2f(tx, ty);
        }

        private void LoadFromXML()
        {
            Logger.Log("Loading menu...");

            var doc = new XmlDocument();
            doc.Load("Markup//Menu.xml");
            var mainPageNode = doc.DocumentElement.SelectSingleNode("mainPage");
            
            if (mainPageNode != null)
            {
                foreach (XmlNode node in mainPageNode.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "bg":
                            bg = SpriteAtlas.Sprites[node.InnerText];
                            Logger.Log("\tLoaded background");
                            break;

                        case "title":
                            title = SpriteAtlas.Sprites[node.InnerText];
                            titleTop = float.Parse(node.Attributes.GetNamedItem("top").Value);
                            Logger.Log("\tLoaded title");
                            break;

                        case "actors":
                            int count = 0;
                            foreach (XmlNode actorNode in node.ChildNodes)
                            {
                                AddActor(new Button(win, actorNode));
                                count++;
                            }
                            Logger.Log($"\tLoaded {count} actors");
                            break;
                    }
                }
            }
            else
            {
                Logger.Log("\t Can't find main page markup!");
            }

            Logger.Log("Done loading menu!");
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                if (actorBounds.Contains(e.X, e.Y))
                {
                    if (actor.OnClick().Length != 0)
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
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            foreach (IActor actor in actors)
            {
                var actorBounds = actor.GetMapBounds();
                actor.Selected = actorBounds.Contains(e.X, e.Y);
            }
        }

        private void OnWinResized(object sender, SizeEventArgs e)
        {
            InitializeElements();
        }
    }
}