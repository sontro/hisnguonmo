using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineLine
{
    public partial class HisMedicineLineDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_LINE> GetView(HisMedicineLineSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_LINE> result = new List<V_HIS_MEDICINE_LINE>();
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

        public V_HIS_MEDICINE_LINE GetViewById(long id, HisMedicineLineSO search)
        {
            V_HIS_MEDICINE_LINE result = null;

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
