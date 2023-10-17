using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using HIS.Desktop.LibraryMessage;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIS.Desktop.Plugins.CallBriefPatient
{
    class CallBriefPatientFactory
    {
        internal static ICallBriefPatient MakeICallBriefPatient(CommonParam param, object[] data)
        {
            ICallBriefPatient result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            RefeshReference RefeshReference = null;
HisTreatmentLogSDO treatmentSDO= null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                         if (data[i] is RefeshReference)
                            {
                             RefeshReference = (RefeshReference)data[i];
                            }
                         else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                         {
                          moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                         }
                         else if (data[i] is HisTreatmentLogSDO)
                         {
                          treatmentSDO = (HisTreatmentLogSDO)data[i];
                         }
                        }

                        if (moduleData != null )
                        {
                         result = new CallBriefPatientBehavior(param, moduleData, RefeshReference, treatmentSDO);
                        }
                        else
                        {
                         MessageManager.Show(MessageUtil.GetMessage(LibraryMessage.Message.Enum.TaiKhoanKhongCoQuyenThucHienChucNang));
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
