using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentHurt
{
    partial class HisAccidentHurtUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_HURT> beforeUpdateHisAccidentHurtDTOs = new List<HIS_ACCIDENT_HURT>();
		
        internal HisAccidentHurtUpdate()
            : base()
        {

        }

        internal HisAccidentHurtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_HURT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHurtCheck checker = new HisAccidentHurtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_HURT raw = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.EXECUTE_ROOM_ID.Value, ref workPlace);
                if (valid)
                {
                    data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.EXECUTE_ROOM_ID = workPlace.RoomId;
                    data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;

					this.beforeUpdateHisAccidentHurtDTOs.Add(raw);
					if (!DAOWorker.HisAccidentHurtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentHurt that bai." + LogUtil.TraceData("data", data));
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentHurtDTOs))
            {
                if (!DAOWorker.HisAccidentHurtDAO.UpdateList(this.beforeUpdateHisAccidentHurtDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHurt that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentHurtDTOs", this.beforeUpdateHisAccidentHurtDTOs));
                }
                this.beforeUpdateHisAccidentHurtDTOs = null;
            }
        }
    }
}
