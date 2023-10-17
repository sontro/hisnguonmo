using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointUpdate : BusinessBase
    {
		private List<HIS_CONTACT_POINT> beforeUpdateHisContactPoints = new List<HIS_CONTACT_POINT>();
		
        internal HisContactPointUpdate()
            : base()
        {

        }

        internal HisContactPointUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CONTACT_POINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactPointCheck checker = new HisContactPointCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CONTACT_POINT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    data.LAST_NAME = data.LAST_NAME != null ? data.LAST_NAME.ToUpper() : null;
                    data.FIRST_NAME = data.FIRST_NAME != null ? data.FIRST_NAME.ToUpper() : null;

					if (!DAOWorker.HisContactPointDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContactPoint_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContactPoint that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisContactPoints.Add(raw);
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

        internal bool UpdateList(List<HIS_CONTACT_POINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactPointCheck checker = new HisContactPointCheck(param);
                List<HIS_CONTACT_POINT> listRaw = new List<HIS_CONTACT_POINT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisContactPointDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContactPoint_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContactPoint that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisContactPoints.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisContactPoints))
            {
                if (!DAOWorker.HisContactPointDAO.UpdateList(this.beforeUpdateHisContactPoints))
                {
                    LogSystem.Warn("Rollback du lieu HisContactPoint that bai, can kiem tra lai." + LogUtil.TraceData("HisContactPoints", this.beforeUpdateHisContactPoints));
                }
				this.beforeUpdateHisContactPoints = null;
            }
        }
    }
}
