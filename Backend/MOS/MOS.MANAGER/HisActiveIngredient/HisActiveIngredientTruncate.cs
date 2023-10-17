using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisActiveIngredient
{
    class HisActiveIngredientTruncate : BusinessBase
    {
        internal HisActiveIngredientTruncate()
            : base()
        {

        }

        internal HisActiveIngredientTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ACTIVE_INGREDIENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisActiveIngredientCheck checker = new HisActiveIngredientCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.CheckConstraint(data);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisActiveIngredientDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDangDuocSuDungKhongChoPhepXoa);
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ACTIVE_INGREDIENT data = null;
                HisActiveIngredientCheck checker = new HisActiveIngredientCheck(param);
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.VerifyId(id, ref data);
                valid = valid && checker.CheckConstraint(data);
                valid = valid && checker.IsUnLock(data);
                if (valid)
                {
                    result = DAOWorker.HisActiveIngredientDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDangDuocSuDungKhongChoPhepXoa);
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateAll()
        {
            bool result = false;
            try
            {
                if (HisEmployeeUtil.CheckAdmin(param))
                {
                    List<string> sqls = new List<string>()
                    {
                        "DELETE FROM HIS_ACIN_INTERACTIVE",
                        "DELETE FROM HIS_ICD_SERVICE WHERE ACTIVE_INGREDIENT_ID IS NOT NULL",
                        "DELETE FROM HIS_MEDICINE_TYPE_ACIN",
                        "DELETE FROM HIS_ACTIVE_INGREDIENT"
                    };
                    result = DAOWorker.SqlDAO.Execute(sqls);
                }
            }
            catch (Exception ex)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDangDuocSuDungKhongChoPhepXoa);
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
