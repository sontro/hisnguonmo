using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactType
{
    partial class HisVaccReactTypeCreate : EntityBase
    {
        public HisVaccReactTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_TYPE>();
        }

        private BridgeDAO<HIS_VACC_REACT_TYPE> bridgeDAO;

        public bool Create(HIS_VACC_REACT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACC_REACT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
