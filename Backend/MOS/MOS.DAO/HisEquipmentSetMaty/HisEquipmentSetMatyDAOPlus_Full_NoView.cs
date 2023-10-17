using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSetMaty
{
    public partial class HisEquipmentSetMatyDAO : EntityBase
    {
        public HIS_EQUIPMENT_SET_MATY GetByCode(string code, HisEquipmentSetMatySO search)
        {
            HIS_EQUIPMENT_SET_MATY result = null;

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

        public Dictionary<string, HIS_EQUIPMENT_SET_MATY> GetDicByCode(HisEquipmentSetMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_EQUIPMENT_SET_MATY> result = new Dictionary<string, HIS_EQUIPMENT_SET_MATY>();
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
