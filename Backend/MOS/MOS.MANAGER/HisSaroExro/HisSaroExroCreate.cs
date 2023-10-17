using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSaroExro
{
    partial class HisSaroExroCreate : BusinessBase
    {
		private List<HIS_SARO_EXRO> recentHisSaroExros = new List<HIS_SARO_EXRO>();
		
        internal HisSaroExroCreate()
            : base()
        {

        }

        internal HisSaroExroCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SARO_EXRO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSaroExroDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaroExro_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSaroExro that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSaroExros.Add(data);
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

        internal bool CreateList(List<HIS_SARO_EXRO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisSaroExroDAO.CreateList(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSaroExros))
            {
                if (!new HisSaroExroTruncate(param).TruncateList(this.recentHisSaroExros))
                {
                    LogSystem.Warn("Rollback du lieu HisSaroExro that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSaroExros", this.recentHisSaroExros));
                }
            }
        }
    }
}
