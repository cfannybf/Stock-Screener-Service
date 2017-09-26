using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QuoteDownloadService
{
    [ServiceContract]
    public interface IQuoteDownload
    {
        [OperationContract]
        string Echo(string message);
    }
}
