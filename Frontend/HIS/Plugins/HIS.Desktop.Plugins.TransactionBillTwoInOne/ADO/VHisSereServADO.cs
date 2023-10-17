using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.ADO
{
    public class VHisSereServADO : V_HIS_SERE_SERV_5
    {
        public decimal? InvoicePrice { get; set; }
        public decimal? RecieptPrice { get; set; }
        public bool IsInvoiced { get; set; }
        public bool IsReciepted { get; set; }

        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public decimal? AMOUNT_PLUS { get; set; }
        public string VIR_PRICE_PLUS { get; set; }
        public decimal VAT { get; set; }
        public bool? IsExpend { get; set; }
        public bool? IsLeaf { get; set; }

        public VHisSereServADO()
        {
        }

        public VHisSereServADO(V_HIS_SERE_SERV_5 service)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<VHisSereServADO>(this, service);
            IsExpend = (service.IS_EXPEND == 1);
            this.AMOUNT_PLUS = service.AMOUNT;
            this.VAT = service.VAT_RATIO * 100;
            this.VIR_PRICE_PLUS = service.VIR_PRICE.HasValue ? Inventec.Common.Number.Convert.NumberToString(service.VIR_PRICE.Value, HIS.Desktop.LocalStorage.ConfigApplication.ConfigApplications.NumberSeperator) : "";
            //this.CONCRETE_ID__IN_SETY = (service.SERVICE_TYPE_ID + "." + service.CONCRETE_ID);
            //this.PARENT_ID__IN_SETY = (service.SERVICE_TYPE_ID + "." + service.PARENT_ID);

        }

        public VHisSereServADO(V_HIS_SERE_SERV service, int patyId)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<VHisSereServADO>(this, service);
            IsExpend = (service.IS_EXPEND == 1);
            this.PARENT_ID__IN_SETY = patyId + "." + service.TDL_SERVICE_TYPE_ID;
            this.CONCRETE_ID__IN_SETY = patyId + "." + service.TDL_SERVICE_TYPE_ID + "." + service.SERVICE_ID;
        }
    }
}
