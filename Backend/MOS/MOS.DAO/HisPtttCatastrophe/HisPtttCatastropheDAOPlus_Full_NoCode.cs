using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCatastrophe
{
    public partial class HisPtttCatastropheDAO : EntityBase
    {
        public List<V_HIS_PTTT_CATASTROPHE> GetView(HisPtttCatastropheSO search, CommonParam param)
        {
            List<V_HIS_PTTT_CATASTROPHE> result = new List<V_HIS_PTTT_CATASTROPHE>();
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

        public V_HIS_PTTT_CATASTROPHE GetViewById(long id, HisPtttCatastropheSO search)
        {
            V_HIS_PTTT_CATASTROPHE result = null;

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
