using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExeServiceModule
{
    public partial class HisExeServiceModuleDAO : EntityBase
    {
        public HIS_EXE_SERVICE_MODULE GetByCode(string code, HisExeServiceModuleSO search)
        {
            HIS_EXE_SERVICE_MODULE result = null;

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

        public Dictionary<string, HIS_EXE_SERVICE_MODULE> GetDicByCode(HisExeServiceModuleSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXE_SERVICE_MODULE> result = new Dictionary<string, HIS_EXE_SERVICE_MODULE>();
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
