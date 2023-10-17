using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class TypeCollection
    {
        internal static readonly List<Type> SdaSql = new List<Type>() { typeof(SDA_SQL), typeof(List<SDA_SQL>) };
    }
}
