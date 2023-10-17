using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointCreate : BusinessBase
    {
		private List<HIS_CONTACT_POINT> recentHisContactPoints = new List<HIS_CONTACT_POINT>();
		
        internal HisContactPointCreate()
            : base()
        {

        }

        internal HisContactPointCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CONTACT_POINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactPointCheck checker = new HisContactPointCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    data.LAST_NAME = data.LAST_NAME != null ? data.LAST_NAME.ToUpper() : null;
                    data.FIRST_NAME = data.FIRST_NAME != null ? data.FIRST_NAME.ToUpper() : null;

					if (!DAOWorker.HisContactPointDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContactPoint_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisContactPoint that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisContactPoints.Add(data);
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
		
		internal bool CreateList(List<HIS_CONTACT_POINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactPointCheck checker = new HisContactPointCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisContactPointDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContactPoint_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisContactPoint that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisContactPoints.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisContactPoints))
            {
                if (!DAOWorker.HisContactPointDAO.TruncateList(this.recentHisContactPoints))
                {
                    LogSystem.Warn("Rollback du lieu HisContactPoint that bai, can kiem tra lai." + LogUtil.TraceData("recentHisContactPoints", this.recentHisContactPoints));
                }
				this.recentHisContactPoints = null;
            }
        }
    }
}
