using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecord
{
    public partial class HisMediRecordDAO : EntityBase
    {
        public List<V_HIS_MEDI_RECORD> GetView(HisMediRecordSO search, CommonParam param)
        {
            List<V_HIS_MEDI_RECORD> result = new List<V_HIS_MEDI_RECORD>();

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

        public HIS_MEDI_RECORD GetByCode(string code, HisMediRecordSO search)
        {
            HIS_MEDI_RECORD result = null;

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
        
        public V_HIS_MEDI_RECORD GetViewById(long id, HisMediRecordSO search)
        {
            V_HIS_MEDI_RECORD result = null;

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

        public V_HIS_MEDI_RECORD GetViewByCode(string code, HisMediRecordSO search)
        {
            V_HIS_MEDI_RECORD result = null;

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

        public Dictionary<string, HIS_MEDI_RECORD> GetDicByCode(HisMediRecordSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_RECORD> result = new Dictionary<string, HIS_MEDI_RECORD>();
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
