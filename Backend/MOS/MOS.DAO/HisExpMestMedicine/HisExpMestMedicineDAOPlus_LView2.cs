using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMedicine
{
    public partial class HisExpMestMedicineDAO : EntityBase
    {
        public List<L_HIS_EXP_MEST_MEDICINE_2> GetLView2(HisExpMestMedicineSO search, CommonParam param)
        {
            List<L_HIS_EXP_MEST_MEDICINE_2> result = new List<L_HIS_EXP_MEST_MEDICINE_2>();
            try
            {
                result = GetWorker.GetLView2(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public L_HIS_EXP_MEST_MEDICINE_2 GetLView2ById(long id, HisExpMestMedicineSO search)
        {
            L_HIS_EXP_MEST_MEDICINE_2 result = null;

            try
            {
                result = GetWorker.GetLView2ById(id, search);
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
