using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPackage
{
    partial class HisPackageCreate : EntityBase
    {
        public HisPackageCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE>();
        }

        private BridgeDAO<HIS_PACKAGE> bridgeDAO;

        public bool Create(HIS_PACKAGE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PACKAGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
