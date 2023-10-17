using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTrouble.CreateByMessege
{
    class SdaTroubleCreateByMessegeBehavior : BeanObjectBase, ISdaTroubleCreateByMessege
    {
        List<string> entities;

        internal SdaTroubleCreateByMessegeBehavior(CommonParam param, List<string> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaTroubleCreateByMessege.Run()
        {
            bool result = false;
            try
            {
                //result = Check() && DAOWorker.SdaTroubleDAO.CreateByMessegeList(entities);
                if (entities != null && entities.Count > 0)
                {
                    List<SDA_TROUBLE> datas = new List<SDA_TROUBLE>();
                    foreach (var message in entities)
                    {
                        if (!String.IsNullOrEmpty(message))
                        {
                            SDA_TROUBLE data = new SDA_TROUBLE();
                            data.MESSAGE = message;
                            datas.Add(data);
                        }
                    }
                    result = DAOWorker.SdaTroubleDAO.CreateList(datas);
                }
                else
                {
                    result = true;
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
