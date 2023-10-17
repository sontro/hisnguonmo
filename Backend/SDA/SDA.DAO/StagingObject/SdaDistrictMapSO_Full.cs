using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaDistrictMapSO : StagingObjectBase
    {
        public SdaDistrictMapSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_DISTRICT_MAP, bool>>> listSdaDistrictMapExpression = new List<System.Linq.Expressions.Expression<Func<SDA_DISTRICT_MAP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_DISTRICT_MAP, bool>>> listVSdaDistrictMapExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_DISTRICT_MAP, bool>>>();
    }
}
