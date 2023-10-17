using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.DAO.StagingObject
{
    public class SdaMetadataSO : StagingObjectBase
    {
        public SdaMetadataSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SDA_METADATA, bool>>> listSdaMetadataExpression = new List<System.Linq.Expressions.Expression<Func<SDA_METADATA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SDA_METADATA, bool>>> listVSdaMetadataExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_METADATA, bool>>>();
    }
}
