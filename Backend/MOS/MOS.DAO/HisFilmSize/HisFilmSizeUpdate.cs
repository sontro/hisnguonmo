using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFilmSize
{
    partial class HisFilmSizeUpdate : EntityBase
    {
        public HisFilmSizeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILM_SIZE>();
        }

        private BridgeDAO<HIS_FILM_SIZE> bridgeDAO;

        public bool Update(HIS_FILM_SIZE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FILM_SIZE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
