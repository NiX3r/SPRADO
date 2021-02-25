using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Instances
{
    class RevisionForList
    {

        private DateTime date;
        private String house;
        private String type;
        private String man;
        private String info;
        private int yearLoop;

        public RevisionForList(DateTime date, String house, String type, String man, String info, int yearLoop)
        {
            this.date = date;
            this.house = house;
            this.type = type;
            this.man = man;
            this.info = info;
            this.yearLoop = yearLoop;
        }

        public DateTime GetDate()
        {
            return this.date;
        }
        public String GetHouse()
        {
            return this.house;
        }
        public String GetRevType()
        {
            return this.type;
        }
        public String GetMan()
        {
            return this.man;
        }
        public String GetInfo()
        {
            return this.info;
        }
        public int GetYearLoop()
        {
            return this.yearLoop;
        }

    }
}
