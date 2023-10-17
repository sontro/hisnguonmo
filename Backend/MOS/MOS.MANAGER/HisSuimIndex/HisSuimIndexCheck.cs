using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServSuin;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisSuimSetySuin;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndex
{
    class HisSuimIndexCheck : BusinessBase
    {
        internal HisSuimIndexCheck()
            : base()
        {

        }

        internal HisSuimIndexCheck(Inventec.Core.CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_SUIM_INDEX data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.SUIM_INDEX_CODE)) throw new ArgumentNullException("data.SUIM_INDEX_CODE");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisSuimIndexDAO.ExistsCode(code, id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(HIS_SUIM_INDEX data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisSuimIndexDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_SUIN> hisSereServSuins = new HisSereServSuinGet().GetBySuimIndexId(id);
                if (IsNotNullOrEmpty(hisSereServSuins))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServSuin_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SERE_SERV_SUIN, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_SUIM_SETY_SUIN> hisSuimSetySuins = new HisSuimSetySuinGet().GetBySuimIndexId(id);
                if (IsNotNullOrEmpty(hisSuimSetySuins))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSuimSetySuin_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_SUIM_SETY_SUIN, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
