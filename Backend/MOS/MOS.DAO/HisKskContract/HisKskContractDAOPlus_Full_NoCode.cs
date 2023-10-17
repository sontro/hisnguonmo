using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskContract
{
    public partial class HisKskContractDAO : EntityBase
    {
        public List<V_HIS_KSK_CONTRACT> GetView(HisKskContractSO search, CommonParam param)
        {
            List<V_HIS_KSK_CONTRACT> result = new List<V_HIS_KSK_CONTRACT>();
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

        public V_HIS_KSK_CONTRACT GetViewById(long id, HisKskContractSO search)
        {
            V_HIS_KSK_CONTRACT result = null;

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
