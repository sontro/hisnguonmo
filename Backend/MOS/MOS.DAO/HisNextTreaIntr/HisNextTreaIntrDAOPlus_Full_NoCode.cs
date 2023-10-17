using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNextTreaIntr
{
    public partial class HisNextTreaIntrDAO : EntityBase
    {
        public List<V_HIS_NEXT_TREA_INTR> GetView(HisNextTreaIntrSO search, CommonParam param)
        {
            List<V_HIS_NEXT_TREA_INTR> result = new List<V_HIS_NEXT_TREA_INTR>();
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

        public V_HIS_NEXT_TREA_INTR GetViewById(long id, HisNextTreaIntrSO search)
        {
            V_HIS_NEXT_TREA_INTR result = null;

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
