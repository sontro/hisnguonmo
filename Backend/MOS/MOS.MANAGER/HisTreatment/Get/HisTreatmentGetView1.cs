using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.Filter;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using AutoMapper;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisHeinApproval;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        internal List<V_HIS_TREATMENT_1> GetView1(HisTreatmentView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_1 GetView1ById(long id)
        {
            try
            {
                return GetView1ById(id, new HisTreatmentView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_1 GetView1ById(long id, HisTreatmentView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_TREATMENT_1> GetByImportView1(HisTreatmentView1ImportFilter filter)
        {
            List<V_HIS_TREATMENT_1> result = null;
            try
            {
                //return DAOWorker.HisTreatmentDAO.GetView1(filter, param);
                if (filter != null && filter.TreatmentImportFilters != null && filter.TreatmentImportFilters.Count > 0)
                {
                    List<V_HIS_TREATMENT_1> ListTreatment = new List<V_HIS_TREATMENT_1>();

                    foreach (var itemFilter in filter.TreatmentImportFilters)
                    {
                        if (itemFilter.IN_TIME.HasValue || itemFilter.OUT_TIME.HasValue || !String.IsNullOrWhiteSpace(itemFilter.TDL_HEIN_CARD_NUMBER)
                            || !String.IsNullOrWhiteSpace(itemFilter.TDL_PATIENT_CODE) || !String.IsNullOrWhiteSpace(itemFilter.TDL_PATIENT_NAME)
                            || !string.IsNullOrWhiteSpace(itemFilter.TREATMENT_CODE))
                        {
                            HisTreatmentView1FilterQuery filterQuery = new HisTreatmentView1FilterQuery();
                            if (itemFilter.IN_TIME.HasValue)
                            {
                                filterQuery.IN_TIME_FROM = Convert.ToInt64(itemFilter.IN_TIME.Value.ToString().Substring(0, 8) + "000000");
                                filterQuery.IN_TIME_TO = Convert.ToInt64(itemFilter.IN_TIME.Value.ToString().Substring(0, 8) + "235959");
                            }

                            if (itemFilter.OUT_TIME.HasValue)
                            {
                                filterQuery.OUT_TIME_FROM = Convert.ToInt64(itemFilter.OUT_TIME.Value.ToString().Substring(0, 8) + "000000");
                                filterQuery.OUT_TIME_TO = Convert.ToInt64(itemFilter.OUT_TIME.Value.ToString().Substring(0, 8) + "235959");
                            }

                            filterQuery.TDL_HEIN_CARD_NUMBER__EXACT = itemFilter.TDL_HEIN_CARD_NUMBER;
                            filterQuery.TREATMENT_CODE__EXACT = itemFilter.TREATMENT_CODE;
                            filterQuery.TDL_PATIENT_CODE__EXACT = itemFilter.TDL_PATIENT_CODE;
                            filterQuery.TDL_PATIENT_NAME = itemFilter.TDL_PATIENT_NAME;
                            filterQuery.ICD_CODEs = itemFilter.ICD_CODEs;
                            var treatments = GetView1(filterQuery);
                            if (treatments != null && treatments.Count > 0)
                            {
                                ListTreatment.AddRange(treatments);
                            }
                        }
                    }

                    if (ListTreatment != null && ListTreatment.Count > 0)
                    {
                        ListTreatment = ListTreatment.GroupBy(o => o.ID).OrderByDescending(o => o.First().MODIFY_TIME).Select(s => s.First()).ToList();

                        if (!string.IsNullOrWhiteSpace(filter.ORDER_FIELD) && !string.IsNullOrWhiteSpace(filter.ORDER_DIRECTION))
                        {
                            if (!param.Start.HasValue || !param.Limit.HasValue)
                            {
                                if (filter.ORDER_DIRECTION == "ACS")
                                {
                                    ListTreatment = ListTreatment.OrderBy(s => s.GetType().GetProperty(filter.ORDER_FIELD).GetValue(s, null)).ToList();
                                }
                                else
                                {
                                    ListTreatment = ListTreatment.OrderByDescending(s => s.GetType().GetProperty(filter.ORDER_FIELD).GetValue(s, null)).ToList();
                                }
                            }
                            else
                            {
                                param.Count = ListTreatment.Count();

                                if (filter.ORDER_DIRECTION == "ACS")
                                {
                                    ListTreatment = ListTreatment.OrderBy(s => s.GetType().GetProperty(filter.ORDER_FIELD).GetValue(s, null)).ToList();
                                }
                                else
                                {
                                    ListTreatment = ListTreatment.OrderByDescending(s => s.GetType().GetProperty(filter.ORDER_FIELD).GetValue(s, null)).ToList();
                                }

                                if (param.Count <= param.Limit.Value && param.Start.Value == 0)
                                {
                                    ListTreatment = ListTreatment.ToList();
                                }
                                else
                                {
                                    ListTreatment = ListTreatment.Skip(param.Start.Value).Take(param.Limit.Value).ToList();
                                }
                            }
                        }
                    }

                    result = ListTreatment;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }
    }
}
