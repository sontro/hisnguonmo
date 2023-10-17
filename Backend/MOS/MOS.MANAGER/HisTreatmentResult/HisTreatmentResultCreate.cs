using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentResult
{
    class HisTreatmentResultCreate : BusinessBase
    {
        internal HisTreatmentResultCreate()
            : base()
        {

        }

        internal HisTreatmentResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentResultCheck checker = new HisTreatmentResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TREATMENT_RESULT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.Create(data);
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

        internal bool CreateList(List<HIS_TREATMENT_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentResultCheck checker = new HisTreatmentResultCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_RESULT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTreatmentResultDAO.CreateList(listData);
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
