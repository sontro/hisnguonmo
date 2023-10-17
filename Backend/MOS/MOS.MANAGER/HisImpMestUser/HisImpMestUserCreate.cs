using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestUser
{
    partial class HisImpMestUserCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_USER> recentHisImpMestUsers = new List<HIS_IMP_MEST_USER>();

        internal HisImpMestUserCreate()
            : base()
        {

        }

        internal HisImpMestUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestUsers.Add(data);
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

        internal bool CreateList(List<HIS_IMP_MEST_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestUsers.AddRange(listData);
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

        internal bool CreateOrReplace(HisImpMestUserSDO data, ref List<HIS_IMP_MEST_USER> resultData)
        {
            bool result = false;
            try
            {
                if (data == null || data.ImpMestId <= 0 || (data.ImpMestUsers != null && data.ImpMestUsers.Select(o => o.LOGINNAME).Distinct().Count() != data.ImpMestUsers.Count()))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Du lieu ko hop le. Du lieu truyen vao ko duoc trung loginname");
                    return false;
                }

                List<HIS_IMP_MEST_USER> impMestUsers = new HisImpMestUserGet().GetByImpMestId(data.ImpMestId);
                if (IsNotNullOrEmpty(impMestUsers))
                {
                    if (!new HisImpMestUserTruncate().TruncateList(impMestUsers))
                    {
                        return false;
                    }
                }

                if (!IsNotNullOrEmpty(data.ImpMestUsers))
                {
                    return true;
                }

                data.ImpMestUsers.ForEach(o => o.IMP_MEST_ID = data.ImpMestId);
                if (this.CreateList(data.ImpMestUsers))
                {
                    result = true;
                    resultData = data.ImpMestUsers;
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
            if (IsNotNullOrEmpty(this.recentHisImpMestUsers))
            {
                if (!new HisImpMestUserTruncate(param).TruncateList(this.recentHisImpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestUsers", this.recentHisImpMestUsers));
                }
            }
        }
    }
}