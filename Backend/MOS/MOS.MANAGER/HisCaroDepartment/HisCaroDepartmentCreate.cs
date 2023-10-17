using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentCreate : BusinessBase
    {
        private List<HIS_CARO_DEPARTMENT> recentHisCaroDepartments = new List<HIS_CARO_DEPARTMENT>();

        internal HisCaroDepartmentCreate()
            : base()
        {

        }

        internal HisCaroDepartmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARO_DEPARTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisCaroDepartmentDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroDepartment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCaroDepartment that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCaroDepartments.Add(data);
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

        internal bool CreateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCaroDepartmentDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroDepartment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCaroDepartment that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCaroDepartments.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCaroDepartments))
            {
                if (!new HisCaroDepartmentTruncate(param).TruncateList(this.recentHisCaroDepartments))
                {
                    LogSystem.Warn("Rollback du lieu HisCaroDepartment that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCaroDepartments", this.recentHisCaroDepartments));
                }
            }
        }
    }
}
