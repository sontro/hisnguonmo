using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using  HIS.Desktop.Plugins.HisImportMedicineType.FormLoad;

namespace  HIS.Desktop.Plugins.HisImportMedicineType
{
    class ImportBehavior : BusinessBase, IImport
    {
        object[] entity;
        //DelegateRefreshData delegateRefresh = null;

        internal ImportBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImport.Run()
        {
            object frm = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                RefeshReference refeshReference = null;
                //List<ServiceImportADO> service = null;
                //List<ServiceImportADO> service = null;


                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is RefeshReference)
                            {
                                refeshReference = (RefeshReference)entity[i];
                            }
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                if (moduleData != null && refeshReference != null)
                {
                    frm = new frmMedicineType(moduleData, refeshReference);
                }
                else if (moduleData != null && refeshReference == null)
                {
                    frm = new frmMedicineType(moduleData);
                }
                return frm;
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
