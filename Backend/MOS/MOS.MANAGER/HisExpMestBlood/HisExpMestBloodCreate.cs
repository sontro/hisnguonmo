using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBlood
{
    partial class HisExpMestBloodCreate : BusinessBase
    {
		private List<HIS_EXP_MEST_BLOOD> recentHisExpMestBloods = new List<HIS_EXP_MEST_BLOOD>();
		
        internal HisExpMestBloodCreate()
            : base()
        {

        }

        internal HisExpMestBloodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestBloodCheck checker = new HisExpMestBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    //trong truong hop ke don thi lay theo thong tin do nguoi dung nhap
                    data.APPROVAL_LOGINNAME = string.IsNullOrWhiteSpace(data.APPROVAL_LOGINNAME) ? loginName : data.APPROVAL_LOGINNAME;
                    data.APPROVAL_USERNAME = string.IsNullOrWhiteSpace(data.APPROVAL_USERNAME) ? userName : data.APPROVAL_USERNAME;
                    data.APPROVAL_TIME = data.APPROVAL_TIME <= 0 ? Inventec.Common.DateTime.Get.Now().Value : data.APPROVAL_TIME;

					if (!DAOWorker.HisExpMestBloodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestBlood that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestBloods.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_BLOOD> listData)
        {
            bool result = false;
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestBloodCheck checker = new HisExpMestBloodCheck(param);
                foreach (var data in listData)
                {
                    //trong truong hop ke don thi lay theo thong tin do nguoi dung nhap
                    data.APPROVAL_LOGINNAME = string.IsNullOrWhiteSpace(data.APPROVAL_LOGINNAME) ? loginName : data.APPROVAL_LOGINNAME;
                    data.APPROVAL_USERNAME = string.IsNullOrWhiteSpace(data.APPROVAL_USERNAME) ? userName : data.APPROVAL_USERNAME;
                    data.APPROVAL_TIME = data.APPROVAL_TIME <= 0 ? Inventec.Common.DateTime.Get.Now().Value : data.APPROVAL_TIME;
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestBloodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestBloods.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExpMestBloods))
            {
                if (!new HisExpMestBloodTruncate(param).TruncateList(this.recentHisExpMestBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestBlood that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestBloods", this.recentHisExpMestBloods));
                }
            }
        }
    }
}
