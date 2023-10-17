using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServ
{
    partial class HisSereServCreate : EntityBase
    {
        public HisSereServCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV>();
        }

        private BridgeDAO<HIS_SERE_SERV> bridgeDAO;

        public bool Create(HIS_SERE_SERV data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
