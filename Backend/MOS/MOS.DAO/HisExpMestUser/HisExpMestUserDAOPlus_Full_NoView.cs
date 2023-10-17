using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestUser
{
    public partial class HisExpMestUserDAO : EntityBase
    {
        public HIS_EXP_MEST_USER GetByCode(string code, HisExpMestUserSO search)
        {
            HIS_EXP_MEST_USER result = null;

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

        public Dictionary<string, HIS_EXP_MEST_USER> GetDicByCode(HisExpMestUserSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_USER> result = new Dictionary<string, HIS_EXP_MEST_USER>();
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
