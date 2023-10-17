using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPackageDetail
{
    partial class HisPackageDetailCreate : EntityBase
    {
        public HisPackageDetailCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE_DETAIL>();
        }

        private BridgeDAO<HIS_PACKAGE_DETAIL> bridgeDAO;

        public bool Create(HIS_PACKAGE_DETAIL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PACKAGE_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
