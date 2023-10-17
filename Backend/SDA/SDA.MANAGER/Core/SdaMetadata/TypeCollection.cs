using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaMetadata
{
    partial class TypeCollection
    {
        internal static readonly List<Type> SdaMetadata = new List<Type>() { typeof(SDA_METADATA), typeof(List<SDA_METADATA>) };
    }
}
