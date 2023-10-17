using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaHideControl.Update
{
    class SdaHideControlUpdateListBehaviorEv : BeanObjectBase, ISdaHideControlUpdate
    {
        List<SDA_HIDE_CONTROL> current;
        List<SDA_HIDE_CONTROL> entity;

        internal SdaHideControlUpdateListBehaviorEv(CommonParam param, List<SDA_HIDE_CONTROL> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.UpdateList(entity);
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
                result = result && SdaHideControlCheckVerifyValidData.Verify(param, entity);
                result = result && SdaHideControlCheckVerifyIsUnlock.Verify(param, entity, ref current);
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
