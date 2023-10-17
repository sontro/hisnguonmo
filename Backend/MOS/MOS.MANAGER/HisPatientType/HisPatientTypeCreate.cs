using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientType
{
    partial class HisPatientTypeCreate : BusinessBase
    {
        internal HisPatientTypeCreate()
            : base()
        {

        }

        internal HisPatientTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PATIENT_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_PATIENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeDAO.CreateList(listData);
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
