using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestUser
{
    partial class HisExpMestUserCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_USER> recentHisExpMestUsers = new List<HIS_EXP_MEST_USER>();

        internal HisExpMestUserCreate()
            : base()
        {

        }

        internal HisExpMestUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestUserCheck checker = new HisExpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestUsers.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestUserCheck checker = new HisExpMestUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestUsers.AddRange(listData);
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

        internal bool CreateOrReplace(HisExpMestUserSDO data, ref List<HIS_EXP_MEST_USER> resultData)
        {
            bool result = false;
            try
            {
                if (data == null || data.ExpMestId <= 0 || (data.ExpMestUsers != null && data.ExpMestUsers.Select(o => o.LOGINNAME).Distinct().Count() != data.ExpMestUsers.Count()))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Du lieu ko hop le. Du lieu truyen vao ko duoc trung loginname");
                    return false;
                }

                List<HIS_EXP_MEST_USER> expMestUsers = new HisExpMestUserGet().GetByExpMestId(data.ExpMestId);
                if (IsNotNullOrEmpty(expMestUsers))
                {
                    if (!new HisExpMestUserTruncate().TruncateList(expMestUsers))
                    {
                        return false;
                    }
                }

                if (!IsNotNullOrEmpty(data.ExpMestUsers))
                {
                    return true;
                }

                data.ExpMestUsers.ForEach(o => o.EXP_MEST_ID = data.ExpMestId);
                if (this.CreateList(data.ExpMestUsers))
                {
                    result = true;
                    resultData = data.ExpMestUsers;
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
            if (IsNotNullOrEmpty(this.recentHisExpMestUsers))
            {
                if (!new HisExpMestUserTruncate(param).TruncateList(this.recentHisExpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestUsers", this.recentHisExpMestUsers));
                }
            }
        }
    }
}
