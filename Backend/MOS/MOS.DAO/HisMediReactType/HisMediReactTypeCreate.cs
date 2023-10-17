using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediReactType
{
    partial class HisMediReactTypeCreate : EntityBase
    {
        public HisMediReactTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_REACT_TYPE>();
        }

        private BridgeDAO<HIS_MEDI_REACT_TYPE> bridgeDAO;

        public bool Create(HIS_MEDI_REACT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_REACT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
