﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLD.BOT.BufferSpace
{
     abstract class PLD
     {
        public string tapeName { get; set; }
        public double position { get; set; }
        public ushort errVacuum { get; set; }
        public ushort errReel { get; set; }
        public double speed { get; set; }
        public double length { get; set; }
        public double lengthSet { get; set; }
        public double runTimes{ get;  set; }
        public double runTimesSet { get; set; }
        public bool procesStart { get; set; }
        public bool[] SendFlags { get; set; }
        public PLD()
        {
            SendFlags = new bool[40];
        }
    }
}
