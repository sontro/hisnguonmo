using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFilmSize
{
    public partial class HisFilmSizeDAO : EntityBase
    {
        public List<V_HIS_FILM_SIZE> GetView(HisFilmSizeSO search, CommonParam param)
        {
            List<V_HIS_FILM_SIZE> result = new List<V_HIS_FILM_SIZE>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public HIS_FILM_SIZE GetByCode(string code, HisFilmSizeSO search)
        {
            HIS_FILM_SIZE result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_HIS_FILM_SIZE GetViewById(long id, HisFilmSizeSO search)
        {
            V_HIS_FILM_SIZE result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_FILM_SIZE GetViewByCode(string code, HisFilmSizeSO search)
        {
            V_HIS_FILM_SIZE result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_FILM_SIZE> GetDicByCode(HisFilmSizeSO search, CommonParam param)
        {
            Dictionary<string, HIS_FILM_SIZE> result = new Dictionary<string, HIS_FILM_SIZE>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
