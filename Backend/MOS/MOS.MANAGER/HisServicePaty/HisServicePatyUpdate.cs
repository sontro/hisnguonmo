using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServicePaty
{
    class HisServicePatyUpdate : BusinessBase
    {
        private HIS_SERVICE_PATY beforeUpdateHisServicePatyDTO;
        internal HisServicePatyUpdate()
            : base()
        {

        }

        internal HisServicePatyUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_PATY raw = null;
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckHeinPrice(data);
                if (valid)
                {
                    this.beforeUpdateHisServicePatyDTO = raw;
                    if (!DAOWorker.HisServicePatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServicePaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServicePaty that bai." + LogUtil.TraceData("data", data));
                    }
                    result = DAOWorker.HisServicePatyDAO.Update(data);
                    HisServicePatyLog.Run(data, raw, LibraryEventLog.EventLog.Enum.HisServicePaty_SuaChinhSachGiaDichVu);
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

        internal bool UpdateList(List<HIS_SERVICE_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                List<HIS_SERVICE_PATY> listRaw = new List<HIS_SERVICE_PATY>();
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckHeinPrice(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisServicePatyDAO.UpdateList(listData);
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
    }
}
