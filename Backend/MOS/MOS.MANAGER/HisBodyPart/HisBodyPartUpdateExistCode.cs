using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBodyPart
{
    partial class HisBodyPartUpdate : BusinessBase
    {
		private List<HIS_BODY_PART> beforeUpdateHisBodyParts = new List<HIS_BODY_PART>();
		
        internal HisBodyPartUpdate()
            : base()
        {

        }

        internal HisBodyPartUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBodyPartCheck checker = new HisBodyPartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BODY_PART raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BODY_PART_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisBodyPartDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBodyPart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBodyPart that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisBodyParts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_BODY_PART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBodyPartCheck checker = new HisBodyPartCheck(param);
                List<HIS_BODY_PART> listRaw = new List<HIS_BODY_PART>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BODY_PART_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisBodyPartDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBodyPart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBodyPart that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisBodyParts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBodyParts))
            {
                if (!DAOWorker.HisBodyPartDAO.UpdateList(this.beforeUpdateHisBodyParts))
                {
                    LogSystem.Warn("Rollback du lieu HisBodyPart that bai, can kiem tra lai." + LogUtil.TraceData("HisBodyParts", this.beforeUpdateHisBodyParts));
                }
				this.beforeUpdateHisBodyParts = null;
            }
        }
    }
}
