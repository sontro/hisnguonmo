using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverType
{
    public partial class HisEmrCoverTypeDAO : EntityBase
    {
        public HIS_EMR_COVER_TYPE GetByCode(string code, HisEmrCoverTypeSO search)
        {
            HIS_EMR_COVER_TYPE result = null;

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

        public Dictionary<string, HIS_EMR_COVER_TYPE> GetDicByCode(HisEmrCoverTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMR_COVER_TYPE> result = new Dictionary<string, HIS_EMR_COVER_TYPE>();
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
