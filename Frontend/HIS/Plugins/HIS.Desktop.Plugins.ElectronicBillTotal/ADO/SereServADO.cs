using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ElectronicBillTotal.ADO
{
    class SereServADO : V_HIS_SERE_SERV_5
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string AMOUNT_DISPLAY { get; set; }
        public string TRANSACTION_CODE { get; set; }
        public string CASHIER_LOGINNAME { get; set; }
        public string CASHIER_USERNAME { get; set; }

        public decimal TRANSACTION_AMOUNT { get; set; }
        public decimal VAT { get; set; }

        public decimal? AMOUNT_PLUS { get; set; }

        public bool? IsLeaf { get; set; }

        public long BILL_ID { get; set; }
        public long TRANSACTION_TIME { get; set; }

        public SereServADO(V_HIS_SERE_SERV_5 service, V_HIS_TRANSACTION tran)
        {
            if (service != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<SereServADO>(this, service);
                this.AMOUNT_PLUS = service.AMOUNT;
                this.VAT = service.VAT_RATIO * 100;
                this.AMOUNT_DISPLAY = Inventec.Common.Number.Convert.NumberToString(service.AMOUNT, HIS.Desktop.LocalStorage.ConfigApplication.ConfigApplications.NumberSeperator);
            }

            if (tran != null)
            {
                this.BILL_ID = tran.ID;
                this.TRANSACTION_CODE = tran.TRANSACTION_CODE;
                this.TRANSACTION_TIME = tran.TRANSACTION_TIME;
                this.TRANSACTION_AMOUNT = tran.AMOUNT;
                this.CASHIER_LOGINNAME = tran.CASHIER_LOGINNAME;
                this.CASHIER_USERNAME = tran.CASHIER_USERNAME;
            }
        }

        public SereServADO()
        {
        }
    }
}
