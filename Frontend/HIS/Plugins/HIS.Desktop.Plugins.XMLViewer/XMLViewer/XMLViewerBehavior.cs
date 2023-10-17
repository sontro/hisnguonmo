using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.XMLViewer.XMLViewer
{
    class XMLViewerBehavior : Tool<IDesktopToolContext>, IXMLViewer
    {
        string url;
        MemoryStream mmStream;
        Inventec.Desktop.Common.Modules.Module Module;
        internal XMLViewerBehavior()
            : base()
        {

        }

        internal XMLViewerBehavior(Inventec.Desktop.Common.Modules.Module module, string data)
            : base()
        {
            this.Module = module;
            this.url = data;
        }
        internal XMLViewerBehavior(Inventec.Desktop.Common.Modules.Module module, MemoryStream _mmStream)
            : base()
        {
            this.Module = module;
            this.mmStream = _mmStream;
        }

        internal XMLViewerBehavior(Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this.Module = module;
        }

        object IXMLViewer.Run()
        {
            object result = null;
            try
            {
                if (this.Module != null && !string.IsNullOrEmpty(url))
                {
                    result = new frmXMLViewer(Module, url);
                    if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Module), Module));
                }
                else if (this.Module != null && mmStream != null)
                {
                    result = new frmXMLViewer(Module, mmStream);
                    if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Module), Module));
                }
                else
                {
                    result = new frmXMLViewer();
                    Inventec.Common.Logging.LogSystem.Error("Module is null****");
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
