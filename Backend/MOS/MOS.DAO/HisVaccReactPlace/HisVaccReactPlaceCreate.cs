using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactPlace
{
    partial class HisVaccReactPlaceCreate : EntityBase
    {
        public HisVaccReactPlaceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_REACT_PLACE>();
        }

        private BridgeDAO<HIS_VACC_REACT_PLACE> bridgeDAO;

        public bool Create(HIS_VACC_REACT_PLACE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACC_REACT_PLACE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
