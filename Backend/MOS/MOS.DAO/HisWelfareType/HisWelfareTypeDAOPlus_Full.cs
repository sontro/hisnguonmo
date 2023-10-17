using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWelfareType
{
    public partial class HisWelfareTypeDAO : EntityBase
    {
        //public List<V_HIS_WELFARE_TYPE> GetView(HisWelfareTypeSO search, CommonParam param)
        //{
        //    List<V_HIS_WELFARE_TYPE> result = new List<V_HIS_WELFARE_TYPE>();

        //    try
        //    {
        //        result = GetWorker.GetView(search, param);
        //    }
        //    catch (Exception ex)
        //    {
        //        param.HasException = true;
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result.Clear();
        //    }

        //    return result;
        //}

        public HIS_WELFARE_TYPE GetByCode(string code, HisWelfareTypeSO search)
        {
            HIS_WELFARE_TYPE result = null;

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
        
        //public V_HIS_WELFARE_TYPE GetViewById(long id, HisWelfareTypeSO search)
        //{
        //    V_HIS_WELFARE_TYPE result = null;

        //    try
        //    {
        //        result = GetWorker.GetViewById(id, search);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result = null;
        //    }

        //    return result;
        //}

        //public V_HIS_WELFARE_TYPE GetViewByCode(string code, HisWelfareTypeSO search)
        //{
        //    V_HIS_WELFARE_TYPE result = null;

        //    try
        //    {
        //        result = GetWorker.GetViewByCode(code, search);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result = null;
        //    }
        //    return result;
        //}

        public Dictionary<string, HIS_WELFARE_TYPE> GetDicByCode(HisWelfareTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_WELFARE_TYPE> result = new Dictionary<string, HIS_WELFARE_TYPE>();
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
