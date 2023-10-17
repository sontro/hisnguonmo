using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuDetail
{
    public partial class HisSurgRemuDetailDAO : EntityBase
    {
        public List<V_HIS_SURG_REMU_DETAIL> GetView(HisSurgRemuDetailSO search, CommonParam param)
        {
            List<V_HIS_SURG_REMU_DETAIL> result = new List<V_HIS_SURG_REMU_DETAIL>();
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

        public V_HIS_SURG_REMU_DETAIL GetViewById(long id, HisSurgRemuDetailSO search)
        {
            V_HIS_SURG_REMU_DETAIL result = null;

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
