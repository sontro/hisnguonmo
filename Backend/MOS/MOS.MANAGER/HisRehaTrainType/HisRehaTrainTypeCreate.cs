using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrainType
{
    partial class HisRehaTrainTypeCreate : BusinessBase
    {
		private List<HIS_REHA_TRAIN_TYPE> recentHisRehaTrainTypes = new List<HIS_REHA_TRAIN_TYPE>();
		
        internal HisRehaTrainTypeCreate()
            : base()
        {

        }

        internal HisRehaTrainTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REHA_TRAIN_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REHA_TRAIN_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRehaTrainTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrainType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaTrainType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRehaTrainTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRehaTrainTypes))
            {
                if (!new HisRehaTrainTypeTruncate(param).TruncateList(this.recentHisRehaTrainTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaTrainType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRehaTrainTypes", this.recentHisRehaTrainTypes));
                }
            }
        }
    }
}
