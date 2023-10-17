using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrain
{
    partial class HisRehaTrainCreate : BusinessBase
    {
        private List<HIS_REHA_TRAIN> recentHisRehaTrains = new List<HIS_REHA_TRAIN>();

        internal HisRehaTrainCreate()
            : base()
        {

        }

        internal HisRehaTrainCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REHA_TRAIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisRehaTrainDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrain_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaTrain that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRehaTrains.Add(data);
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

        internal bool CreateList(List<HIS_REHA_TRAIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRehaTrainDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrain_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaTrain that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRehaTrains.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRehaTrains))
            {
                if (!new HisRehaTrainTruncate(param).TruncateList(this.recentHisRehaTrains))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaTrain that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRehaTrains", this.recentHisRehaTrains));
                }
            }
        }
    }
}
