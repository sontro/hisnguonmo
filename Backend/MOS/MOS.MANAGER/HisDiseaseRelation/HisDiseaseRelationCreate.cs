using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiseaseRelation
{
    class HisDiseaseRelationCreate : BusinessBase
    {
        internal HisDiseaseRelationCreate()
            : base()
        {

        }

        internal HisDiseaseRelationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DISEASE_RELATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiseaseRelationCheck checker = new HisDiseaseRelationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DISEASE_RELATION_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisDiseaseRelationDAO.Create(data);
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

        internal bool CreateList(List<HIS_DISEASE_RELATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDiseaseRelationCheck checker = new HisDiseaseRelationCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DISEASE_RELATION_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisDiseaseRelationDAO.CreateList(listData);
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
