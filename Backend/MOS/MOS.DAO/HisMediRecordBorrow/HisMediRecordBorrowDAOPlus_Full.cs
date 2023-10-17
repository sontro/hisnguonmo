using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecordBorrow
{
    public partial class HisMediRecordBorrowDAO : EntityBase
    {
        public List<V_HIS_MEDI_RECORD_BORROW> GetView(HisMediRecordBorrowSO search, CommonParam param)
        {
            List<V_HIS_MEDI_RECORD_BORROW> result = new List<V_HIS_MEDI_RECORD_BORROW>();

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

        public HIS_MEDI_RECORD_BORROW GetByCode(string code, HisMediRecordBorrowSO search)
        {
            HIS_MEDI_RECORD_BORROW result = null;

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
        
        public V_HIS_MEDI_RECORD_BORROW GetViewById(long id, HisMediRecordBorrowSO search)
        {
            V_HIS_MEDI_RECORD_BORROW result = null;

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

        public V_HIS_MEDI_RECORD_BORROW GetViewByCode(string code, HisMediRecordBorrowSO search)
        {
            V_HIS_MEDI_RECORD_BORROW result = null;

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

        public Dictionary<string, HIS_MEDI_RECORD_BORROW> GetDicByCode(HisMediRecordBorrowSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_RECORD_BORROW> result = new Dictionary<string, HIS_MEDI_RECORD_BORROW>();
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
