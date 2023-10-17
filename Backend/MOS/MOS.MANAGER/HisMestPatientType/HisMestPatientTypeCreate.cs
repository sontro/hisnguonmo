using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatientType
{
    class HisMestPatientTypeCreate : BusinessBase
    {
        internal HisMestPatientTypeCreate()
            : base()
        {

        }

        internal HisMestPatientTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PATIENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatientTypeCheck checker = new HisMestPatientTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisMestPatientTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatientTypeCheck checker = new HisMestPatientTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisMestPatientTypeDAO.CreateList(listData);
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
