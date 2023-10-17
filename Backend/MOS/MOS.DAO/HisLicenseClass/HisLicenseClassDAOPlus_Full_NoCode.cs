using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisLicenseClass
{
    public partial class HisLicenseClassDAO : EntityBase
    {
        public List<V_HIS_LICENSE_CLASS> GetView(HisLicenseClassSO search, CommonParam param)
        {
            List<V_HIS_LICENSE_CLASS> result = new List<V_HIS_LICENSE_CLASS>();
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

        public V_HIS_LICENSE_CLASS GetViewById(long id, HisLicenseClassSO search)
        {
            V_HIS_LICENSE_CLASS result = null;

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
