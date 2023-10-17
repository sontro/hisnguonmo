using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticOldReg
{
    public partial class HisAntibioticOldRegDAO : EntityBase
    {
        public List<V_HIS_ANTIBIOTIC_OLD_REG> GetView(HisAntibioticOldRegSO search, CommonParam param)
        {
            List<V_HIS_ANTIBIOTIC_OLD_REG> result = new List<V_HIS_ANTIBIOTIC_OLD_REG>();
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

        public V_HIS_ANTIBIOTIC_OLD_REG GetViewById(long id, HisAntibioticOldRegSO search)
        {
            V_HIS_ANTIBIOTIC_OLD_REG result = null;

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
