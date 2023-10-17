using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttMethod
{
    partial class HisPtttMethodCreate : BusinessBase
    {
        private List<HIS_PTTT_METHOD> recentHisPtttMethodDTOs = new List<HIS_PTTT_METHOD>();

        internal HisPtttMethodCreate()
            : base()
        {

        }

        internal HisPtttMethodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PTTT_METHOD_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisPtttMethodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttMethod that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttMethodDTOs.Add(data);
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

        internal bool CreateList(List<HIS_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (HIS_PTTT_METHOD data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_METHOD_CODE, null);
                }

                if (valid)
                {
                    if (!DAOWorker.HisPtttMethodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttMethod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttMethod that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPtttMethodDTOs.AddRange(listData);
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
            if (this.recentHisPtttMethodDTOs != null)
            {
                if (!new HisPtttMethodTruncate(param).TruncateList(this.recentHisPtttMethodDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttMethodDTOs", this.recentHisPtttMethodDTOs));
                }
            }
        }
    }
}
