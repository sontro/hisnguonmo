using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTextLib
{
    class HisTextLibUpdate : BusinessBase
    {
        internal HisTextLibUpdate()
            : base()
        {

        }

        internal HisTextLibUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TEXT_LIB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TEXT_LIB raw = null;
                HisTextLibCheck checker = new HisTextLibCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckExists(data, raw.CREATOR);
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.Update(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateList(List<HIS_TEXT_LIB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                List<HIS_TEXT_LIB> listRaw = new List<HIS_TEXT_LIB>();
                HisTextLibCheck checker = new HisTextLibCheck(param);
                List<long> listId = listData.Select(s => s.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                foreach (var data in listData)
                {
                    HIS_TEXT_LIB raw = listRaw.FirstOrDefault(o => o.ID == data.ID);
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckExists(data, raw.CREATOR);
                }
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.UpdateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
