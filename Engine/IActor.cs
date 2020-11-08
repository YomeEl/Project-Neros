﻿using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Project_Neros.Engine
{
    interface IActor
    {
        Vector2f Position { get; set; }
        bool Selected { get; set; }

        Queue<ICommand> OnClick();
        FloatRect GetMapBounds();
        void Draw(Vector2f position, RenderWindow win);
    }
}
