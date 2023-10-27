using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpCreate : BusinessBase
    {
		private List<ACS_OTP> recentAcsOtps = new List<ACS_OTP>();
		
        internal AcsOtpCreate()
            : base()
        {

        }

        internal AcsOtpCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(ACS_OTP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.AcsOtpDAO.Create(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsOtp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsOtp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentAcsOtps.Add(data);
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
		
		internal bool CreateList(List<ACS_OTP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                AcsOtpCheck checker = new AcsOtpCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.AcsOtpDAO.CreateList(listData))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsOtp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsOtp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentAcsOtps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentAcsOtps))
            {
                if (!DAOWorker.AcsOtpDAO.TruncateList(this.recentAcsOtps))
                {
                    LogSystem.Warn("Rollback du lieu AcsOtp that bai, can kiem tra lai." + LogUtil.TraceData("recentAcsOtps", this.recentAcsOtps));
                }
				this.recentAcsOtps = null;
            }
        }
    }
}
