using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapCreate : BusinessBase
    {
		private List<HIS_MATERIAL_TYPE_MAP> recentHisMaterialTypeMaps = new List<HIS_MATERIAL_TYPE_MAP>();
		
        internal HisMaterialTypeMapCreate()
            : base()
        {

        }

        internal HisMaterialTypeMapCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATERIAL_TYPE_MAP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMaterialTypeMapDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialTypeMap_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialTypeMap that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMaterialTypeMaps.Add(data);
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
		
		internal bool CreateList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialTypeMapCheck checker = new HisMaterialTypeMapCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialTypeMapDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialTypeMap_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialTypeMap that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMaterialTypeMaps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMaterialTypeMaps))
            {
                if (!DAOWorker.HisMaterialTypeMapDAO.TruncateList(this.recentHisMaterialTypeMaps))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialTypeMap that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMaterialTypeMaps", this.recentHisMaterialTypeMaps));
                }
				this.recentHisMaterialTypeMaps = null;
            }
        }
    }
}
