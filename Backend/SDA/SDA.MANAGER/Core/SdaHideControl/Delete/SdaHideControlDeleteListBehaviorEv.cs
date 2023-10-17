using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaHideControl.Delete
{
    class SdaHideControlDeleteListBehaviorEv : BeanObjectBase, ISdaHideControlDelete
    {
        List<SDA_HIDE_CONTROL> processDatas;
        List<long> entity;

        internal SdaHideControlDeleteListBehaviorEv(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.TruncateList(processDatas);
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
                processDatas = new List<SDA_HIDE_CONTROL>();
                foreach (var item in entity)
                {
                    bool valid = true;
                    SDA_HIDE_CONTROL raw = new SDA_HIDE_CONTROL();
                    valid = valid && SdaHideControlCheckVerifyIsUnlock.Verify(param, item, ref raw);
                    if (valid)
                        processDatas.Add(raw);
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
