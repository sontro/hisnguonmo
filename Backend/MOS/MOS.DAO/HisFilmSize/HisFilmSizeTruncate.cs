using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFilmSize
{
    partial class HisFilmSizeTruncate : EntityBase
    {
        public HisFilmSizeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILM_SIZE>();
        }

        private BridgeDAO<HIS_FILM_SIZE> bridgeDAO;

        public bool Truncate(HIS_FILM_SIZE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FILM_SIZE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
