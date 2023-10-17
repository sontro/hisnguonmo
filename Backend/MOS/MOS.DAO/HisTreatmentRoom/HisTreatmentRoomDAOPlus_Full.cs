using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentRoom
{
    public partial class HisTreatmentRoomDAO : EntityBase
    {
        public List<V_HIS_TREATMENT_ROOM> GetView(HisTreatmentRoomSO search, CommonParam param)
        {
            List<V_HIS_TREATMENT_ROOM> result = new List<V_HIS_TREATMENT_ROOM>();

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

        public HIS_TREATMENT_ROOM GetByCode(string code, HisTreatmentRoomSO search)
        {
            HIS_TREATMENT_ROOM result = null;

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
        
        public V_HIS_TREATMENT_ROOM GetViewById(long id, HisTreatmentRoomSO search)
        {
            V_HIS_TREATMENT_ROOM result = null;

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

        public V_HIS_TREATMENT_ROOM GetViewByCode(string code, HisTreatmentRoomSO search)
        {
            V_HIS_TREATMENT_ROOM result = null;

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

        public Dictionary<string, HIS_TREATMENT_ROOM> GetDicByCode(HisTreatmentRoomSO search, CommonParam param)
        {
            Dictionary<string, HIS_TREATMENT_ROOM> result = new Dictionary<string, HIS_TREATMENT_ROOM>();
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
