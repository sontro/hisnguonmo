using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreDhty
{
    partial class HisHoreDhtyCreate : EntityBase
    {
        public HisHoreDhtyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_DHTY>();
        }

        private BridgeDAO<HIS_HORE_DHTY> bridgeDAO;

        public bool Create(HIS_HORE_DHTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HORE_DHTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
