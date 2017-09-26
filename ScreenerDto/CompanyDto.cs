using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerDto
{
    [DataContract]
    public class CompanyDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public CandleDto[] Chart { get; set; }
    }
}
