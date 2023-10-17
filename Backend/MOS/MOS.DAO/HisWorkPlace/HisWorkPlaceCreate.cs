using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisWorkPlace
{
    partial class HisWorkPlaceCreate : EntityBase
    {
        public HisWorkPlaceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_WORK_PLACE>();
        }

        private BridgeDAO<HIS_WORK_PLACE> bridgeDAO;

        public bool Create(HIS_WORK_PLACE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_WORK_PLACE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
