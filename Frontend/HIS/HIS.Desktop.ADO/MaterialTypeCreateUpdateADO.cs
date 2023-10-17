using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public delegate void DelegateRefeshData();
    public class MaterialTypeCreateUpdateADO
    {
       public DelegateRefeshData refreshData;

        public MaterialTypeCreateUpdateADO()
        {
        }

        public MaterialTypeCreateUpdateADO(DelegateRefeshData _refreshData)
        {
            try
            {
                this.refreshData = _refreshData;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
