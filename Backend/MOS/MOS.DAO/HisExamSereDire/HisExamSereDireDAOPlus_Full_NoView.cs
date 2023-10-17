using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSereDire
{
    public partial class HisExamSereDireDAO : EntityBase
    {
        public HIS_EXAM_SERE_DIRE GetByCode(string code, HisExamSereDireSO search)
        {
            HIS_EXAM_SERE_DIRE result = null;

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

        public Dictionary<string, HIS_EXAM_SERE_DIRE> GetDicByCode(HisExamSereDireSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXAM_SERE_DIRE> result = new Dictionary<string, HIS_EXAM_SERE_DIRE>();
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
