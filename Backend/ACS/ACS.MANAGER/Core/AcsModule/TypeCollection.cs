using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsModule = new List<Type>() { typeof(ACS_MODULE), typeof(List<ACS_MODULE>) };
    }
}
