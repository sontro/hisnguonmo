using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisActiveIngredient
{
    class HisActiveIngredientUpdate : BusinessBase
    {
        internal HisActiveIngredientUpdate()
            : base()
        {

        }

        internal HisActiveIngredientUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACTIVE_INGREDIENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisActiveIngredientCheck checker = new HisActiveIngredientCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisActiveIngredientDAO.Update(data);
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

        internal bool UpdateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisActiveIngredientCheck checker = new HisActiveIngredientCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisActiveIngredientDAO.UpdateList(listData);
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
