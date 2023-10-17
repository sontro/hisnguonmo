using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediReact
{
    partial class HisMediReactCreate : EntityBase
    {
        public HisMediReactCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT>();
        }

        private BridgeDAO<HIS_MEDI_REACT> bridgeDAO;

        public bool Create(HIS_MEDI_REACT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_REACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
