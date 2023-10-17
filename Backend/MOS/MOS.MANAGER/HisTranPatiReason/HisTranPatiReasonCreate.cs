using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiReason
{
    class HisTranPatiReasonCreate : BusinessBase
    {
        internal HisTranPatiReasonCreate()
            : base()
        {

        }

        internal HisTranPatiReasonCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRAN_PATI_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiReasonCheck checker = new HisTranPatiReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_REASON_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiReasonDAO.Create(data);
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

        internal bool CreateList(List<HIS_TRAN_PATI_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiReasonCheck checker = new HisTranPatiReasonCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_REASON_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTranPatiReasonDAO.CreateList(listData);
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
