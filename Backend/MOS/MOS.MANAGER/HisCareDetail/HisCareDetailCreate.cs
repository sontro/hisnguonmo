using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareDetail
{
    partial class HisCareDetailCreate : BusinessBase
    {
        private List<HIS_CARE_DETAIL> recentHisCareDetails = new List<HIS_CARE_DETAIL>();

        internal HisCareDetailCreate()
            : base()
        {

        }

        internal HisCareDetailCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisCareDetailDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareDetail that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCareDetails.Add(data);
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

        internal bool CreateList(List<HIS_CARE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCareDetailDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCareDetails.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCareDetails))
            {
                if (!new HisCareDetailTruncate(param).TruncateList(this.recentHisCareDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisCareDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisCareDetail", this.recentHisCareDetails));
                }
            }
        }
    }
}
