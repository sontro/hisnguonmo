using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRationSum.ADO
{
    public class SereServ1ADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_1
    {
        public string CHILD_ID { get; set; }
        public string PARENT_ID_STR { get; set; }
        public decimal AMOUNT_SUM { get; set; }
        public int IS_PARENT { get; set; }

        public string TITLE_NAME { get; set; }

        public SereServ1ADO()
        {

        }

        public SereServ1ADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_1 sereServ)
        {
            try
            {
                if (sereServ != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_1>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, item.GetValue(sereServ));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
