using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineBean;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    class HisExpMestMedicineTruncate : BusinessBase
    {
        internal HisExpMestMedicineTruncate()
            : base()
        {

        }

        internal HisExpMestMedicineTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EXP_MEST_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMedicineDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_EXP_MEST_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisExpMestMedicineDAO.TruncateList(listData);
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

        internal bool TruncateList(List<long> expMestMedicineIds)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicineIds))
                {
                    string sqlTruncate = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "DELETE FROM HIS_EXP_MEST_MEDICINE WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sqlTruncate))
                    {
                        throw new Exception("Xoa exp_mest_medicine that bai");
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
