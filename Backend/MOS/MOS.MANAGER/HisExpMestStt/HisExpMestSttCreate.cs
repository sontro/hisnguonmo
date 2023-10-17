using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestStt
{
    class HisExpMestSttCreate : BusinessBase
    {
        internal HisExpMestSttCreate()
            : base()
        {

        }

        internal HisExpMestSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestSttCheck checker = new HisExpMestSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXP_MEST_STT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisExpMestSttDAO.Create(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestSttCheck checker = new HisExpMestSttCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXP_MEST_STT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisExpMestSttDAO.CreateList(listData);
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
