using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineInteractive
{
    public partial class HisMedicineInteractiveDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_INTERACTIVE> GetView(HisMedicineInteractiveSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_INTERACTIVE> result = new List<V_HIS_MEDICINE_INTERACTIVE>();
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

        public V_HIS_MEDICINE_INTERACTIVE GetViewById(long id, HisMedicineInteractiveSO search)
        {
            V_HIS_MEDICINE_INTERACTIVE result = null;

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
