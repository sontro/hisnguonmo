using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAcinInteractive;
using MOS.MANAGER.HisMedicineTypeAcin;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisActiveIngredient
{
    class HisActiveIngredientCheck : BusinessBase
    {
        internal HisActiveIngredientCheck()
            : base()
        {

        }

        internal HisActiveIngredientCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyId(long id, ref HIS_ACTIVE_INGREDIENT data)
        {
            bool valid = true;
            try
            {
                data = new HisActiveIngredientGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
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

        internal bool VerifyRequireField(HIS_ACTIVE_INGREDIENT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ACTIVE_INGREDIENT_CODE)) throw new ArgumentNullException("data.ACTIVE_INGREDIENT_CODE");
                if (!IsNotNullOrEmpty(data.ACTIVE_INGREDIENT_NAME)) throw new ArgumentNullException("data.ACTIVE_INGREDIENT_NAME");
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

        internal bool IsUnLock(HIS_ACTIVE_INGREDIENT data)
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
                if (!DAOWorker.HisActiveIngredientDAO.IsUnLock(id))
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

        internal bool CheckConstraint(HIS_ACTIVE_INGREDIENT data)
        {
            bool valid = true;
            try
            {
                List<HIS_ACIN_INTERACTIVE> acinInteractives = new HisAcinInteractiveGet().GetByActiveIngredientIdOrActiveIngredientConflictId(data.ID);
                if (IsNotNullOrEmpty(acinInteractives))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAcinInteractive_TonTaiDuLieu);
                    return false;
                }

                List<HIS_MEDICINE_TYPE_ACIN> medicineTypeAcins = new HisMedicineTypeAcinGet().GetByActiveIngredientId(data.ID);
                if (IsNotNullOrEmpty(medicineTypeAcins))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineTypeAcin_TonTaiDuLieu);
                    return false;
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
