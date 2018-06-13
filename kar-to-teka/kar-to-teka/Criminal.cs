using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kar_to_teka
{
    class Criminal
    {
        public string id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string data_urodzenia { get; set; }
        public string miejsce_urodzenia { get; set; }
        public string miejsce_zamieszkania { get; set; }
        public bool poszukiwany { get; set; }
        public string pseudonim { get; set; }
    }
}
