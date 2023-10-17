using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBranchTime
{
    partial class HisBranchTimeCreate : EntityBase
    {
        public HisBranchTimeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BRANCH_TIME>();
        }

        private BridgeDAO<HIS_BRANCH_TIME> bridgeDAO;

        public bool Create(HIS_BRANCH_TIME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BRANCH_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
