using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minigis_Surkov
{
    public class Symbol
    {
        public int size;
        public System.Drawing.Color color;
        public string font;
        public byte number;

        public Symbol()
        {
            size = 26;
            color = System.Drawing.Color.Purple;
            font = "Wingdings";
            number = 0x59;
        }
    }
}