using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigenMety
{
    public partial class HisAntigenMetyDAO : EntityBase
    {
        public HIS_ANTIGEN_METY GetByCode(string code, HisAntigenMetySO search)
        {
            HIS_ANTIGEN_METY result = null;

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

        public Dictionary<string, HIS_ANTIGEN_METY> GetDicByCode(HisAntigenMetySO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIGEN_METY> result = new Dictionary<string, HIS_ANTIGEN_METY>();
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
