using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintTypeCfg
{
    public partial class SarPrintTypeCfgDAO : EntityBase
    {
        public SAR_PRINT_TYPE_CFG GetByCode(string code, SarPrintTypeCfgSO search)
        {
            SAR_PRINT_TYPE_CFG result = null;

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

        public Dictionary<string, SAR_PRINT_TYPE_CFG> GetDicByCode(SarPrintTypeCfgSO search, CommonParam param)
        {
            Dictionary<string, SAR_PRINT_TYPE_CFG> result = new Dictionary<string, SAR_PRINT_TYPE_CFG>();
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
