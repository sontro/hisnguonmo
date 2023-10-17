using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSereDire
{
    public partial class HisExamSereDireDAO : EntityBase
    {
        public List<V_HIS_EXAM_SERE_DIRE> GetView(HisExamSereDireSO search, CommonParam param)
        {
            List<V_HIS_EXAM_SERE_DIRE> result = new List<V_HIS_EXAM_SERE_DIRE>();

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
        
        public V_HIS_EXAM_SERE_DIRE GetViewById(long id, HisExamSereDireSO search)
        {
            V_HIS_EXAM_SERE_DIRE result = null;

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

        public V_HIS_EXAM_SERE_DIRE GetViewByCode(string code, HisExamSereDireSO search)
        {
            V_HIS_EXAM_SERE_DIRE result = null;

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
