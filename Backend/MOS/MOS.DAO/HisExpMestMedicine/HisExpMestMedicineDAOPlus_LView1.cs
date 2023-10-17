using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMedicine
{
    public partial class HisExpMestMedicineDAO : EntityBase
    {
        public List<L_HIS_EXP_MEST_MEDICINE_1> GetLView1(HisExpMestMedicineSO search, CommonParam param)
        {
            List<L_HIS_EXP_MEST_MEDICINE_1> result = new List<L_HIS_EXP_MEST_MEDICINE_1>();
            try
            {
                result = GetWorker.GetLView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public L_HIS_EXP_MEST_MEDICINE_1 GetLView1ById(long id, HisExpMestMedicineSO search)
        {
            L_HIS_EXP_MEST_MEDICINE_1 result = null;

            try
            {
                result = GetWorker.GetLView1ById(id, search);
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
