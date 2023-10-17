using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisService
{
    public partial class HisServiceDAO : EntityBase
    {
        //public HIS_SERVICE GetByCode(string code, HisServiceSO search)
        //{
        //    HIS_SERVICE result = null;

        //    try
        //    {
        //        result = GetWorker.GetByCode(code, search);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result = null;
        //    }

        //    return result;
        //}

        //public Dictionary<string, HIS_SERVICE> GetDicByCode(HisServiceSO search, CommonParam param)
        //{
        //    Dictionary<string, HIS_SERVICE> result = new Dictionary<string, HIS_SERVICE>();
        //    try
        //    {
        //        result = GetWorker.GetDicByCode(search, param);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result.Clear();
        //    }

        //    return result;
        //}

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
