using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GratitudeApp.ViewModel
{
    public class TaloKirjaus
    {
        public int Talo_Id { get; set; }
        public string Teksti { get; set; }
        public DateTime pvm { get; set; }

        public string Linkki { get; set; }
        public int Kirjaaja_id { get; set; }
        public int tarkeys { get; set; }
        public int Kirjaus_id { get; set; }
    }
}