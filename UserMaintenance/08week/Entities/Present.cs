﻿using _08week.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08week.Entities
{
   public class Present : Toy
    {

        public SolidBrush RibbonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }
        public Present(Color ribboncolor, Color boxcolor)
        {
            RibbonColor = new SolidBrush(ribboncolor);

            BoxColor = new SolidBrush(boxcolor);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, 5, 10, Width, Height);
            g.FillRectangle(RibbonColor, 10, 5, Width, Height);

        }
    }
}
