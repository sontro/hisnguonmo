using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTech
{
    public partial class HisTranPatiTechDAO : EntityBase
    {
        public List<V_HIS_TRAN_PATI_TECH> GetView(HisTranPatiTechSO search, CommonParam param)
        {
            List<V_HIS_TRAN_PATI_TECH> result = new List<V_HIS_TRAN_PATI_TECH>();
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

        public V_HIS_TRAN_PATI_TECH GetViewById(long id, HisTranPatiTechSO search)
        {
            V_HIS_TRAN_PATI_TECH result = null;

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
