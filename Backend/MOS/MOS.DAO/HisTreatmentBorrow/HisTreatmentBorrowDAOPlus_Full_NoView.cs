using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBorrow
{
    public partial class HisTreatmentBorrowDAO : EntityBase
    {
        public HIS_TREATMENT_BORROW GetByCode(string code, HisTreatmentBorrowSO search)
        {
            HIS_TREATMENT_BORROW result = null;

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

        public Dictionary<string, HIS_TREATMENT_BORROW> GetDicByCode(HisTreatmentBorrowSO search, CommonParam param)
        {
            Dictionary<string, HIS_TREATMENT_BORROW> result = new Dictionary<string, HIS_TREATMENT_BORROW>();
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
