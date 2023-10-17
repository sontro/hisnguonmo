using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeDelete : BusinessBase
    {
        internal HisBidMaterialTypeDelete()
            : base()
        {

        }

        internal HisBidMaterialTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BID_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MATERIAL_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBidMaterialTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BID_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                List<HIS_BID_MATERIAL_TYPE> listRaw = new List<HIS_BID_MATERIAL_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBidMaterialTypeDAO.DeleteList(listData);
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
