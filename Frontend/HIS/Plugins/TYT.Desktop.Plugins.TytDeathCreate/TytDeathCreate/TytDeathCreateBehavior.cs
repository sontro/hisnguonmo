using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using TYT.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.TytDeathCreate
{
    class TytDeathCreateBehavior : BusinessBase, ITytDeathCreate
    {
        object[] entity;
        internal TytDeathCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITytDeathCreate.Run()
        {
            object rs = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_PATIENT patient = null;
                TYT_DEATH tytHiv = null;

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
                            if (entity[i] is V_HIS_PATIENT)
                            {
                                patient = (V_HIS_PATIENT)entity[i];
                            }
                            if (entity[i] is TYT_DEATH)
                            {
                                tytHiv = (TYT_DEATH)entity[i];
                            }
                        }
                    }
                }

                if (moduleData != null)
                {
                    if (patient != null)
                    {
                        rs = new frmTytDeathCreate(moduleData, patient);
                    }
                    else if (tytHiv != null)
                    {
                        rs = new frmTytDeathCreate(moduleData, tytHiv);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return rs;
        }
    }
}
