using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapDelete : BusinessBase
    {
        internal HisMaterialTypeMapDelete()
            : base()
        {

        }

        internal HisMaterialTypeMapDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MATERIAL_TYPE_MAP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE_MAP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMaterialTypeMapDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                List<HIS_MATERIAL_TYPE_MAP> listRaw = new List<HIS_MATERIAL_TYPE_MAP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMaterialTypeMapDAO.DeleteList(listData);
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
