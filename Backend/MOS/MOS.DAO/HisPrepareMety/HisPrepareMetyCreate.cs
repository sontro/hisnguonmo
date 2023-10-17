using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepareMety
{
    partial class HisPrepareMetyCreate : EntityBase
    {
        public HisPrepareMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_METY>();
        }

        private BridgeDAO<HIS_PREPARE_METY> bridgeDAO;

        public bool Create(HIS_PREPARE_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PREPARE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
