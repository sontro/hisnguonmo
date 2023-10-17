using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPackage
{
    public partial class HisPackageDAO : EntityBase
    {
        public List<V_HIS_PACKAGE> GetView(HisPackageSO search, CommonParam param)
        {
            List<V_HIS_PACKAGE> result = new List<V_HIS_PACKAGE>();
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

        public V_HIS_PACKAGE GetViewById(long id, HisPackageSO search)
        {
            V_HIS_PACKAGE result = null;

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
