using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaCommuneMapSO : StagingObjectBase
    {
        public SdaCommuneMapSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_COMMUNE_MAP, bool>>> listSdaCommuneMapExpression = new List<System.Linq.Expressions.Expression<Func<SDA_COMMUNE_MAP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_COMMUNE_MAP, bool>>> listVSdaCommuneMapExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_COMMUNE_MAP, bool>>>();
    }
}
