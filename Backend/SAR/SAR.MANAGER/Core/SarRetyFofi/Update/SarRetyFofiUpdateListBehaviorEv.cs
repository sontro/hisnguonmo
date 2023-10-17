using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarRetyFofi.Update
{
    class SarRetyFofiUpdateListBehaviorEv : BeanObjectBase, ISarRetyFofiUpdate
    {
        List<SAR_RETY_FOFI> entity;

        internal SarRetyFofiUpdateListBehaviorEv(CommonParam param, List<SAR_RETY_FOFI> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarRetyFofiUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarRetyFofiDAO.UpdateList(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                foreach (var item in entity)
                {
                    result = result && SarRetyFofiCheckVerifyValidData.Verify(param, item);
                    result = result && SarRetyFofiCheckVerifyIsUnlock.Verify(param, item.ID);
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
    }
}
