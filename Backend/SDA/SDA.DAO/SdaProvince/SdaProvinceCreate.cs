using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaProvince
{
    partial class SdaProvinceCreate : EntityBase
    {
        public SdaProvinceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE>();
        }

        private BridgeDAO<SDA_PROVINCE> bridgeDAO;

        public bool Create(SDA_PROVINCE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_PROVINCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
