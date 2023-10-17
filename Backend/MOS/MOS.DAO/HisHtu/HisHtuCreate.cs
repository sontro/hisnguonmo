using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHtu
{
    partial class HisHtuCreate : EntityBase
    {
        public HisHtuCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HTU>();
        }

        private BridgeDAO<HIS_HTU> bridgeDAO;

        public bool Create(HIS_HTU data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HTU> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
