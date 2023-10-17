using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisGender
{
    public partial class HisGenderDAO : EntityBase
    {
        public List<V_HIS_GENDER> GetView(HisGenderSO search, CommonParam param)
        {
            List<V_HIS_GENDER> result = new List<V_HIS_GENDER>();
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

        public V_HIS_GENDER GetViewById(long id, HisGenderSO search)
        {
            V_HIS_GENDER result = null;

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
