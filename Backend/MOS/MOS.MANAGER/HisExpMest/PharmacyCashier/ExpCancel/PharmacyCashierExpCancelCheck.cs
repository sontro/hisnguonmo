using Inventec.Core;
using Inventec.Common.Logging;

using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisExpMest.Common.Get;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.ExpCancel
{
    public class PharmacyCashierExpCancelCheck: BusinessBase
    {
        internal PharmacyCashierExpCancelCheck()
            : base()
        {
        }

        internal PharmacyCashierExpCancelCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidExpMest(PharmacyCashierExpCancelSDO sdo, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                HIS_EXP_MEST exp = new HisExpMestGet().GetByCode(sdo.ExpMestCode);
                if (exp == null)
				{
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.ExpMestId ko hop le");
                    return false;
                }
                if (exp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongPhaiPhieuXuatBan, exp.EXP_MEST_CODE);
                    return false;
				}
                expMest = exp;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
