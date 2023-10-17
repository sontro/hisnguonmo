using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaHideControl.Create
{
    class SdaHideControlCreateListBehaviorEv : BeanObjectBase, ISdaHideControlCreate
    {
        List<SDA_HIDE_CONTROL> entity;

        internal SdaHideControlCreateListBehaviorEv(CommonParam param, List<SDA_HIDE_CONTROL> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.CreateList(entity);
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
