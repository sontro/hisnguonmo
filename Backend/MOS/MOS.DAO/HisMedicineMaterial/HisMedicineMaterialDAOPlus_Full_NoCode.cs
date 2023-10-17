using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineMaterial
{
    public partial class HisMedicineMaterialDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_MATERIAL> GetView(HisMedicineMaterialSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_MATERIAL> result = new List<V_HIS_MEDICINE_MATERIAL>();
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

        public V_HIS_MEDICINE_MATERIAL GetViewById(long id, HisMedicineMaterialSO search)
        {
            V_HIS_MEDICINE_MATERIAL result = null;

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
