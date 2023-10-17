using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class TypeCollection
    {
        internal static readonly List<Type> SarPrintLog = new List<Type>() { typeof(SAR_PRINT_LOG), typeof(List<SAR_PRINT_LOG>), typeof(SDO.SarPrintLogSDO), typeof(List<SDO.SarPrintLogSDO>) };
    }
}
