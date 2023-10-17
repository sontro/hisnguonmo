using Inventec.Common.Logging;
using MOS.MANAGER.HisTreatment.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Xml
{
    public class XmlSwitch
    {
        public static void Run()
        {
            try
            {
                HisTreatmentExportXML4210.Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
