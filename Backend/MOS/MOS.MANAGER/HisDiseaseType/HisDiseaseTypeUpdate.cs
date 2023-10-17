using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDiseaseType
{
    partial class HisDiseaseTypeUpdate : BusinessBase
    {
		private List<HIS_DISEASE_TYPE> beforeUpdateHisDiseaseTypes = new List<HIS_DISEASE_TYPE>();
		
        internal HisDiseaseTypeUpdate()
            : base()
        {

        }

        internal HisDiseaseTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DISEASE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DISEASE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDiseaseTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiseaseType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDiseaseType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDiseaseTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_DISEASE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                List<HIS_DISEASE_TYPE> listRaw = new List<HIS_DISEASE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDiseaseTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDiseaseType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDiseaseType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDiseaseTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDiseaseTypes))
            {
                if (!DAOWorker.HisDiseaseTypeDAO.UpdateList(this.beforeUpdateHisDiseaseTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDiseaseType that bai, can kiem tra lai." + LogUtil.TraceData("HisDiseaseTypes", this.beforeUpdateHisDiseaseTypes));
                }
				this.beforeUpdateHisDiseaseTypes = null;
            }
        }
    }
}
