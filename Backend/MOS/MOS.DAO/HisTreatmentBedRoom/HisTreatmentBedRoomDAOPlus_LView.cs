using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBedRoom
{
    public partial class HisTreatmentBedRoomDAO : EntityBase
    {
        public List<L_HIS_TREATMENT_BED_ROOM> GetLView(HisTreatmentBedRoomSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_BED_ROOM> result = new List<L_HIS_TREATMENT_BED_ROOM>();
            try
            {
                result = GetWorker.GetLView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public L_HIS_TREATMENT_BED_ROOM GetLViewById(long id, HisTreatmentBedRoomSO search)
        {
            L_HIS_TREATMENT_BED_ROOM result = null;

            try
            {
                result = GetWorker.GetLViewById(id, search);
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
