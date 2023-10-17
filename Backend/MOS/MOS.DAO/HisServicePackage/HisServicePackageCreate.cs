using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServicePackage
{
    partial class HisServicePackageCreate : EntityBase
    {
        public HisServicePackageCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PACKAGE>();
        }

        private BridgeDAO<HIS_SERVICE_PACKAGE> bridgeDAO;

        public bool Create(HIS_SERVICE_PACKAGE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_PACKAGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
