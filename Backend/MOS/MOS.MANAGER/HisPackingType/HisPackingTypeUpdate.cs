using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackingType
{
    partial class HisPackingTypeUpdate : BusinessBase
    {
		private List<HIS_PACKING_TYPE> beforeUpdateHisPackingTypes = new List<HIS_PACKING_TYPE>();
		
        internal HisPackingTypeUpdate()
            : base()
        {

        }

        internal HisPackingTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PACKING_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackingTypeCheck checker = new HisPackingTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PACKING_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PACKING_TYPE_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisPackingTypes.Add(raw);
					if (!DAOWorker.HisPackingTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackingType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackingType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PACKING_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackingTypeCheck checker = new HisPackingTypeCheck(param);
                List<HIS_PACKING_TYPE> listRaw = new List<HIS_PACKING_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PACKING_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPackingTypes.AddRange(listRaw);
					if (!DAOWorker.HisPackingTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackingType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackingType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPackingTypes))
            {
                if (!new HisPackingTypeUpdate(param).UpdateList(this.beforeUpdateHisPackingTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisPackingType that bai, can kiem tra lai." + LogUtil.TraceData("HisPackingTypes", this.beforeUpdateHisPackingTypes));
                }
            }
        }
    }
}
