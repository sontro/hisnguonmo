using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    partial class HisMaterialMaterialCreate : BusinessBase
    {
		private List<HIS_MATERIAL_MATERIAL> recentHisMaterialMaterials = new List<HIS_MATERIAL_MATERIAL>();
		
        internal HisMaterialMaterialCreate()
            : base()
        {

        }

        internal HisMaterialMaterialCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATERIAL_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMaterialMaterialDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMaterialMaterials.Add(data);
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
		
		internal bool CreateList(List<HIS_MATERIAL_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialMaterialCheck checker = new HisMaterialMaterialCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialMaterialDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMaterialMaterials.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMaterialMaterials))
            {
                if (!DAOWorker.HisMaterialMaterialDAO.TruncateList(this.recentHisMaterialMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialMaterial that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMaterialMaterials", this.recentHisMaterialMaterials));
                }
				this.recentHisMaterialMaterials = null;
            }
        }
    }
}
