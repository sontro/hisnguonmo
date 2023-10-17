using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HID.Desktop.Plugins.FamilyInformation.HID.Desktop.Plugins.FamilyInformation;

namespace HID.Desktop.Plugins.FamilyInformation
{
    class HisIcdBehavior : BusinessBase, IFamilyInformation
    {
        object[] entity;
        internal HisIcdBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IFamilyInformation.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                String personcode = "";

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
                            if (entity[i] is String)
                            {
                                personcode = (String)entity[i];
                            }
                        }
                    }
                }

                return new frmFamilyInformation(moduleData, personcode);
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
