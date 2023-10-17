using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineUseForm
{
    public partial class HisMedicineUseFormDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_USE_FORM> GetView(HisMedicineUseFormSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_USE_FORM> result = new List<V_HIS_MEDICINE_USE_FORM>();
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

        public V_HIS_MEDICINE_USE_FORM GetViewById(long id, HisMedicineUseFormSO search)
        {
            V_HIS_MEDICINE_USE_FORM result = null;

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