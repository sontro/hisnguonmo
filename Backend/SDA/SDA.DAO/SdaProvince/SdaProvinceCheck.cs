using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaProvince
{
    partial class SdaProvinceCheck : EntityBase
    {
        public SdaProvinceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE>();
        }

        private BridgeDAO<SDA_PROVINCE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
