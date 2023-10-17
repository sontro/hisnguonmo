using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWorkPlace
{
    partial class HisWorkPlaceDelete : BusinessBase
    {
        internal HisWorkPlaceDelete()
            : base()
        {

        }

        internal HisWorkPlaceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_WORK_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkPlaceCheck checker = new HisWorkPlaceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_WORK_PLACE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisWorkPlaceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_WORK_PLACE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWorkPlaceCheck checker = new HisWorkPlaceCheck(param);
                List<HIS_WORK_PLACE> listRaw = new List<HIS_WORK_PLACE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisWorkPlaceDAO.DeleteList(listData);
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
