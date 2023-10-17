using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_BODY_PART> beforeUpdateHisAccidentBodyParts = new List<HIS_ACCIDENT_BODY_PART>();
		
        internal HisAccidentBodyPartUpdate()
            : base()
        {

        }

        internal HisAccidentBodyPartUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_BODY_PART raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_BODY_PART_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentBodyParts.Add(raw);
					if (!DAOWorker.HisAccidentBodyPartDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentBodyPart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentBodyPart that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                List<HIS_ACCIDENT_BODY_PART> listRaw = new List<HIS_ACCIDENT_BODY_PART>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_BODY_PART_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentBodyParts.AddRange(listRaw);
					if (!DAOWorker.HisAccidentBodyPartDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentBodyPart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentBodyPart that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentBodyParts))
            {
                if (!new HisAccidentBodyPartUpdate(param).UpdateList(this.beforeUpdateHisAccidentBodyParts))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentBodyPart that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentBodyParts", this.beforeUpdateHisAccidentBodyParts));
                }
            }
        }
    }
}
