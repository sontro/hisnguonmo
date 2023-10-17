using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceCreate : BusinessBase
    {
		private List<HIS_EXP_BLTY_SERVICE> recentHisExpBltyServices = new List<HIS_EXP_BLTY_SERVICE>();
		
        internal HisExpBltyServiceCreate()
            : base()
        {

        }

        internal HisExpBltyServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisExpBltyServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpBltyService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpBltyService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpBltyServices.Add(data);
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
		
		internal bool CreateList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpBltyServiceDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpBltyService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpBltyService that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpBltyServices.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExpBltyServices))
            {
                if (!DAOWorker.HisExpBltyServiceDAO.TruncateList(this.recentHisExpBltyServices))
                {
                    LogSystem.Warn("Rollback du lieu HisExpBltyService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpBltyServices", this.recentHisExpBltyServices));
                }
				this.recentHisExpBltyServices = null;
            }
        }
    }
}
