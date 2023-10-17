using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    partial class HisContactCreate : BusinessBase
    {
		private List<HIS_CONTACT> recentHisContacts = new List<HIS_CONTACT>();
		
        internal HisContactCreate()
            : base()
        {

        }

        internal HisContactCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CONTACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactCheck checker = new HisContactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisContactDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContact_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisContact that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisContacts.Add(data);
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
		
		internal bool CreateList(List<HIS_CONTACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactCheck checker = new HisContactCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisContactDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContact_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisContact that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisContacts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisContacts))
            {
                if (!DAOWorker.HisContactDAO.TruncateList(this.recentHisContacts))
                {
                    LogSystem.Warn("Rollback du lieu HisContact that bai, can kiem tra lai." + LogUtil.TraceData("recentHisContacts", this.recentHisContacts));
                }
				this.recentHisContacts = null;
            }
        }
    }
}
