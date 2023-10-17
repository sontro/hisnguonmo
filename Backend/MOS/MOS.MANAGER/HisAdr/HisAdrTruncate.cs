using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAdrMedicineType;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrTruncate : BusinessBase
    {
        internal HisAdrTruncate()
            : base()
        {

        }

        internal HisAdrTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrCheck checker = new HisAdrCheck(param);
                HisAdrMedicineTypeCheck medicineTypeChecker = new HisAdrMedicineTypeCheck(param);
                HIS_ADR raw = null;
                List<HIS_ADR_MEDICINE_TYPE> adrMedicineTypes = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.AllowUpdateOrDeleteLoginname(raw, ref adrMedicineTypes);
                valid = valid && medicineTypeChecker.IsUnLock(adrMedicineTypes);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    string sqlAdr = String.Format("DELETE HIS_ADR WHERE ID = {0}", id);
                    string sqlMety = String.Format("DELETE HIS_ADR_MEDICINE_TYPE WHERE ADR_ID = {0}", id);
                    sqls.Add(sqlMety);
                    sqls.Add(sqlAdr);
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Xoa HIS_ADR that bai");
                    }
                    result = true;
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

        internal bool TruncateList(List<HIS_ADR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrCheck checker = new HisAdrCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAdrDAO.TruncateList(listData);
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
