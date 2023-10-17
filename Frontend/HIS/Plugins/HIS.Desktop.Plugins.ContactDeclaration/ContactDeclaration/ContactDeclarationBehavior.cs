using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContactDeclaration.ContactDeclaration
{
    class ContactDeclarationBehavior : BusinessBase, IContactDeclaration
    {
        object[] entity;
        internal ContactDeclarationBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IContactDeclaration.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_CONTACT_POINT ContactPoint = null; 

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

                            if (entity[i] is V_HIS_CONTACT_POINT)
                            {
                                ContactPoint = (V_HIS_CONTACT_POINT)entity[i];
                            }
                        }
                    }
                }
                if (ContactPoint != null)
                {
                    return new frmContactDeclaration(moduleData, ContactPoint);
                }
                else
                {
                    return new frmContactDeclaration(moduleData);
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
