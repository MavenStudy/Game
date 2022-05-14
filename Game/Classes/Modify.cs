﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Modify
    {
        public PointF position;
        public Size size;

        public Modify(PointF position, Size size)
        {
            this.position = position;
            this.size = size;
        }
    }
}