using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialMaterial
{
    partial class HisMaterialMaterialDelete : BusinessBase
    {
        internal HisMaterialMaterialDelete()
            : base()
        {

        }

        internal HisMaterialMaterialDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MATERIAL_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_MATERIAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMaterialMaterialDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MATERIAL_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                List<HIS_MATERIAL_MATERIAL> listRaw = new List<HIS_MATERIAL_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMaterialMaterialDAO.DeleteList(listData);
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
