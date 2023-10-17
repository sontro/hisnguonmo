using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrainUnit
{
    partial class HisRehaTrainUnitCreate : BusinessBase
    {
        private List<HIS_REHA_TRAIN_UNIT> recentHisRehaTrainUnits = new List<HIS_REHA_TRAIN_UNIT>();

        internal HisRehaTrainUnitCreate()
            : base()
        {

        }

        internal HisRehaTrainUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REHA_TRAIN_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainUnitCheck checker = new HisRehaTrainUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REHA_TRAIN_UNIT_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisRehaTrainUnitDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrainUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaTrainUnit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRehaTrainUnits.Add(data);
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

        internal bool CreateList(List<HIS_REHA_TRAIN_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainUnitCheck checker = new HisRehaTrainUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRehaTrainUnitDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrainUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaTrainUnit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRehaTrainUnits.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRehaTrainUnits))
            {
                if (!new HisRehaTrainUnitTruncate(param).TruncateList(this.recentHisRehaTrainUnits))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaTrainUnit that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRehaTrainUnits", this.recentHisRehaTrainUnits));
                }
            }
        }
    }
}
