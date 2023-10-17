using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCustomizeButton
{
    partial class SdaCustomizeButtonGet : EntityBase
    {
        public Dictionary<string, SDA_CUSTOMIZE_BUTTON> GetDicByCode(SdaCustomizeButtonSO search, CommonParam param)
        {
            Dictionary<string, SDA_CUSTOMIZE_BUTTON> dic = new Dictionary<string, SDA_CUSTOMIZE_BUTTON>();
            try
            {
                List<SDA_CUSTOMIZE_BUTTON> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BUTTON_PATH))
                        {
                            dic.Add(item.BUTTON_PATH, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                dic.Clear();
            }
            return dic;
        }
    }
}
