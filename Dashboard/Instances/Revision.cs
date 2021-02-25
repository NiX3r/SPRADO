using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class Revision
    {

        private String info;
        private DateTime lastDone;
        private RevMan revisionMan;
        private RevType revisionType;

        public Revision(String info, DateTime lastDone, RevMan revMan, RevType revType)
        {
            this.info = info;
            this.lastDone = lastDone;
            this.revisionMan = revMan;
            this.revisionType = revType;
        }

        // GETTERS
        public String GetInfo()
        {
            return this.info;
        }
        public DateTime GetLastDone()
        {
            return this.lastDone;
        }
        public RevType GetRevisionType()
        {
            return this.revisionType;
        }
        public RevMan GetRevisionMan()
        {
            return this.revisionMan;
        }

        // SETTERS
        public void SetInfo(String info)
        {
            this.info = info;
        }
        public void SetDateTime(DateTime lastDone)
        {
            this.lastDone = lastDone;
        }
        public void SetRevisionMan(RevMan revisionMan)
        {
            this.revisionMan = revisionMan;
        }

    }
}
