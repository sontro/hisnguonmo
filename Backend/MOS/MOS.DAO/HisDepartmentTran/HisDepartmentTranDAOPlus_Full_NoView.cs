using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepartmentTran
{
    public partial class HisDepartmentTranDAO : EntityBase
    {
        public HIS_DEPARTMENT_TRAN GetByCode(string code, HisDepartmentTranSO search)
        {
            HIS_DEPARTMENT_TRAN result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public Dictionary<string, HIS_DEPARTMENT_TRAN> GetDicByCode(HisDepartmentTranSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEPARTMENT_TRAN> result = new Dictionary<string, HIS_DEPARTMENT_TRAN>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {

            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
