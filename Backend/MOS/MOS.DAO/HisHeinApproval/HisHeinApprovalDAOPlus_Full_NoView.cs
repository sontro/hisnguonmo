using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHeinApproval
{
    public partial class HisHeinApprovalDAO : EntityBase
    {
        public HIS_HEIN_APPROVAL GetByCode(string code, HisHeinApprovalSO search)
        {
            HIS_HEIN_APPROVAL result = null;

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

        public Dictionary<string, HIS_HEIN_APPROVAL> GetDicByCode(HisHeinApprovalSO search, CommonParam param)
        {
            Dictionary<string, HIS_HEIN_APPROVAL> result = new Dictionary<string, HIS_HEIN_APPROVAL>();
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
