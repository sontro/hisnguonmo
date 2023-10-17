using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWorkPlace
{
    partial class HisWorkPlaceUpdate : BusinessBase
    {
		private List<HIS_WORK_PLACE> beforeUpdateHisWorkPlaces = new List<HIS_WORK_PLACE>();
		
        internal HisWorkPlaceUpdate()
            : base()
        {

        }

        internal HisWorkPlaceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_WORK_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkPlaceCheck checker = new HisWorkPlaceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_WORK_PLACE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.WORK_PLACE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisWorkPlaces.Add(raw);
					if (!DAOWorker.HisWorkPlaceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkPlace_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWorkPlace that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_WORK_PLACE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWorkPlaceCheck checker = new HisWorkPlaceCheck(param);
                List<HIS_WORK_PLACE> listRaw = new List<HIS_WORK_PLACE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.WORK_PLACE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisWorkPlaces.AddRange(listRaw);
					if (!DAOWorker.HisWorkPlaceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkPlace_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWorkPlace that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisWorkPlaces))
            {
                if (!new HisWorkPlaceUpdate(param).UpdateList(this.beforeUpdateHisWorkPlaces))
                {
                    LogSystem.Warn("Rollback du lieu HisWorkPlace that bai, can kiem tra lai." + LogUtil.TraceData("HisWorkPlaces", this.beforeUpdateHisWorkPlaces));
                }
            }
        }
    }
}
