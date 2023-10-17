using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmteMaterialType
{
    class HisEmteMaterialTypeCreate : BusinessBase
    {
        internal HisEmteMaterialTypeCreate()
            : base()
        {

        }

        internal HisEmteMaterialTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMTE_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmteMaterialTypeCheck checker = new HisEmteMaterialTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisEmteMaterialTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmteMaterialTypeCheck checker = new HisEmteMaterialTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisEmteMaterialTypeDAO.CreateList(listData);
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
