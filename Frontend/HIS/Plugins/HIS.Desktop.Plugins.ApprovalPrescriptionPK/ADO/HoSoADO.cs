using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK.ADO
{
    public class HoSoADO
    {
        public string ID { get; set; }
        public string HOSO_NAME { get; set; }

        public HoSoADO() { }
        public HoSoADO(string id, string name) 
        {
            try
            {
                this.ID = id;
                this.HOSO_NAME = name;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

    }
}
