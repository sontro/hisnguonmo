using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndType
{
    partial class HisTreatmentEndTypeCreate : BusinessBase
    {
        internal HisTreatmentEndTypeCreate()
            : base()
        {

        }

        internal HisTreatmentEndTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_END_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_TREATMENT_END_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.CreateList(listData);
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
