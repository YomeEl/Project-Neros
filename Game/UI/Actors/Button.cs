using Project_Neros.Engine;

using SFML.Graphics;
using SFML.System;

using System.Xml;

namespace Project_Neros.Game.UI.Actors
{
    class Button : IActor
    {
        public Vector2f Position { get; set; }
        public Vector2f RelativePosition { get; set; }
        public bool Selected { get; set; } = false;

        private readonly RenderWindow win;
        private readonly Sprite sprite;
        private readonly Sprite hoverSprite;
        private readonly Command[] commands;

        public Button(RenderWindow win, XmlNode xmlNode)
        {
            this.win = win;
            commands = new Command[0];

            try
            {
                if (xmlNode.Attributes["class"].Value == "button")
                {
                    foreach (XmlNode node in xmlNode.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "pos":
                                var left = float.Parse(node.Attributes["left"].Value);
                                var top = float.Parse(node.Attributes["top"].Value);
                                RelativePosition = new Vector2f(left, top);
                                break;
                            case "sprite":
                                sprite = SpriteAtlas.Sprites[node.InnerText];
                                break;
                            case "hoverSprite":
                                hoverSprite = SpriteAtlas.Sprites[node.InnerText];
                                break;
                            case "command":
                                commands = new Command[]
                                {
                                    new Command()
                                    {
                                        type = (CommandType)System.Enum.Parse(typeof(CommandType), node.InnerText)
                                    }
                                };
                                break;
                        }
                    }
                    Logger.Log("\tLoaded button");
                }
            }
            catch
            {
                Logger.Log($"Wrong XML template for {xmlNode.Name}");
            }
        }

        public void Draw(Vector2f position, float scale, RenderWindow win)
        {
            var actSprite = Selected ? hoverSprite : sprite;
            var topLeftPos = new Vector2f(win.Size.X * RelativePosition.X, win.Size.Y * RelativePosition.Y);
            var shift = (Vector2f)actSprite.Texture.Size / 2;
            actSprite.Position = topLeftPos - shift;
            actSprite.Draw(win, RenderStates.Default);
        }

        public FloatRect GetMapBounds()
        { 
            var topLeftPos = new Vector2f(win.Size.X * RelativePosition.X, win.Size.Y * RelativePosition.Y);
            var shift = (Vector2f)sprite.Texture.Size / 2;
            var bounds = new FloatRect(topLeftPos - shift, (Vector2f)sprite.Texture.Size);
            return bounds;
        }

        public Command[] OnClick()
        {
            return commands;
        }
    }
}