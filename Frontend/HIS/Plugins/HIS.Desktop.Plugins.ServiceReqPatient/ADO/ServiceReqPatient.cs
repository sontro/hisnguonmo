using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqPatient.ADO
{
    class ServiceReqPatientADO : V_HIS_EXP_MEST_MEDICINE
    {
        public ServiceReqPatientADO() { }

        public ServiceReqPatientADO(ServiceReqPatientADO data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ServiceReqPatientADO>(this, data);
                this.AmountDateList = (from r in data.AmountDateList select r).ToList();
                this.AmountDate = data.AmountDate;
                this.IS_MADICINE = data.IS_MADICINE;
            }
        }

        public long MEDI_MATE_EXP_MEST_ID { get; set; }

        public long? MATERIAL_TYPE_ID { get; set; }

        public long MEDI_MATE_TYPE_ID { get; set; }

        public string ServiceReqPatientName { get; set; }
        public string IS_HP { get; set; }
        public string NOTE { get; set; }
        public string UNIT_NAME { get; set; }
        public int IS_MADICINE { get; set; }
        public decimal SUM { get; set; }

        public class amountdate
        {

            public long SortDate { get; set; }
            public string DATE { get; set; }
            public decimal Amount { get; set; }
            public Boolean IS_export { get; set; }

            public void amountDate(string date, decimal amount, Boolean IS_Export)
            {
                this.DATE = date;
                this.Amount = amount;
                this.IS_export = IS_Export;
            }
        }
        public amountdate AmountDate { get; set; }
        public string Type { get; set; }
        public List<amountdate> AmountDateList { get; set; }
        public Boolean IS_STAR_MARK { get; set; }
        public Boolean IS_Red { get; set; }
    }
}
