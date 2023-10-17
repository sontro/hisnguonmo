using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AttachFileViewer.AttachFileViewer;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.AttachFileViewer
{
    class AttachFileViewerBehavior : BusinessBase, IAttachFileViewer
    {
        object[] entity;
        internal AttachFileViewerBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAttachFileViewer.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                AttachFileADO attachFileADO = null;

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
                            else if (entity[i] is AttachFileADO)
                            {
                                attachFileADO = (AttachFileADO)entity[i];
                            }
                        }
                    }
                }

                return new frmAttachFileViewer(moduleData, attachFileADO);
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
