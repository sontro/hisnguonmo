using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.HisDebateTemp.HisDebateTemp
{
    class HisDebateTempBehaviory : BusinessBase, IHisDebateTemp
    {
         object[] entity;
         internal HisDebateTempBehaviory(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

         object IHisDebateTemp.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
               HIS_DEBATE_TEMP HisDebateTemp = null;
                HIS_DEBATE HisDebate = null ;
                     
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is HIS_DEBATE_TEMP)
                            {
                                HisDebateTemp = (HIS_DEBATE_TEMP)entity[i];
                            }
                            if (entity[i] is HIS_DEBATE)
                            {
                                HisDebate = (HIS_DEBATE)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && HisDebateTemp != null)
                {
                    return new frmHisDebateTemp(moduleData, HisDebateTemp);
                }
                else if (moduleData != null && HisDebate != null)
                {
                    return new frmHisDebateTemp(moduleData, HisDebate);
                }
                else
                {
                    return new frmHisDebateTemp(moduleData);
                }
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}

