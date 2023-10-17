using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaDistrict
{
    partial class SdaDistrictCheck : EntityBase
    {
        public SdaDistrictCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT>();
        }

        private BridgeDAO<SDA_DISTRICT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
