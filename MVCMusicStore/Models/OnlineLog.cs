using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
    public class OnlineLog
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime LastHeartBeat { get; set; }

        public bool IsOnlineNow()
        {
            if ((DateTime.Now - LastHeartBeat).Seconds < 15)
                return true;
            else
                return false;
        }
    }
}
