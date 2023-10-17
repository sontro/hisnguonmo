using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCondition
{
    partial class HisPtttConditionCreate : BusinessBase
    {
		private HIS_PTTT_CONDITION recentHisPtttConditionDTO;
		private List<HIS_PTTT_CONDITION> recentHisPtttConditionDTOs;
		
        internal HisPtttConditionCreate()
            : base()
        {

        }

        internal HisPtttConditionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttConditionCheck checker = new HisPtttConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PTTT_CONDITION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPtttConditionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCondition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttCondition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttConditionDTO = data;
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

        internal bool CreateList(List<HIS_PTTT_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttConditionCheck checker = new HisPtttConditionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_CONDITION_CODE, null);
                }
                if (valid)
                {
					if (!DAOWorker.HisPtttConditionDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCondition_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisPtttCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.recentHisPtttConditionDTOs = listData;
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
            if (this.recentHisPtttConditionDTO != null)
            {
                if (!new HisPtttConditionTruncate(param).Truncate(this.recentHisPtttConditionDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttConditionDTO", this.recentHisPtttConditionDTO));
                }
            }
			
			if (this.recentHisPtttConditionDTOs != null)
            {
                if (!new HisPtttConditionTruncate(param).TruncateList(this.recentHisPtttConditionDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttConditionDTOs", this.recentHisPtttConditionDTOs));
                }
            }
        }
    }
}
