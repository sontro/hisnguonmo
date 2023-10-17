using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCatastrophe
{
    public partial class HisPtttCatastropheDAO : EntityBase
    {
        public List<V_HIS_PTTT_CATASTROPHE> GetView(HisPtttCatastropheSO search, CommonParam param)
        {
            List<V_HIS_PTTT_CATASTROPHE> result = new List<V_HIS_PTTT_CATASTROPHE>();

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

        public HIS_PTTT_CATASTROPHE GetByCode(string code, HisPtttCatastropheSO search)
        {
            HIS_PTTT_CATASTROPHE result = null;

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
        
        public V_HIS_PTTT_CATASTROPHE GetViewById(long id, HisPtttCatastropheSO search)
        {
            V_HIS_PTTT_CATASTROPHE result = null;

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

        public V_HIS_PTTT_CATASTROPHE GetViewByCode(string code, HisPtttCatastropheSO search)
        {
            V_HIS_PTTT_CATASTROPHE result = null;

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

        public Dictionary<string, HIS_PTTT_CATASTROPHE> GetDicByCode(HisPtttCatastropheSO search, CommonParam param)
        {
            Dictionary<string, HIS_PTTT_CATASTROPHE> result = new Dictionary<string, HIS_PTTT_CATASTROPHE>();
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
