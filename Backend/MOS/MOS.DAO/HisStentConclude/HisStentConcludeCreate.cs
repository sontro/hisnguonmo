using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeCreate : EntityBase
    {
        public HisStentConcludeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STENT_CONCLUDE>();
        }

        private BridgeDAO<HIS_STENT_CONCLUDE> bridgeDAO;

        public bool Create(HIS_STENT_CONCLUDE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_STENT_CONCLUDE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
