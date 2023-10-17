using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.MANAGER.Core.SarFormData.Update
{
    class SarFormDataUpdateListBehaviorEv : BeanObjectBase, ISarFormDataUpdate
    {
        List<SAR_FORM_DATA> entity;

        internal SarFormDataUpdateListBehaviorEv(CommonParam param, List<SAR_FORM_DATA> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDataUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDataDAO.UpdateList(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                if (param.Messages != null && param.Messages.Count > 0)
                {
                    param.Messages = param.Messages.Distinct().ToList();
                }
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                foreach (var data in entity)
                {
                    result = result && SarFormDataCheckVerifyValidData.Verify(param, data);
                    result = result && SarFormDataCheckVerifyIsUnlock.Verify(param, data.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                if (param.Messages != null && param.Messages.Count > 0)
                {
                    param.Messages = param.Messages.Distinct().ToList();
                }
                result = false;
            }
            return result;
        }
    }
}
