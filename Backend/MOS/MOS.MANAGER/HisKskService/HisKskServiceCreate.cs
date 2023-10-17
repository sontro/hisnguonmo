using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceCreate : BusinessBase
    {
        private List<HIS_KSK_SERVICE> recentHisKskServices = new List<HIS_KSK_SERVICE>();

        internal HisKskServiceCreate()
            : base()
        {

        }

        internal HisKskServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckServicePaty(data);
                valid = valid && checker.CheckExecuteRoom(new List<HIS_KSK_SERVICE>() { data });
                if (valid)
                {
                    if (!DAOWorker.HisKskServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskServices.Add(data);
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

        internal bool CreateList(List<HIS_KSK_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && checker.CheckExecuteRoom(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckServicePaty(data);
                }


                if (valid)
                {
                    if (!DAOWorker.HisKskServiceDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskService that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskServices.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskServices))
            {
                if (!DAOWorker.HisKskServiceDAO.TruncateList(this.recentHisKskServices))
                {
                    LogSystem.Warn("Rollback du lieu HisKskService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskServices", this.recentHisKskServices));
                }
                this.recentHisKskServices = null;
            }
        }
    }
}
