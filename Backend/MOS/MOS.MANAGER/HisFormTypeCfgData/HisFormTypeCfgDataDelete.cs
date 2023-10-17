using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataDelete : BusinessBase
    {
        internal HisFormTypeCfgDataDelete()
            : base()
        {

        }

        internal HisFormTypeCfgDataDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_FORM_TYPE_CFG_DATA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG_DATA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisFormTypeCfgDataDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                List<HIS_FORM_TYPE_CFG_DATA> listRaw = new List<HIS_FORM_TYPE_CFG_DATA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisFormTypeCfgDataDAO.DeleteList(listData);
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
