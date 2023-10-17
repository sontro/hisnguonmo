using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.Config;
using MOS.TDO;

namespace MOS.MANAGER.HisPatientType
{
    partial class HisPatientTypeGet : BusinessBase
    {
        /// <summary>
        /// Phuc vu third-party
        /// </summary>
        /// <returns></returns>
        internal List<HisPatientTypeTDO> GetTdo()
        {
            try
            {
                List<HIS_PATIENT_TYPE> list = this.Get(new HisPatientTypeFilterQuery());
                if (IsNotNullOrEmpty(list))
                {
                    return list.Select(o => new HisPatientTypeTDO
                    {
                        PatientTypeCode = o.PATIENT_TYPE_CODE,
                        PatientTypeName = o.PATIENT_TYPE_NAME
                    }).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
