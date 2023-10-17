using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecord
{
    public partial class HisMediRecordDAO : EntityBase
    {
        public List<V_HIS_MEDI_RECORD_2> GetView2(HisMediRecordSO search, CommonParam param)
        {
            List<V_HIS_MEDI_RECORD_2> result = new List<V_HIS_MEDI_RECORD_2>();

            try
            {
                result = GetWorker.GetView2(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public V_HIS_MEDI_RECORD_2 GetView2ById(long id, HisMediRecordSO search)
        {
            V_HIS_MEDI_RECORD_2 result = null;

            try
            {
                result = GetWorker.GetView2ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_MEDI_RECORD_2 GetView2ByCode(string code, HisMediRecordSO search)
        {
            V_HIS_MEDI_RECORD_2 result = null;

            try
            {
                result = GetWorker.GetView2ByCode(code, search);
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
