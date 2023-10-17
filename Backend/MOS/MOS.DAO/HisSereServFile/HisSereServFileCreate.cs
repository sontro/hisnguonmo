using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServFile
{
    partial class HisSereServFileCreate : EntityBase
    {
        public HisSereServFileCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_FILE>();
        }

        private BridgeDAO<HIS_SERE_SERV_FILE> bridgeDAO;

        public bool Create(HIS_SERE_SERV_FILE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_FILE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
