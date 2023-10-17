using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapUpdate : BusinessBase
    {
		private List<HIS_MATERIAL_TYPE_MAP> beforeUpdateHisMaterialTypeMaps = new List<HIS_MATERIAL_TYPE_MAP>();
		
        internal HisMaterialTypeMapUpdate()
            : base()
        {

        }

        internal HisMaterialTypeMapUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATERIAL_TYPE_MAP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MATERIAL_TYPE_MAP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_MAP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMaterialTypeMapDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialTypeMap_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialTypeMap that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMaterialTypeMaps.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                List<HIS_MATERIAL_TYPE_MAP> listRaw = new List<HIS_MATERIAL_TYPE_MAP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_MAP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMaterialTypeMapDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialTypeMap_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialTypeMap that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMaterialTypeMaps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMaterialTypeMaps))
            {
                if (!DAOWorker.HisMaterialTypeMapDAO.UpdateList(this.beforeUpdateHisMaterialTypeMaps))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialTypeMap that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialTypeMaps", this.beforeUpdateHisMaterialTypeMaps));
                }
				this.beforeUpdateHisMaterialTypeMaps = null;
            }
        }
    }
}
