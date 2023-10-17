using Inventec.Common.Logging;
using MOS.OldSystem.HmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class InvoiceConsumer
    {
        private IeMRMainServiceClient client;

        public InvoiceConsumer(string baseUri)
        {
            this.client = new IeMRMainServiceClient("WSHttpBinding_IeMRMainService", baseUri);
        }

        public long? Get(string templateCode)
        {
            string rs = "";
            try
            {
                rs = this.client.srv_hms_get_ordernumber(templateCode);
                return long.Parse(rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Goi sang he thong PMS de lay so hoa don. Input: " + templateCode + ". Ket qua: " + rs);
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}
