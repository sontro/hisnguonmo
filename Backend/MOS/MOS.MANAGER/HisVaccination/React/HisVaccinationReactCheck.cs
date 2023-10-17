using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React
{
    class HisVaccinationReactCheck : BusinessBase
    {
        internal HisVaccinationReactCheck()
            : base()
        {

        }

        internal HisVaccinationReactCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckReactResponse(HisVaccReactInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data.IsReactResponse.HasValue && data.IsReactResponse.Value)
                {
                    if (String.IsNullOrWhiteSpace(data.ReactResponser)
                        || String.IsNullOrWhiteSpace(data.ReactReporter))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("IsReactResponse is true, ReactResponser or ReactReporter is null");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckDeathInfo(HisVaccReactInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data.VaccHealthSttId.HasValue &&
                    data.VaccHealthSttId.Value == IMSys.DbConfig.HIS_RS.HIS_VACC_HEALTH_STT.ID__TU_VONG)
                {
                    if (!data.DeathTime.HasValue)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("VaccHealthSttId is death, DeathTime is null");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidData(HisVaccReactInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.VaccinationId <= 0) throw new ArgumentNullException("data.VaccinationId");
                if (data.VaccinationReactId <= 0) throw new ArgumentNullException("data.VaccinationReactId");
                if (data.ReactTime <= 0) throw new ArgumentNullException("data.ReactTime");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasReactInfo(HIS_VACCINATION data)
        {
            bool valid = true;
            try
            {
                if (!data.VACCINATION_REACT_ID.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisVaccination_KhongCoThongTinPhanUngSauTiem);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsFollowerOrAdmin(HIS_VACCINATION raw)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (HisEmployeeUtil.IsAdmin(loginname))
                {
                    return true;
                }
                if (raw.FOLLOW_LOGINNAME != loginname)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisVaccination_BanKhongPhaiLaNguoiTheoDoi);
                    return false;
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
