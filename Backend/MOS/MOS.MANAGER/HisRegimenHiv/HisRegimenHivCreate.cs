using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    partial class HisRegimenHivCreate : BusinessBase
    {
		private List<HIS_REGIMEN_HIV> recentHisRegimenHivs = new List<HIS_REGIMEN_HIV>();
		
        internal HisRegimenHivCreate()
            : base()
        {

        }

        internal HisRegimenHivCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REGIMEN_HIV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REGIMEN_HIV_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRegimenHivDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegimenHiv_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRegimenHiv that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRegimenHivs.Add(data);
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
		
		internal bool CreateList(List<HIS_REGIMEN_HIV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REGIMEN_HIV_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRegimenHivDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegimenHiv_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRegimenHiv that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRegimenHivs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRegimenHivs))
            {
                if (!DAOWorker.HisRegimenHivDAO.TruncateList(this.recentHisRegimenHivs))
                {
                    LogSystem.Warn("Rollback du lieu HisRegimenHiv that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRegimenHivs", this.recentHisRegimenHivs));
                }
				this.recentHisRegimenHivs = null;
            }
        }
    }
}
