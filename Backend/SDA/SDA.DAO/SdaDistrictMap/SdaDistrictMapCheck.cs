using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaDistrictMap
{
    partial class SdaDistrictMapCheck : EntityBase
    {
        public SdaDistrictMapCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT_MAP>();
        }

        private BridgeDAO<SDA_DISTRICT_MAP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
