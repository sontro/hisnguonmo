using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttHighTech
{
    public partial class HisPtttHighTechDAO : EntityBase
    {
        public List<V_HIS_PTTT_HIGH_TECH> GetView(HisPtttHighTechSO search, CommonParam param)
        {
            List<V_HIS_PTTT_HIGH_TECH> result = new List<V_HIS_PTTT_HIGH_TECH>();
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

        public V_HIS_PTTT_HIGH_TECH GetViewById(long id, HisPtttHighTechSO search)
        {
            V_HIS_PTTT_HIGH_TECH result = null;

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
