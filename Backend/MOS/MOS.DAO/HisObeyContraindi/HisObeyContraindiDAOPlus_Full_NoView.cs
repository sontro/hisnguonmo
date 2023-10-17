using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisObeyContraindi
{
    public partial class HisObeyContraindiDAO : EntityBase
    {
        public HIS_OBEY_CONTRAINDI GetByCode(string code, HisObeyContraindiSO search)
        {
            HIS_OBEY_CONTRAINDI result = null;

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

        public Dictionary<string, HIS_OBEY_CONTRAINDI> GetDicByCode(HisObeyContraindiSO search, CommonParam param)
        {
            Dictionary<string, HIS_OBEY_CONTRAINDI> result = new Dictionary<string, HIS_OBEY_CONTRAINDI>();
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
