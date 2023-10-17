using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBranch
{
    partial class HisBranchCreate : EntityBase
    {
        public HisBranchCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH>();
        }

        private BridgeDAO<HIS_BRANCH> bridgeDAO;

        public bool Create(HIS_BRANCH data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BRANCH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
