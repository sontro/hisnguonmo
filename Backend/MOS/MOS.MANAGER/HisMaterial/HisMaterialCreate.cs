using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialCreate : BusinessBase
    {
        private List<HIS_MATERIAL> recentHisMaterialDTOs;

        internal HisMaterialCreate()
            : base()
        {

        }

        internal HisMaterialCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialCheck checker = new HisMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    if (this.recentHisMaterialDTOs == null)
                    {
                        this.recentHisMaterialDTOs = new List<HIS_MATERIAL>();
                    }
                    this.recentHisMaterialDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialCheck checker = new HisMaterialCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    if (this.recentHisMaterialDTOs == null)
                    {
                        this.recentHisMaterialDTOs = new List<HIS_MATERIAL>();
                    }
                    this.recentHisMaterialDTOs.AddRange(listData);
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
            if (this.recentHisMaterialDTOs != null)
            {
                if (!DAOWorker.HisMaterialDAO.TruncateList(this.recentHisMaterialDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialDTOs", this.recentHisMaterialDTOs));
                }
            }
        }
    }
}
