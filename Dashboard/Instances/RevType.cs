using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class RevType
    {

        private int key;
        private String name;

        public RevType(int key, String name)
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
