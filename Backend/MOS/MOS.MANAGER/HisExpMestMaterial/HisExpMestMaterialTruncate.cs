using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialBean;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    class HisExpMestMaterialTruncate : BusinessBase
    {
        internal HisExpMestMaterialTruncate()
            : base()
        {

        }

        internal HisExpMestMaterialTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EXP_MEST_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMaterialDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisExpMestMaterialDAO.TruncateList(listData);
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

        internal bool TruncateList(List<long> expMestMaterialIds)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterialIds))
                {
                    string sqlTruncate = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sqlTruncate))
                    {
                        throw new Exception("Xoa exp_mest_material that bai");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
