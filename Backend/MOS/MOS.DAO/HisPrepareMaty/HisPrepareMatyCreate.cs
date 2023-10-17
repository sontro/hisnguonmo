using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepareMaty
{
    partial class HisPrepareMatyCreate : EntityBase
    {
        public HisPrepareMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_MATY>();
        }

        private BridgeDAO<HIS_PREPARE_MATY> bridgeDAO;

        public bool Create(HIS_PREPARE_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PREPARE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
