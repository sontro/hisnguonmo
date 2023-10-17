using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAdr
{
    partial class HisAdrCreate : EntityBase
    {
        public HisAdrCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR>();
        }

        private BridgeDAO<HIS_ADR> bridgeDAO;

        public bool Create(HIS_ADR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ADR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
