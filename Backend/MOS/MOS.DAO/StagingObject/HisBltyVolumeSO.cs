using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisBltyVolumeSO : StagingObjectBase
    {
        public HisBltyVolumeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_BLTY_VOLUME, bool>>> listHisBltyVolumeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLTY_VOLUME, bool>>>();
    }
}
