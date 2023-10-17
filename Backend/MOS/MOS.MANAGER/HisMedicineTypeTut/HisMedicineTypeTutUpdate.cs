using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_TYPE_TUT> beforeUpdateHisMedicineTypeTuts = new List<HIS_MEDICINE_TYPE_TUT>();
		
        internal HisMedicineTypeTutUpdate()
            : base()
        {

        }

        internal HisMedicineTypeTutUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_TYPE_TUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_TYPE_TUT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                //valid = valid && checker.IsNotExisted(raw); //cho phep tao nhieu HDSD cho 1 thuoc
                if (valid)
                {
                    this.beforeUpdateHisMedicineTypeTuts.Add(raw);
					if (!DAOWorker.HisMedicineTypeTutDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeTut_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineTypeTut that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                List<HIS_MEDICINE_TYPE_TUT> listRaw = new List<HIS_MEDICINE_TYPE_TUT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMedicineTypeTuts.AddRange(listRaw);
					if (!DAOWorker.HisMedicineTypeTutDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeTut_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineTypeTut that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineTypeTuts))
            {
                if (!new HisMedicineTypeTutUpdate(param).UpdateList(this.beforeUpdateHisMedicineTypeTuts))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineTypeTut that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineTypeTuts", this.beforeUpdateHisMedicineTypeTuts));
                }
            }
        }
    }
}
