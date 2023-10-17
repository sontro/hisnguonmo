using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescDelete : BusinessBase
    {
        internal HisEyeSurgryDescDelete()
            : base()
        {

        }

        internal HisEyeSurgryDescDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EYE_SURGRY_DESC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEyeSurgryDescCheck checker = new HisEyeSurgryDescCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EYE_SURGRY_DESC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEyeSurgryDescDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EYE_SURGRY_DESC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEyeSurgryDescCheck checker = new HisEyeSurgryDescCheck(param);
                List<HIS_EYE_SURGRY_DESC> listRaw = new List<HIS_EYE_SURGRY_DESC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEyeSurgryDescDAO.DeleteList(listData);
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
