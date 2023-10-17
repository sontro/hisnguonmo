using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisGender
{
    class HisGenderCreate : BusinessBase
    {
        internal HisGenderCreate()
            : base()
        {

        }

        internal HisGenderCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_GENDER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisGenderCheck checker = new HisGenderCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.GENDER_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisGenderDAO.Create(data);
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

        internal bool CreateList(List<HIS_GENDER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisGenderCheck checker = new HisGenderCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.GENDER_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisGenderDAO.CreateList(listData);
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
