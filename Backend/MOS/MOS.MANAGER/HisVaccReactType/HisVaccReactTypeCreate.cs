using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactType
{
    partial class HisVaccReactTypeCreate : BusinessBase
    {
		private List<HIS_VACC_REACT_TYPE> recentHisVaccReactTypes = new List<HIS_VACC_REACT_TYPE>();
		
        internal HisVaccReactTypeCreate()
            : base()
        {

        }

        internal HisVaccReactTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACC_REACT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccReactTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccReactType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccReactTypes.Add(data);
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
		
		internal bool CreateList(List<HIS_VACC_REACT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccReactTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccReactType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccReactTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccReactTypes))
            {
                if (!DAOWorker.HisVaccReactTypeDAO.TruncateList(this.recentHisVaccReactTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccReactType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccReactTypes", this.recentHisVaccReactTypes));
                }
				this.recentHisVaccReactTypes = null;
            }
        }
    }
}
