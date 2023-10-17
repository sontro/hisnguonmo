using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.XMLViewer130.XMLViewer130
{
    class XMLViewer130BehaviorFactory
    {
        internal static IXMLViewer130 MakeITransactionDepositCancel(object[] data)
        {
            IXMLViewer130 result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            string FilePathDefault = null;
            MemoryStream mmStream = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is string)
                            {
                                FilePathDefault = (string)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is MemoryStream)
                            {
                                mmStream = (MemoryStream)data[i];
                            }
                        }

                        if (moduleData != null && FilePathDefault != null)
                        {
                            result = new XMLViewer130Behavior(moduleData, FilePathDefault);
                        }
                        else if (mmStream != null && moduleData != null)
                        {
                            result = new XMLViewer130Behavior(moduleData, mmStream);
                        }
                        else if (moduleData != null)
                        {
                            result = new XMLViewer130Behavior(moduleData);
                        }
                        else
                        {
                            result = new XMLViewer130Behavior();
                        }
                    }
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
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
