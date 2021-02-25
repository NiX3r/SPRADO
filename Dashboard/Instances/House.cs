using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class House
    {

        int key;
        String name;

        public House(int key, String name)
        {
            this.key = key;
            this.name = name;
        }

        // SETTERS
        public void SetName(String name)
        {
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
