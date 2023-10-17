using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarRetyFofi.Create
{
    class SarRetyFofiCreateBehaviorListEv : BeanObjectBase, ISarRetyFofiCreate
    {
        List<SAR_RETY_FOFI> entities;

        internal SarRetyFofiCreateBehaviorListEv(CommonParam param, List<SAR_RETY_FOFI> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISarRetyFofiCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarRetyFofiDAO.CreateList(entities);
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
                result = result && SarRetyFofiCheckVerifyValidData.Verify(param, entities);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
