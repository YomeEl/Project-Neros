using System.Collections.Generic;
using System.IO;

using SFML.Graphics;

namespace Project_Neros.Game
{
    static class SpriteAtlas
    {
        public static Dictionary<string, Sprite> Sprites { get; } = new Dictionary<string, Sprite>();

        public static void LoadSprites()
        {
            Engine.Logger.Log("Loading sprites...");
            foreach (string directory in Directory.EnumerateDirectories("Sprites"))
            {
                foreach (string fname in Directory.EnumerateFiles(directory))
                {
                    string ext = fname.Substring(fname.IndexOf('.'));
                    if (ext == ".png")
                    {
                        var sprite = new Sprite(new Texture(fname));
                        string name = "";
                        string[] path = fname.Split('\\');
                        for (int i = 1; i < path.Length; i++)
                        {
                            if (i != path.Length - 1)
                            {
                                name += path[i] + '.';
                            }
                            else
                            {
                                name += path[i].Split('.')[0];
                            }
                        }
                        Sprites.Add(name, sprite);
                        Engine.Logger.Log($"\tLoaded {fname} as {name}");
                    }
                }
            }
            Engine.Logger.Log("Done loading sprites!");
        }
    }
}