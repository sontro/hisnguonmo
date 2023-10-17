using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.VitaminA
{
    class HisExpMestVitaminACheck : BusinessBase
    {
        internal HisExpMestVitaminACheck()
            : base()
        {

        }

        internal HisExpMestVitaminACheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisExpMestVitaminASDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (!IsNotNullOrEmpty(data.VitaminAIds)) throw new ArgumentNullException("data.VitaminAIds");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
