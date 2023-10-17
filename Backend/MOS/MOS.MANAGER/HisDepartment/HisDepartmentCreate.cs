using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartment
{
    class HisDepartmentCreate : BusinessBase
    {
        internal HisDepartmentCreate()
            : base()
        {

        }

        internal HisDepartmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEPARTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepartmentCheck checker = new HisDepartmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEPARTMENT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisDepartmentDAO.Create(data);
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

        internal bool CreateList(List<HIS_DEPARTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDepartmentCheck checker = new HisDepartmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEPARTMENT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisDepartmentDAO.CreateList(listData);
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
