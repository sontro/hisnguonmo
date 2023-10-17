using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestType
{
    class HisExpMestTypeCreate : BusinessBase
    {
        internal HisExpMestTypeCreate()
            : base()
        {

        }

        internal HisExpMestTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestTypeCheck checker = new HisExpMestTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXP_MEST_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisExpMestTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestTypeCheck checker = new HisExpMestTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXP_MEST_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisExpMestTypeDAO.CreateList(listData);
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
