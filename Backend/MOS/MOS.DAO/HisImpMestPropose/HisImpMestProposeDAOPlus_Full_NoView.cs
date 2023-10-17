using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPropose
{
    public partial class HisImpMestProposeDAO : EntityBase
    {
        public HIS_IMP_MEST_PROPOSE GetByCode(string code, HisImpMestProposeSO search)
        {
            HIS_IMP_MEST_PROPOSE result = null;

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

        public Dictionary<string, HIS_IMP_MEST_PROPOSE> GetDicByCode(HisImpMestProposeSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_MEST_PROPOSE> result = new Dictionary<string, HIS_IMP_MEST_PROPOSE>();
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
