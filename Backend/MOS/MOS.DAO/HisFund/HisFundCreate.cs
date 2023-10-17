using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFund
{
    partial class HisFundCreate : EntityBase
    {
        public HisFundCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUND>();
        }

        private BridgeDAO<HIS_FUND> bridgeDAO;

        public bool Create(HIS_FUND data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FUND> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
