using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSpeciality
{
    partial class HisSpecialityUpdate : BusinessBase
    {
		private List<HIS_SPECIALITY> beforeUpdateHisSpecialitys = new List<HIS_SPECIALITY>();
		
        internal HisSpecialityUpdate()
            : base()
        {

        }

        internal HisSpecialityUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SPECIALITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSpecialityCheck checker = new HisSpecialityCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SPECIALITY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SPECIALITY_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSpecialityDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeciality_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSpeciality that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSpecialitys.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SPECIALITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSpecialityCheck checker = new HisSpecialityCheck(param);
                List<HIS_SPECIALITY> listRaw = new List<HIS_SPECIALITY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SPECIALITY_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSpecialityDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeciality_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSpeciality that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSpecialitys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSpecialitys))
            {
                if (!DAOWorker.HisSpecialityDAO.UpdateList(this.beforeUpdateHisSpecialitys))
                {
                    LogSystem.Warn("Rollback du lieu HisSpeciality that bai, can kiem tra lai." + LogUtil.TraceData("HisSpecialitys", this.beforeUpdateHisSpecialitys));
                }
				this.beforeUpdateHisSpecialitys = null;
            }
        }
    }
}
