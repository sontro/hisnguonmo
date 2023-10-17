using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticNewReg
{
    public partial class HisAntibioticNewRegDAO : EntityBase
    {
        public HIS_ANTIBIOTIC_NEW_REG GetByCode(string code, HisAntibioticNewRegSO search)
        {
            HIS_ANTIBIOTIC_NEW_REG result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public Dictionary<string, HIS_ANTIBIOTIC_NEW_REG> GetDicByCode(HisAntibioticNewRegSO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIBIOTIC_NEW_REG> result = new Dictionary<string, HIS_ANTIBIOTIC_NEW_REG>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {

            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
