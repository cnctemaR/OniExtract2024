using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutClusterTraveler
    {
        public bool quickTravelToAsteroidIfInOrbit = true;
        public bool revealsFogOfWarAsItTravels = true;
        public bool stopAndNotifyWhenPathChanges;

        public OutClusterTraveler(ClusterTraveler obj)
        {
            this.quickTravelToAsteroidIfInOrbit = obj.quickTravelToAsteroidIfInOrbit;
            this.revealsFogOfWarAsItTravels = obj.revealsFogOfWarAsItTravels;
            this.stopAndNotifyWhenPathChanges = obj.stopAndNotifyWhenPathChanges;
        }
    }
}
