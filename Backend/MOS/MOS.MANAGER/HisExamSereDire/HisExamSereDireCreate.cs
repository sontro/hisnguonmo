using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSereDire
{
    partial class HisExamSereDireCreate : BusinessBase
    {
		private List<HIS_EXAM_SERE_DIRE> recentHisExamSereDireDTOs = new List<HIS_EXAM_SERE_DIRE>();
		
        internal HisExamSereDireCreate()
            : base()
        {

        }

        internal HisExamSereDireCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXAM_SERE_DIRE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExamSereDireDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSereDire_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExamSereDire that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExamSereDireDTOs.Add(data);
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

        internal bool CreateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExamSereDireDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSereDire_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisExamSereDire that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.recentHisExamSereDireDTOs.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisExamSereDireDTOs))
            {
                if (!new HisExamSereDireTruncate(param).TruncateList(this.recentHisExamSereDireDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisExamSereDire that bai, can kiem tra lai." + LogUtil.TraceData("HisExamSereDireDTOs", this.recentHisExamSereDireDTOs));
                }
            }
        }
    }
}
