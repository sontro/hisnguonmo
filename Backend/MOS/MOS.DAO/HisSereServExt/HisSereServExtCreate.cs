using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServExt
{
    partial class HisSereServExtCreate : EntityBase
    {
        public HisSereServExtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_EXT>();
        }

        private BridgeDAO<HIS_SERE_SERV_EXT> bridgeDAO;

        public bool Create(HIS_SERE_SERV_EXT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_EXT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
