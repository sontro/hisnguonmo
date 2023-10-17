using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSubclinicalRsAdd
{
    public partial class HisSubclinicalRsAddDAO : EntityBase
    {
        public List<V_HIS_SUBCLINICAL_RS_ADD> GetView(HisSubclinicalRsAddSO search, CommonParam param)
        {
            List<V_HIS_SUBCLINICAL_RS_ADD> result = new List<V_HIS_SUBCLINICAL_RS_ADD>();
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

        public V_HIS_SUBCLINICAL_RS_ADD GetViewById(long id, HisSubclinicalRsAddSO search)
        {
            V_HIS_SUBCLINICAL_RS_ADD result = null;

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
