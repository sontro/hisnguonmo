using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDhst
{
    class HisDhstGetSql : GetBase
    {
        internal HisDhstGetSql()
            : base()
        {

        }

        internal HisDhstGetSql(CommonParam param)
            : base(param)
        {

        }
        internal List<HisDhstTDO> GetForEmr(HisDhstForEmrFilter filter)
        {
            List<HisDhstTDO> result = null;
            try
            {
                if (filter.TREATMENT_ID.HasValue || !String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    long treatmentId = 0;

                    if (!String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                    {
                        treatmentId = DAOWorker.SqlDAO.GetSqlSingle<long>("SELECT ID FROM HIS_TREATMENT WHERE TREATMENT_CODE = :param1", filter.TREATMENT_CODE__EXACT);
                    }
                    else
                    {
                        treatmentId = filter.TREATMENT_ID.Value;
                    }

                    if (treatmentId > 0)
                    {
                        result = new List<HisDhstTDO>();

                        List<HIS_DHST> lstDhst = DAOWorker.SqlDAO.GetSql<HIS_DHST>("SELECT * FROM HIS_DHST WHERE TREATMENT_ID = :param1 ORDER BY EXECUTE_TIME DESC", treatmentId);

                        if (IsNotNullOrEmpty(lstDhst))
                        {
                            foreach (HIS_DHST dhst in lstDhst)
                            {
                                HisDhstTDO tdo = new HisDhstTDO();
                                if (dhst.ID != null)
                                {
                                    tdo.DhstId = (int)dhst.ID;
                                }

                                tdo.ExecuteTime = dhst.EXECUTE_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(dhst.EXECUTE_TIME.Value) : null;

                                if (dhst.VIR_BMI.HasValue)
                                {
                                    tdo.BMI = (double)dhst.VIR_BMI;
                                }
                                if (dhst.WEIGHT.HasValue)
                                {
                                    tdo.CanNang = (double)dhst.WEIGHT;
                                }
                                if (dhst.HEIGHT.HasValue)
                                {
                                    tdo.ChieuCao = (double)dhst.HEIGHT;
                                }

                                if (dhst.BLOOD_PRESSURE_MAX.HasValue && dhst.BLOOD_PRESSURE_MIN.HasValue)
                                {
                                    tdo.HuyetAp = dhst.BLOOD_PRESSURE_MAX + "/" + dhst.BLOOD_PRESSURE_MIN;
                                }

                                if (dhst.PULSE.HasValue)
                                {
                                    tdo.Mach = (int)dhst.PULSE;
                                }
                                if (dhst.TEMPERATURE.HasValue)
                                {
                                    tdo.NhietDo = (double)dhst.TEMPERATURE;
                                }
                                if (dhst.BREATH_RATE.HasValue)
                                {
                                    tdo.NhipTho = (int)dhst.BREATH_RATE;
                                }

                                tdo.IsTracking = dhst.TRACKING_ID.HasValue;
                                tdo.IsCare = dhst.CARE_ID.HasValue;

                                if (tdo.Mach.HasValue 
                                    || tdo.NhietDo.HasValue 
                                    || !String.IsNullOrEmpty(tdo.HuyetAp) 
                                    || tdo.NhipTho.HasValue 
                                    || tdo.CanNang.HasValue 
                                    || tdo.ChieuCao.HasValue 
                                    || tdo.BMI.HasValue)
                                {
                                    result.Add(tdo);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

    }
}
