using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigen
{
    partial class HisAntigenCreate : EntityBase
    {
        public HisAntigenCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN>();
        }

        private BridgeDAO<HIS_ANTIGEN> bridgeDAO;

        public bool Create(HIS_ANTIGEN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIGEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
