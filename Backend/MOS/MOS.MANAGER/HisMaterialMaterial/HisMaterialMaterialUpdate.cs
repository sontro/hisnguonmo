using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialMaterial
{
    partial class HisMaterialMaterialUpdate : BusinessBase
    {
		private List<HIS_MATERIAL_MATERIAL> beforeUpdateHisMaterialMaterials = new List<HIS_MATERIAL_MATERIAL>();
		
        internal HisMaterialMaterialUpdate()
            : base()
        {

        }

        internal HisMaterialMaterialUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATERIAL_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MATERIAL_MATERIAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMaterialMaterialDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialMaterial that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMaterialMaterials.Add(raw);
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

        internal bool UpdateList(List<HIS_MATERIAL_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                List<HIS_MATERIAL_MATERIAL> listRaw = new List<HIS_MATERIAL_MATERIAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMaterialMaterialDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMaterialMaterials.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMaterialMaterials))
            {
                if (!DAOWorker.HisMaterialMaterialDAO.UpdateList(this.beforeUpdateHisMaterialMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialMaterials", this.beforeUpdateHisMaterialMaterials));
                }
				this.beforeUpdateHisMaterialMaterials = null;
            }
        }
    }
}
