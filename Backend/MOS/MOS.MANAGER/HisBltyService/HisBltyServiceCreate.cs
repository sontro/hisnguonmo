using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    partial class HisBltyServiceCreate : BusinessBase
    {
		private List<HIS_BLTY_SERVICE> recentHisBltyServices = new List<HIS_BLTY_SERVICE>();
		
        internal HisBltyServiceCreate()
            : base()
        {

        }

        internal HisBltyServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
					if (!DAOWorker.HisBltyServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBltyService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBltyServices.Add(data);
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
		
		internal bool CreateList(List<HIS_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBltyServiceDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBltyService that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBltyServices.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBltyServices))
            {
                if (!DAOWorker.HisBltyServiceDAO.TruncateList(this.recentHisBltyServices))
                {
                    LogSystem.Warn("Rollback du lieu HisBltyService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBltyServices", this.recentHisBltyServices));
                }
				this.recentHisBltyServices = null;
            }
        }
    }
}
