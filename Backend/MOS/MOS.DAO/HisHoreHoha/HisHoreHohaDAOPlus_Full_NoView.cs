using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHoha
{
    public partial class HisHoreHohaDAO : EntityBase
    {
        public HIS_HORE_HOHA GetByCode(string code, HisHoreHohaSO search)
        {
            HIS_HORE_HOHA result = null;

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

        public Dictionary<string, HIS_HORE_HOHA> GetDicByCode(HisHoreHohaSO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_HOHA> result = new Dictionary<string, HIS_HORE_HOHA>();
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
