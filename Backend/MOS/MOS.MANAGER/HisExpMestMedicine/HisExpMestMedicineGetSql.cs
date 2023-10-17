using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine
{
    partial class HisExpMestMedicineGetSql : GetBase
    {
        internal HisExpMestMedicineGetSql()
            : base()
        {

        }

        internal HisExpMestMedicineGetSql(CommonParam paramGet)
            : base(paramGet)
        {

        }

        
    }
}
