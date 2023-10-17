using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDebt
{
    public partial class HisSereServDebtDAO : EntityBase
    {
        public HIS_SERE_SERV_DEBT GetByCode(string code, HisSereServDebtSO search)
        {
            HIS_SERE_SERV_DEBT result = null;

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

        public Dictionary<string, HIS_SERE_SERV_DEBT> GetDicByCode(HisSereServDebtSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_DEBT> result = new Dictionary<string, HIS_SERE_SERV_DEBT>();
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
