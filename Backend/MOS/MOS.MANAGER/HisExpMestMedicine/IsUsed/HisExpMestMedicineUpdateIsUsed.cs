using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine.IsUsed
{
	partial class HisExpMestMedicineUpdateIsUsed : BusinessBase
	{
		internal HisExpMestMedicineUpdateIsUsed()
			: base()
		{

		}

        internal HisExpMestMedicineUpdateIsUsed(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Used(long expMestMedicineId, ref HIS_EXP_MEST_MEDICINE resultData)
		{
			bool result = false;
			try
			{
				HIS_EXP_MEST_MEDICINE expMestMedicine = null;
                HisExpMestMedicineCheck expMestMedicineCheck = new HisExpMestMedicineCheck(param);
                HisExpMestMedicineUpdateIsUsedCheck checker = new HisExpMestMedicineUpdateIsUsedCheck(param);
                bool valid = true;

                valid = valid && expMestMedicineCheck.VerifyId(expMestMedicineId, ref expMestMedicine);
                valid = valid && checker.IsExported(expMestMedicine);

                if (valid)
				{
                    expMestMedicine.IS_USED = Constant.IS_TRUE;

                    if (DAOWorker.HisExpMestMedicineDAO.Update(expMestMedicine))
					{
						result = true;
                        resultData = expMestMedicine;
					}
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

        internal bool Unused(long expMestMedicineId, ref HIS_EXP_MEST_MEDICINE resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST_MEDICINE expMestMedicine = null;
                HisExpMestMedicineCheck expMestMedicineCheck = new HisExpMestMedicineCheck(param);
                bool valid = true;

                valid = valid && expMestMedicineCheck.VerifyId(expMestMedicineId, ref expMestMedicine);

                if (valid)
                {
                    expMestMedicine.IS_USED = null;

                    if (DAOWorker.HisExpMestMedicineDAO.Update(expMestMedicine))
                    {
                        result = true;
                        resultData = expMestMedicine;
                    }
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
