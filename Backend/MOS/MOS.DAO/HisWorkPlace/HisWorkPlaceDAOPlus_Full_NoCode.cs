using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWorkPlace
{
    public partial class HisWorkPlaceDAO : EntityBase
    {
        public List<V_HIS_WORK_PLACE> GetView(HisWorkPlaceSO search, CommonParam param)
        {
            List<V_HIS_WORK_PLACE> result = new List<V_HIS_WORK_PLACE>();
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

        public V_HIS_WORK_PLACE GetViewById(long id, HisWorkPlaceSO search)
        {
            V_HIS_WORK_PLACE result = null;

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
