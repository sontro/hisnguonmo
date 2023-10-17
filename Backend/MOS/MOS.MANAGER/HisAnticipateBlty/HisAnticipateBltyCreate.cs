using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateBlty
{
    partial class HisAnticipateBltyCreate : BusinessBase
    {
		private List<HIS_ANTICIPATE_BLTY> recentHisAnticipateBltys = new List<HIS_ANTICIPATE_BLTY>();
		
        internal HisAnticipateBltyCreate()
            : base()
        {

        }

        internal HisAnticipateBltyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTICIPATE_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAnticipateBltyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateBlty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateBlty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAnticipateBltys.Add(data);
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
		
		internal bool CreateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAnticipateBltyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateBlty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateBlty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAnticipateBltys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAnticipateBltys))
            {
                if (!new HisAnticipateBltyTruncate(param).TruncateList(this.recentHisAnticipateBltys))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipateBlty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAnticipateBltys", this.recentHisAnticipateBltys));
                }
            }
        }
    }
}
