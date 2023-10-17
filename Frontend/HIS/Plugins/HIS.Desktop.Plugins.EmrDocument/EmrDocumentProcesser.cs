using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.EmrDocument;


namespace HIS.Desktop.Plugins.EmrDocument
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "HIS.Desktop.Plugins.EmrDocument",
     "Danh mục",
     "Bussiness",
     4,
     "showproduct_32x32.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true)
  ]
    public class EmrDocumentProcessor : ModuleBase, IDesktopRoot
    {

        CommonParam param;
        public EmrDocumentProcessor()
        {
            param = new CommonParam();
        }
        public EmrDocumentProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData delegateSelect = null;
                long _treatmentId = 0;
                string TreatmentCode = "";
                List<string> treatmentCodes = null;
                bool isStore = false;
                RefeshReference delegateRefresh = null;
                if (args.GetType() == typeof(object[]))
                {
                    if (args != null && args.Count() > 0)
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (args[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)args[i];
                            }
                            else if (args[i] is long)
                            {
                                _treatmentId = (long)args[i];
                            }
                            else if (args[i] is string)
                            {
                                TreatmentCode = (string)args[i];
                            }
                            else if (args[i] is List<string>)
                            {
                                treatmentCodes = (List<string>)args[i];
                            }
                            else if (args[i] is bool)
                            {
                                isStore = (bool)args[i];
                            }
                            else if (args[i] is RefeshReference)                          
                            {
                                delegateRefresh = (RefeshReference)args[i]; 
                            }
                               

                        }
                    }
                }

                if (_treatmentId > 0)
                    result = new HIS.Desktop.Plugins.EmrDocument.EmrDocumentForm(moduleData, _treatmentId, delegateRefresh);
                else if (!String.IsNullOrWhiteSpace(TreatmentCode))
                {
                    result = new HIS.Desktop.Plugins.EmrDocument.EmrDocumentForm(moduleData, TreatmentCode, isStore, delegateRefresh);
                }
                else if (treatmentCodes != null && treatmentCodes.Count > 0)
                {
                    result = new HIS.Desktop.Plugins.EmrDocument.EmrDocumentForm(moduleData, treatmentCodes, isStore, delegateRefresh);
                }
                else
                    result = new HIS.Desktop.Plugins.EmrDocument.EmrDocumentForm(moduleData, delegateRefresh);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
