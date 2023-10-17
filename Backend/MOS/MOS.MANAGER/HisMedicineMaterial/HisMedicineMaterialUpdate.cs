using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineMaterial
{
    partial class HisMedicineMaterialUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_MATERIAL> beforeUpdateHisMedicineMaterials = new List<HIS_MEDICINE_MATERIAL>();
		
        internal HisMedicineMaterialUpdate()
            : base()
        {

        }

        internal HisMedicineMaterialUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineMaterialCheck checker = new HisMedicineMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_MATERIAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMedicineMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineMaterial that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMedicineMaterials.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDICINE_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineMaterialCheck checker = new HisMedicineMaterialCheck(param);
                List<HIS_MEDICINE_MATERIAL> listRaw = new List<HIS_MEDICINE_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMedicineMaterials.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineMaterials))
            {
                if (!DAOWorker.HisMedicineMaterialDAO.UpdateList(this.beforeUpdateHisMedicineMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineMaterials", this.beforeUpdateHisMedicineMaterials));
                }
				this.beforeUpdateHisMedicineMaterials = null;
            }
        }
    }
}
