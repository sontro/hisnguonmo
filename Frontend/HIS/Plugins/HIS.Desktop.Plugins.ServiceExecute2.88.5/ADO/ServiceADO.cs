using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute.ADO
{
    public class ServiceADO : HIS_SERE_SERV
    {
        public bool isSave { get; set; }
        public string SoPhieu { get; set; }

        public int tableIndex { get; set; }//phục vụ all in one lấy theo index tạo ra
        public string conclude { get; set; }
        public string note { get; set; }
        public string description { get; set; }
        public bool isEdit { get; set; }

        public long? MACHINE_ID { get; set; }
        public long? NUMBER_OF_FILM { get; set; }

        public bool MustHavePressBeforeExecute { get; set; }

        public ServiceADO(HIS_SERE_SERV data)
        {
            try
            {
                if (data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HIS_SERE_SERV>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(data)));
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
