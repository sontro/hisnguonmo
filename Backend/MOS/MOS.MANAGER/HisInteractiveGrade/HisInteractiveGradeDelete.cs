using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInteractiveGrade
{
    partial class HisInteractiveGradeDelete : BusinessBase
    {
        internal HisInteractiveGradeDelete()
            : base()
        {

        }

        internal HisInteractiveGradeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_INTERACTIVE_GRADE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_INTERACTIVE_GRADE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisInteractiveGradeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                List<HIS_INTERACTIVE_GRADE> listRaw = new List<HIS_INTERACTIVE_GRADE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisInteractiveGradeDAO.DeleteList(listData);
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
