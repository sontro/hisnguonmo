using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestAggrExam.ADO
{
    public class MssMedicineTypeSDO
    {
        public long TYPE_ID { get; set; }//1.Thuoc, 2.VatTu
        public decimal AMOUNT { get; set; }

        public MssMedicineTypeSDO() { }

        public MssMedicineTypeSDO(List<V_HIS_EXP_MEST_MEDICINE> datas)
        {
            try
            {
                this.TYPE_ID = 1;
                this.AMOUNT = datas.Sum(p => p.AMOUNT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public MssMedicineTypeSDO(List<V_HIS_EXP_MEST_MATERIAL> datas)
        {
            try
            {
                this.TYPE_ID = 2;
                this.AMOUNT = datas.Sum(p => p.AMOUNT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
