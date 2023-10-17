using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    class ThoiDiemADO
    {
        public long id { get; set; }
        public string ThoiDiem { get; set; }

        public ThoiDiemADO(long id, string ThoiDiem)
        {

            try
            {
                this.id = id;
                this.ThoiDiem = ThoiDiem;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
