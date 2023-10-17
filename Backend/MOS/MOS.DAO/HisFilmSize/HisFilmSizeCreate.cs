using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFilmSize
{
    partial class HisFilmSizeCreate : EntityBase
    {
        public HisFilmSizeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILM_SIZE>();
        }

        private BridgeDAO<HIS_FILM_SIZE> bridgeDAO;

        public bool Create(HIS_FILM_SIZE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FILM_SIZE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
