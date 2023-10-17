using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    internal class SereServTempProcess
    {
        static List<HIS_SERE_SERV_TEMP> listTemplate;

        internal static HIS_SERE_SERV_TEMP GetDescription(HIS_SERE_SERV_TEMP data)
        {
            HIS_SERE_SERV_TEMP resutl = null;
            if (listTemplate != null && listTemplate.Count > 0)
            {
                resutl = listTemplate.FirstOrDefault(o => o.ID == data.ID);
            }
            return resutl;
        }

        internal static void SetDescription(HIS_SERE_SERV_TEMP data)
        {
            try
            {
                if (data != null)
                {
                    if (listTemplate == null)
                    {
                        listTemplate = new List<HIS_SERE_SERV_TEMP>();
                    }

                    var oldData = listTemplate.FirstOrDefault(o => o.ID == data.ID);
                    if (oldData != null && oldData.MODIFY_TIME != data.MODIFY_TIME)
                    {
                        oldData.DESCRIPTION = data.DESCRIPTION;
                        oldData.MODIFY_TIME = data.MODIFY_TIME;
                    }
                    else if (oldData == null)
                    {
                        listTemplate.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
