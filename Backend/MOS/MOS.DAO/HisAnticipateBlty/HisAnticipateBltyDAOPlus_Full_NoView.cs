using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateBlty
{
    public partial class HisAnticipateBltyDAO : EntityBase
    {
        public HIS_ANTICIPATE_BLTY GetByCode(string code, HisAnticipateBltySO search)
        {
            HIS_ANTICIPATE_BLTY result = null;

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

        public Dictionary<string, HIS_ANTICIPATE_BLTY> GetDicByCode(HisAnticipateBltySO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTICIPATE_BLTY> result = new Dictionary<string, HIS_ANTICIPATE_BLTY>();
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
