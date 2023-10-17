using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBedBsty
{
    partial class HisBedBstyCreate : EntityBase
    {
        public HisBedBstyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_BSTY>();
        }

        private BridgeDAO<HIS_BED_BSTY> bridgeDAO;

        public bool Create(HIS_BED_BSTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BED_BSTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
