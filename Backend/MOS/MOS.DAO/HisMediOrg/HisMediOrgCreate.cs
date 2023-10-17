using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediOrg
{
    partial class HisMediOrgCreate : EntityBase
    {
        public HisMediOrgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_ORG>();
        }

        private BridgeDAO<HIS_MEDI_ORG> bridgeDAO;

        public bool Create(HIS_MEDI_ORG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_ORG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
