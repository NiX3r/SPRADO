using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class RevMan
    {
        private int key;
        private String name;

        public RevMan(int key, String name)
        {
            this.key = key;
            this.name = name;
        }

        // GETTERS
        public int GetKey()
        {
            return this.key;
        }
        public String GetName()
        {
            return this.name;
        }
    }
}
