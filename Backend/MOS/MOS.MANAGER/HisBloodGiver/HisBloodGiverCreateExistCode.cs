using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverCreate : BusinessBase
    {
        private List<HIS_BLOOD_GIVER> recentHisBloodGivers = new List<HIS_BLOOD_GIVER>();

        internal HisBloodGiverCreate()
            : base()
        {

        }

        internal HisBloodGiverCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_GIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.GIVE_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisBloodGiverDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGiver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodGiver that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodGivers.Add(data);
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

        internal bool CreateList(List<HIS_BLOOD_GIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.GIVE_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBloodGiverDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGiver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodGiver that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBloodGivers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBloodGivers))
            {
                if (!DAOWorker.HisBloodGiverDAO.TruncateList(this.recentHisBloodGivers))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodGiver that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBloodGivers", this.recentHisBloodGivers));
                }
                this.recentHisBloodGivers = null;
            }
        }
    }
}
