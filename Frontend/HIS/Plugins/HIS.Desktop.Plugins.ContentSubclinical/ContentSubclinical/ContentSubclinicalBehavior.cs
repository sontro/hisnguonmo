using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ContentSubclinical;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ContentSubclinical.ContentSubclinical
{
    public sealed class ContentSubclinicalBehavior : Tool<IDesktopToolContext>, IContentSubclinical
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId = 0;
        bool returnObject = false;
        public ContentSubclinicalBehavior()
            : base()
        {
        }

        public ContentSubclinicalBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IContentSubclinical.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    HIS.Desktop.Common.DelegateSelectData _DelegateSelectData = null;

                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        else if (item is HIS.Desktop.Common.DelegateSelectData)
                        {
                            _DelegateSelectData = (HIS.Desktop.Common.DelegateSelectData)item;
                        }
                        else if (item is bool)
                        {
                            returnObject = (bool)item;
                        }

                    }
                    if (currentModule != null && treatmentId > 0 && _DelegateSelectData != null)
                    {
                        result = new frmContentSubclinical(currentModule, treatmentId, _DelegateSelectData, returnObject);
                    }

                }
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
