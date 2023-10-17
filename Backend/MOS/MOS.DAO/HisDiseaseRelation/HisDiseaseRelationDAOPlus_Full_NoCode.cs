using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDiseaseRelation
{
    public partial class HisDiseaseRelationDAO : EntityBase
    {
        public List<V_HIS_DISEASE_RELATION> GetView(HisDiseaseRelationSO search, CommonParam param)
        {
            List<V_HIS_DISEASE_RELATION> result = new List<V_HIS_DISEASE_RELATION>();
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

        public V_HIS_DISEASE_RELATION GetViewById(long id, HisDiseaseRelationSO search)
        {
            V_HIS_DISEASE_RELATION result = null;

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
