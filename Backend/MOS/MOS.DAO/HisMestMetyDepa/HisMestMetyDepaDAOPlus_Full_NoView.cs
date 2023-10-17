using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMetyDepa
{
    public partial class HisMestMetyDepaDAO : EntityBase
    {
        public HIS_MEST_METY_DEPA GetByCode(string code, HisMestMetyDepaSO search)
        {
            HIS_MEST_METY_DEPA result = null;

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

        public Dictionary<string, HIS_MEST_METY_DEPA> GetDicByCode(HisMestMetyDepaSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_METY_DEPA> result = new Dictionary<string, HIS_MEST_METY_DEPA>();
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
