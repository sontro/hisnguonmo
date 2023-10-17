using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaProvinceMapSO : StagingObjectBase
    {
        public SdaProvinceMapSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_PROVINCE_MAP, bool>>> listSdaProvinceMapExpression = new List<System.Linq.Expressions.Expression<Func<SDA_PROVINCE_MAP, bool>>>();
    }
}
