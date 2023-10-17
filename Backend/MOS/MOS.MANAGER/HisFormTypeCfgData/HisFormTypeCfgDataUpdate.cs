using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataUpdate : BusinessBase
    {
		private List<HIS_FORM_TYPE_CFG_DATA> beforeUpdateHisFormTypeCfgDatas = new List<HIS_FORM_TYPE_CFG_DATA>();
		
        internal HisFormTypeCfgDataUpdate()
            : base()
        {

        }

        internal HisFormTypeCfgDataUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FORM_TYPE_CFG_DATA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FORM_TYPE_CFG_DATA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckExists(data.FORM_TYPE_CFG_ID, data.FORM_TYPE_CODE, raw.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisFormTypeCfgDataDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfgData_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFormTypeCfgData that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisFormTypeCfgDatas.Add(raw);
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

        internal bool UpdateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                List<HIS_FORM_TYPE_CFG_DATA> listRaw = new List<HIS_FORM_TYPE_CFG_DATA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckExists(data.FORM_TYPE_CFG_ID, data.FORM_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisFormTypeCfgDataDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfgData_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFormTypeCfgData that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisFormTypeCfgDatas.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFormTypeCfgDatas))
            {
                if (!DAOWorker.HisFormTypeCfgDataDAO.UpdateList(this.beforeUpdateHisFormTypeCfgDatas))
                {
                    LogSystem.Warn("Rollback du lieu HisFormTypeCfgData that bai, can kiem tra lai." + LogUtil.TraceData("HisFormTypeCfgDatas", this.beforeUpdateHisFormTypeCfgDatas));
                }
				this.beforeUpdateHisFormTypeCfgDatas = null;
            }
        }
    }
}
