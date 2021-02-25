using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class RevisionType
    {

        private String Name, Info;
        private int YearLoop;

        public RevisionType(String Name, int YearLoop, String Info)
        {
            this.Name = Name;
            this.YearLoop = YearLoop;
            this.Info = Info;
        }

        // GETTERS
        public String GetName()
        {
            return Name;
        }
        public int GetYearLoop()
        {
            return YearLoop;
        }
        public String GetInfo()
        {
            return Info;
        }

        // SETTERS
        public void SetYearLoop(int YearLoop)
        {
            this.YearLoop = YearLoop;
        }
        public void SetInfo(String Info)
        {
            this.Info = Info;
        }

    }
}
