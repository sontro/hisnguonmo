using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisDepartmentTran.Receive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    class HisTreatmentBedRoomUpdateTime : BusinessBase
    {
        internal HisTreatmentBedRoomUpdateTime()
            : base()
        {

        }

        internal HisTreatmentBedRoomUpdateTime(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TREATMENT_BED_ROOM data, ref HIS_TREATMENT_BED_ROOM resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT_BED_ROOM raw = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisDepartmentTranReceiveCheck dReciveChecker = new HisDepartmentTranReceiveCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.CheckValid(data, raw);
                valid = valid && treatChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && treatChecker.IsUnpause(treatment);
                valid = valid && treatChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
                valid = valid && treatChecker.IsUnLockHein(treatment);
                valid = valid && checker.IsValidDepartmentTranTime(data);
                valid = valid && dReciveChecker.IsValidInTime(data.ADD_TIME, true);

                if (valid)
                {
                    string addTimeBefore = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(raw.ADD_TIME);
                    string removeTimeBefore = raw.REMOVE_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(raw.REMOVE_TIME.Value) : "";

                    raw.ADD_TIME = data.ADD_TIME;
                    raw.REMOVE_TIME = data.REMOVE_TIME;
                    if (DAOWorker.HisTreatmentBedRoomDAO.Update(raw))
                    {
                        result = true;
                        resultData = raw;

                        string addTimeAfter = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(raw.ADD_TIME);
                        string removeTimeAfter = raw.REMOVE_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(raw.REMOVE_TIME.Value) : "";
                        string bedRoomName = HisBedRoomCFG.DATA != null ? HisBedRoomCFG.DATA.Where(o => o.ID == raw.BED_ROOM_ID).Select(o => o.BED_ROOM_NAME).FirstOrDefault() : "";
                        new EventLogGenerator(EventLog.Enum.HisTreatment_SuaThongTinNamBuong, bedRoomName, addTimeBefore, addTimeAfter, removeTimeBefore, removeTimeAfter).TreatmentCode(treatment.TREATMENT_CODE).Run();
                    }

                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool CheckValid(HIS_TREATMENT_BED_ROOM data, HIS_TREATMENT_BED_ROOM raw)
        {
            bool valid = true;
            try
            {
                if (raw.REMOVE_TIME.HasValue && !data.REMOVE_TIME.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("old co REMOVE_TIME, new khong co REMOVE_TIME");
                }
                if (data.REMOVE_TIME.HasValue && !raw.REMOVE_TIME.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("old khong co REMOVE_TIME, new co REMOVE_TIME");
                }
                if (data.REMOVE_TIME.HasValue && data.REMOVE_TIME.Value < data.ADD_TIME)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Thoi gian ra be hon thoi gian vao");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
