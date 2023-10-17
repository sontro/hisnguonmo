using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    partial class HisMedicineMaterialCreate : BusinessBase
    {
		private List<HIS_MEDICINE_MATERIAL> recentHisMedicineMaterials = new List<HIS_MEDICINE_MATERIAL>();
		
        internal HisMedicineMaterialCreate()
            : base()
        {

        }

        internal HisMedicineMaterialCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineMaterialCheck checker = new HisMedicineMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMedicineMaterialDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineMaterials.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICINE_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineMaterialCheck checker = new HisMedicineMaterialCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineMaterialDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicineMaterials.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineMaterials))
            {
                if (!DAOWorker.HisMedicineMaterialDAO.TruncateList(this.recentHisMedicineMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineMaterial that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineMaterials", this.recentHisMedicineMaterials));
                }
				this.recentHisMedicineMaterials = null;
            }
        }
    }
}
