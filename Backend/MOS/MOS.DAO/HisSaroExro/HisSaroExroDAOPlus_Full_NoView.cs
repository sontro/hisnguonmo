using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSaroExro
{
    public partial class HisSaroExroDAO : EntityBase
    {
        public HIS_SARO_EXRO GetByCode(string code, HisSaroExroSO search)
        {
            HIS_SARO_EXRO result = null;

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

        public Dictionary<string, HIS_SARO_EXRO> GetDicByCode(HisSaroExroSO search, CommonParam param)
        {
            Dictionary<string, HIS_SARO_EXRO> result = new Dictionary<string, HIS_SARO_EXRO>();
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
