using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataCreate : BusinessBase
    {
		private List<HIS_FORM_TYPE_CFG_DATA> recentHisFormTypeCfgDatas = new List<HIS_FORM_TYPE_CFG_DATA>();
		
        internal HisFormTypeCfgDataCreate()
            : base()
        {

        }

        internal HisFormTypeCfgDataCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FORM_TYPE_CFG_DATA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckExists(data.FORM_TYPE_CFG_ID, data.FORM_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisFormTypeCfgDataDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfgData_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFormTypeCfgData that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFormTypeCfgDatas.Add(data);
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
		
		internal bool CreateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFormTypeCfgDataCheck checker = new HisFormTypeCfgDataCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckExists(data.FORM_TYPE_CFG_ID, data.FORM_TYPE_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisFormTypeCfgDataDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfgData_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFormTypeCfgData that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisFormTypeCfgDatas.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisFormTypeCfgDatas))
            {
                if (!DAOWorker.HisFormTypeCfgDataDAO.TruncateList(this.recentHisFormTypeCfgDatas))
                {
                    LogSystem.Warn("Rollback du lieu HisFormTypeCfgData that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFormTypeCfgDatas", this.recentHisFormTypeCfgDatas));
                }
				this.recentHisFormTypeCfgDatas = null;
            }
        }
    }
}
