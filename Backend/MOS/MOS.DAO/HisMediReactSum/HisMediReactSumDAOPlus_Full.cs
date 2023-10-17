using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediReactSum
{
    public partial class HisMediReactSumDAO : EntityBase
    {
        public List<V_HIS_MEDI_REACT_SUM> GetView(HisMediReactSumSO search, CommonParam param)
        {
            List<V_HIS_MEDI_REACT_SUM> result = new List<V_HIS_MEDI_REACT_SUM>();

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

        public HIS_MEDI_REACT_SUM GetByCode(string code, HisMediReactSumSO search)
        {
            HIS_MEDI_REACT_SUM result = null;

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
        
        public V_HIS_MEDI_REACT_SUM GetViewById(long id, HisMediReactSumSO search)
        {
            V_HIS_MEDI_REACT_SUM result = null;

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

        public V_HIS_MEDI_REACT_SUM GetViewByCode(string code, HisMediReactSumSO search)
        {
            V_HIS_MEDI_REACT_SUM result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_MEDI_REACT_SUM> GetDicByCode(HisMediReactSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_REACT_SUM> result = new Dictionary<string, HIS_MEDI_REACT_SUM>();
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
