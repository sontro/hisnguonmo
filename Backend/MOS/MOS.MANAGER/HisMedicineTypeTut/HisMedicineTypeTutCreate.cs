using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutCreate : BusinessBase
    {
		private List<HIS_MEDICINE_TYPE_TUT> recentHisMedicineTypeTuts = new List<HIS_MEDICINE_TYPE_TUT>();
		
        internal HisMedicineTypeTutCreate()
            : base()
        {

        }

        internal HisMedicineTypeTutCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_TYPE_TUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                //valid = valid && checker.IsNotExisted(data); //cho phep tao nhieu HDSD voi 1 thuoc
                if (valid)
                {
					if (!DAOWorker.HisMedicineTypeTutDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeTut_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineTypeTut that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineTypeTuts.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineTypeTutDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeTut_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineTypeTut that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineTypeTuts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineTypeTuts))
            {
                if (!new HisMedicineTypeTutTruncate(param).TruncateList(this.recentHisMedicineTypeTuts))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineTypeTut that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineTypeTuts", this.recentHisMedicineTypeTuts));
                }
            }
        }
    }
}
