using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRationSum.ADO
{
    class SereServ15ADO : V_HIS_SERE_SERV_15
    {
        public string CHILD_ID { get; set; }
        public string PARENT_ID_STR { get; set; }
        public decimal AMOUNT_SUM { get; set; }
        public int IS_PARENT { get; set; }
        public string INSTRUCTION_NOTE { get; set; }
        public string TITLE_NAME { get; set; }

        public SereServ15ADO()
        {
        }

        public SereServ15ADO(V_HIS_SERE_SERV_15 sereServ)
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
