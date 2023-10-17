using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServSegr
{
    class HisServSegrCreate : BusinessBase
    {
        internal HisServSegrCreate()
            : base()
        {

        }

        internal HisServSegrCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERV_SEGR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServSegrCheck checker = new HisServSegrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisServSegrDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERV_SEGR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServSegrCheck checker = new HisServSegrCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisServSegrDAO.CreateList(listData);
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
