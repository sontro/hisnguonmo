using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeCreate : BusinessBase
    {
        private List<HIS_ACCIDENT_HURT_TYPE> recentHisAccidentHurtTypes = new List<HIS_ACCIDENT_HURT_TYPE>();

        internal HisAccidentHurtTypeCreate()
            : base()
        {

        }

        internal HisAccidentHurtTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_HURT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisAccidentHurtTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurtType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentHurtType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentHurtTypes.Add(data);
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

        internal bool CreateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAccidentHurtTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurtType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentHurtType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAccidentHurtTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentHurtTypes))
            {
                if (!new HisAccidentHurtTypeTruncate(param).TruncateList(this.recentHisAccidentHurtTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHurtType that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentHurtType", this.recentHisAccidentHurtTypes));
                }
            }
        }
    }
}
