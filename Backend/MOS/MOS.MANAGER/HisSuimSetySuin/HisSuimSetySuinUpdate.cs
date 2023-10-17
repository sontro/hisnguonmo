using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSuimSetySuin
{
    partial class HisSuimSetySuinUpdate : BusinessBase
    {
        internal HisSuimSetySuinUpdate()
            : base()
        {

        }

        internal HisSuimSetySuinUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SUIM_SETY_SUIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimSetySuinCheck checker = new HisSuimSetySuinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SUIM_SETY_SUIN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSuimSetySuinDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SUIM_SETY_SUIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSuimSetySuinCheck checker = new HisSuimSetySuinCheck(param);
                List<HIS_SUIM_SETY_SUIN> listRaw = new List<HIS_SUIM_SETY_SUIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisSuimSetySuinDAO.UpdateList(listData);
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
