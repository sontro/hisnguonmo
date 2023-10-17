using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterial
{
    partial class HisMaterialCreate : EntityBase
    {
        public HisMaterialCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL> bridgeDAO;

        public bool Create(HIS_MATERIAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
