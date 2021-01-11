using Project_Neros.Game.World.Actors;

using System.Collections.Generic;
using System.Xml;

using SFML.Graphics;

namespace Project_Neros.Game
{
    static class BuildingAtlas
    {
        public static Dictionary<string, Building> Buildings { get; } = new Dictionary<string, Building>();

        public static void LoadBuildings()
        {
            Engine.Logger.Log("Loading buildings...");

            var doc = new XmlDocument();
            doc.Load("Objects//Buildings.xml");
            var root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                var building = new Building(node);
                Buildings[building.TypeName] = building;
                Engine.Logger.Log($"\tLoaded {building.TypeName}");
            }

            Engine.Logger.Log("Done loading buildings!");
        }
    }
}