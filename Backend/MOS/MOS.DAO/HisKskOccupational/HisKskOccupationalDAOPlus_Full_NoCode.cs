using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOccupational
{
    public partial class HisKskOccupationalDAO : EntityBase
    {
        public List<V_HIS_KSK_OCCUPATIONAL> GetView(HisKskOccupationalSO search, CommonParam param)
        {
            List<V_HIS_KSK_OCCUPATIONAL> result = new List<V_HIS_KSK_OCCUPATIONAL>();
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

        public V_HIS_KSK_OCCUPATIONAL GetViewById(long id, HisKskOccupationalSO search)
        {
            V_HIS_KSK_OCCUPATIONAL result = null;

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
