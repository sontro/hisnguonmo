using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransfusionSum.CreateOrUpdateSdo
{
    class HisTransfusionSumSdoCheck : BusinessBase
    {
        internal HisTransfusionSumSdoCheck()
            : base()
        {
        }

        internal HisTransfusionSumSdoCheck(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(HisTransfusionSumSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ExpMestBloodId <= 0) throw new ArgumentNullException("data.ExpMestBloodId");
                if (data.TreatmentId <= 0) throw new ArgumentNullException("data.TreatmentId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
