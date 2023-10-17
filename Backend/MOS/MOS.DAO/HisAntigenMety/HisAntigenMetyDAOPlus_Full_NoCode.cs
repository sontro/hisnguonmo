using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigenMety
{
    public partial class HisAntigenMetyDAO : EntityBase
    {
        public List<V_HIS_ANTIGEN_METY> GetView(HisAntigenMetySO search, CommonParam param)
        {
            List<V_HIS_ANTIGEN_METY> result = new List<V_HIS_ANTIGEN_METY>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_ANTIGEN_METY GetViewById(long id, HisAntigenMetySO search)
        {
            V_HIS_ANTIGEN_METY result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
