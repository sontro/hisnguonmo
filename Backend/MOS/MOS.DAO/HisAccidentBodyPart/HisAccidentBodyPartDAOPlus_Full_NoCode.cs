using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    public partial class HisAccidentBodyPartDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_BODY_PART> GetView(HisAccidentBodyPartSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_BODY_PART> result = new List<V_HIS_ACCIDENT_BODY_PART>();
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

        public V_HIS_ACCIDENT_BODY_PART GetViewById(long id, HisAccidentBodyPartSO search)
        {
            V_HIS_ACCIDENT_BODY_PART result = null;

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
