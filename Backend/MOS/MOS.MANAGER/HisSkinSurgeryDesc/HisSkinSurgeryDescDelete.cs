using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescDelete : BusinessBase
    {
        internal HisSkinSurgeryDescDelete()
            : base()
        {

        }

        internal HisSkinSurgeryDescDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SKIN_SURGERY_DESC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SKIN_SURGERY_DESC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSkinSurgeryDescDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                List<HIS_SKIN_SURGERY_DESC> listRaw = new List<HIS_SKIN_SURGERY_DESC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSkinSurgeryDescDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
