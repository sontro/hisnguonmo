using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaTrain
{
    partial class HisRehaTrainUpdate : BusinessBase
    {
        private List<HIS_REHA_TRAIN> beforeUpdateHisRehaTrains = new List<HIS_REHA_TRAIN>();

        internal HisRehaTrainUpdate()
            : base()
        {

        }

        internal HisRehaTrainUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REHA_TRAIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REHA_TRAIN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisRehaTrains.Add(raw);
                    if (!DAOWorker.HisRehaTrainDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrain_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaTrain that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_REHA_TRAIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                List<HIS_REHA_TRAIN> listRaw = new List<HIS_REHA_TRAIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisRehaTrains.AddRange(listRaw);
                    if (!DAOWorker.HisRehaTrainDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrain_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaTrain that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRehaTrains))
            {
                if (!new HisRehaTrainUpdate(param).UpdateList(this.beforeUpdateHisRehaTrains))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaTrain that bai, can kiem tra lai." + LogUtil.TraceData("HisRehaTrains", this.beforeUpdateHisRehaTrains));
                }
            }
        }
    }
}