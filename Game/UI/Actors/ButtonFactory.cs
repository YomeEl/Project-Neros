using SFML.System;
using SFML.Graphics;

namespace Project_Neros.Game.UI.Actors
{
    class ButtonFactory
    {
        private readonly RenderWindow win;
        public ButtonFactory(RenderWindow win)
        {
            this.win = win;
        }

        public Button CreateNewGameButton(Vector2f RelativePos)
        {
            var sprite = SpriteAtlas.Sprites["Menu.NewGame"];
            var hoverSprite = SpriteAtlas.Sprites["Menu.NewGame_sel"];
            var commands = new Engine.Command[]
            {
                new Engine.Command()
                {
                    type = Engine.CommandType.StartNew
                }
            };
            var button = new Button(win, sprite, hoverSprite, commands);
            button.RelativePosition = RelativePos;
            return button;
        }

        public Button CreateLoadButton(Vector2f RelativePos)
        {
            var sprite = SpriteAtlas.Sprites["Menu.Load"];
            var hoverSprite = SpriteAtlas.Sprites["Menu.Load_sel"];
            var button = new Button(win, sprite, hoverSprite, new Engine.Command[0]);
            button.RelativePosition = RelativePos;
            return button;
        }

        public Button CreateQuitButton(Vector2f RelativePos)
        {
            var sprite = SpriteAtlas.Sprites["Menu.Quit"];
            var hoverSprite = SpriteAtlas.Sprites["Menu.Quit_sel"];
            var commands = new Engine.Command[] 
            { 
                new Engine.Command() 
                { 
                    type = Engine.CommandType.Quit 
                } 
            };
            var button = new Button(win, sprite, hoverSprite, commands);
            button.RelativePosition = RelativePos;
            return button;
        }
    }
}
