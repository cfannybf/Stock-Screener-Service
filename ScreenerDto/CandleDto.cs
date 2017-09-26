using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerDto
{
    [DataContract]
    public class CandleDto
    {
        [DataMember]
        public decimal Open { get; set; }

        [DataMember]
        public decimal High { get; set; }

        [DataMember]
        public decimal Low { get; set; }

        [DataMember]
        public decimal Close { get; set; }

        [DataMember]
        public decimal Volume { get; set; }
    }
}
