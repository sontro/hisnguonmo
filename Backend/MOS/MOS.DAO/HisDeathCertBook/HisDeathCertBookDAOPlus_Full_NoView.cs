using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCertBook
{
    public partial class HisDeathCertBookDAO : EntityBase
    {
        public HIS_DEATH_CERT_BOOK GetByCode(string code, HisDeathCertBookSO search)
        {
            HIS_DEATH_CERT_BOOK result = null;

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

        public Dictionary<string, HIS_DEATH_CERT_BOOK> GetDicByCode(HisDeathCertBookSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEATH_CERT_BOOK> result = new Dictionary<string, HIS_DEATH_CERT_BOOK>();
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
