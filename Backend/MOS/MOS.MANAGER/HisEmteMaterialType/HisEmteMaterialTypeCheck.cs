using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;

namespace MOS.MANAGER.HisEmteMaterialType
{
    class HisEmteMaterialTypeCheck : BusinessBase
    {
        internal HisEmteMaterialTypeCheck()
            : base()
        {

        }

        internal HisEmteMaterialTypeCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }
        
        internal bool VerifyRequireField(HIS_EMTE_MATERIAL_TYPE data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.EXP_MEST_TEMPLATE_ID)) throw new ArgumentNullException("data.EXP_MEST_TEMPLATE_ID");
                if (data.IS_OUT_MEDI_STOCK != Constant.IS_TRUE && (!data.MATERIAL_TYPE_ID.HasValue || !data.SERVICE_UNIT_ID.HasValue)) throw new ArgumentNullException("data.MATERIAL_TYPE_ID hoac data.SERVICE_UNIT_ID");
                if (string.IsNullOrWhiteSpace(data.MATERIAL_TYPE_NAME)) throw new ArgumentNullException("data.MATERIAL_TYPE_NAME");
                if (string.IsNullOrWhiteSpace(data.SERVICE_UNIT_NAME)) throw new ArgumentNullException("data.SERVICE_UNIT_NAME");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsUnLock(HIS_EMTE_MATERIAL_TYPE data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
        
        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisEmteMaterialTypeDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
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
