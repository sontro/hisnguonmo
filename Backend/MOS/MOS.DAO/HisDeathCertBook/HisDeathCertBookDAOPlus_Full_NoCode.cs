using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCertBook
{
    public partial class HisDeathCertBookDAO : EntityBase
    {
        public List<V_HIS_DEATH_CERT_BOOK> GetView(HisDeathCertBookSO search, CommonParam param)
        {
            List<V_HIS_DEATH_CERT_BOOK> result = new List<V_HIS_DEATH_CERT_BOOK>();
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

        public V_HIS_DEATH_CERT_BOOK GetViewById(long id, HisDeathCertBookSO search)
        {
            V_HIS_DEATH_CERT_BOOK result = null;

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
