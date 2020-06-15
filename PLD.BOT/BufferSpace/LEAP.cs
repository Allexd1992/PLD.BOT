using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLD.BOT.BufferSpace
{
    abstract class LEAP
    {
        public string OpModMsg { get; set; }
        public double OpModCode{ get; set; }
        public double OpModCodeOld { get; set; }
        public double Egy { get; set; }
        public double EgyOld { get; set; }
        public double Hv { get; set; }
        public double HvOld { get; set; }
        public bool sideA { get; set; }
        public double counterNewFill { get; set; }

    }
}
