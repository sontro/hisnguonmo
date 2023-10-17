using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBirthCertBook
{
    public partial class HisBirthCertBookDAO : EntityBase
    {
        public HIS_BIRTH_CERT_BOOK GetByCode(string code, HisBirthCertBookSO search)
        {
            HIS_BIRTH_CERT_BOOK result = null;

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

        public Dictionary<string, HIS_BIRTH_CERT_BOOK> GetDicByCode(HisBirthCertBookSO search, CommonParam param)
        {
            Dictionary<string, HIS_BIRTH_CERT_BOOK> result = new Dictionary<string, HIS_BIRTH_CERT_BOOK>();
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
