using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaProvinceMap
{
    partial class SdaProvinceMapCreate : EntityBase
    {
        public SdaProvinceMapCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE_MAP>();
        }

        private BridgeDAO<SDA_PROVINCE_MAP> bridgeDAO;

        public bool Create(SDA_PROVINCE_MAP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_PROVINCE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
