using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSampleRoom
{
    partial class HisSampleRoomCreate : EntityBase
    {
        public HisSampleRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SAMPLE_ROOM>();
        }

        private BridgeDAO<HIS_SAMPLE_ROOM> bridgeDAO;

        public bool Create(HIS_SAMPLE_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SAMPLE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
