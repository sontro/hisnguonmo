using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
    class VaoLucADO
    {
        public long id { get; set; }
        public string VaoLuc { get; set; }

        public VaoLucADO(long id, string VaoLuc)
        {

            try
            {
                this.id = id;
                this.VaoLuc = VaoLuc;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
