using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaProvinceMap
{
    partial class SdaProvinceMapCheck : EntityBase
    {
        public SdaProvinceMapCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE_MAP>();
        }

        private BridgeDAO<SDA_PROVINCE_MAP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
