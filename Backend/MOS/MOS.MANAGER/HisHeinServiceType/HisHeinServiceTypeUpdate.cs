using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinServiceType
{
    class HisHeinServiceTypeUpdate : BusinessBase
    {
        internal HisHeinServiceTypeUpdate()
            : base()
        {

        }

        internal HisHeinServiceTypeUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HEIN_SERVICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinServiceTypeCheck checker = new HisHeinServiceTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.HEIN_SERVICE_TYPE_CODE, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisHeinServiceTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHeinServiceType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHeinServiceType that bai." + LogUtil.TraceData("data", data));
                    }
                    result = true;
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

        internal bool UpdateList(List<HIS_HEIN_SERVICE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHeinServiceTypeCheck checker = new HisHeinServiceTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.HEIN_SERVICE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisHeinServiceTypeDAO.UpdateList(listData);
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
