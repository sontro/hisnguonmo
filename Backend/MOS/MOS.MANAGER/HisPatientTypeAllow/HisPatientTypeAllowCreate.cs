using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeAllow
{
    class HisPatientTypeAllowCreate : BusinessBase
    {
        internal HisPatientTypeAllowCreate()
            : base()
        {

        }

        internal HisPatientTypeAllowCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_TYPE_ALLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAllowCheck checker = new HisPatientTypeAllowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.Create(data);
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

        internal bool CreateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeAllowCheck checker = new HisPatientTypeAllowCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.CreateList(listData);
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
