using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactPlace
{
    public partial class HisVaccReactPlaceDAO : EntityBase
    {
        public List<V_HIS_VACC_REACT_PLACE> GetView(HisVaccReactPlaceSO search, CommonParam param)
        {
            List<V_HIS_VACC_REACT_PLACE> result = new List<V_HIS_VACC_REACT_PLACE>();

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

        public HIS_VACC_REACT_PLACE GetByCode(string code, HisVaccReactPlaceSO search)
        {
            HIS_VACC_REACT_PLACE result = null;

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
        
        public V_HIS_VACC_REACT_PLACE GetViewById(long id, HisVaccReactPlaceSO search)
        {
            V_HIS_VACC_REACT_PLACE result = null;

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

        public V_HIS_VACC_REACT_PLACE GetViewByCode(string code, HisVaccReactPlaceSO search)
        {
            V_HIS_VACC_REACT_PLACE result = null;

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

        public Dictionary<string, HIS_VACC_REACT_PLACE> GetDicByCode(HisVaccReactPlaceSO search, CommonParam param)
        {
            Dictionary<string, HIS_VACC_REACT_PLACE> result = new Dictionary<string, HIS_VACC_REACT_PLACE>();
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
