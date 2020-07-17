using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLastHope.Management
{
    [Serializable]
    public class Player
    {
        public int Credit { get; set; }
        public string CurrentPosition { get; set; }

        public Player(int credit)
        {
            Credit = credit;
        }
    }
}
