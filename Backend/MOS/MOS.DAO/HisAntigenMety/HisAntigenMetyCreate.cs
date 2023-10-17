using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigenMety
{
    partial class HisAntigenMetyCreate : EntityBase
    {
        public HisAntigenMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN_METY>();
        }

        private BridgeDAO<HIS_ANTIGEN_METY> bridgeDAO;

        public bool Create(HIS_ANTIGEN_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIGEN_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
