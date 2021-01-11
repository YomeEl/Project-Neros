using Project_Neros.Engine;

using SFML.Graphics;
using SFML.System;

using System.Xml;

namespace Project_Neros.Game.World.Actors
{
    class Building : IActor
    {
        public string TypeName { get; set; }
        public Vector2f Position { get; set; }
        public bool Selected { get; set; }

        private Sprite sprite;
        private Vector2f size;

        public Building(XmlNode xmlNode)
        {
            try
            {
                TypeName = xmlNode.Attributes.GetNamedItem("typeName").Value;
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "sprite":
                            sprite = SpriteAtlas.Sprites[node.InnerText];
                            break;

                        case "size":
                            var x = float.Parse(node.Attributes.GetNamedItem("x").Value);
                            var y = float.Parse(node.Attributes.GetNamedItem("y").Value);
                            size = new Vector2f(x, y);
                            break;
                    }

                }
            }
            catch
            {
                Logger.Log($"Wrong XML template for {xmlNode.Name} named {xmlNode.Attributes.GetNamedItem(TypeName)}");
            }
        }

        public Building(string typeName)
        {
            var building = BuildingAtlas.Buildings[typeName];
            TypeName = building.TypeName;
            Position = new Vector2f();
            sprite = building.sprite;
            size = building.size;
        }

        public void Draw(Vector2f position, RenderWindow win)
        {
            sprite.Position = position - size / 2;
            var sSize = sprite.Texture.Size;
            var scale = new Vector2f(size.X / sSize.X, size.Y / sSize.Y);
            sprite.Scale = scale;
            win.Draw(sprite);
        }

        public FloatRect GetMapBounds()
        {
            var topLeft = Position - (Vector2f)sprite.Texture.Size / 2;
            var bounds = new FloatRect(topLeft, (Vector2f)sprite.Texture.Size);
            return bounds;
        }

        public Command[] OnClick()
        {
            return new Command[] { };
        }
    }
}
