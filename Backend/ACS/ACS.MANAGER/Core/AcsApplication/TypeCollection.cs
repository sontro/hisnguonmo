using ACS.EFMODEL.DataModels;
using ACS.SDO;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsApplication = new List<Type>() { typeof(ACS_APPLICATION), typeof(AcsApplicationWithDataSDO), typeof(List<ACS_APPLICATION>) };
    }
}
