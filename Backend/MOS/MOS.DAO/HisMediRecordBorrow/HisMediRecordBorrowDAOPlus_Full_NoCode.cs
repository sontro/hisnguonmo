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
    }
}
