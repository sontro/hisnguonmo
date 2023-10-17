using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal List<HisServiceReqTDO> GetResultForEmr(HisServiceReqResultForEmrFilter filter)
        {
            List<HisServiceReqTDO> result = null;
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
                        result = new List<HisServiceReqTDO>();
                        HisServiceReqFilterQuery reqFilter = new HisServiceReqFilterQuery();
                        reqFilter.TREATMENT_ID = treatmentId;
                        reqFilter.SERVICE_REQ_STT_IDs = new List<long>();
                        reqFilter.SERVICE_REQ_STT_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT);
                        reqFilter.SERVICE_REQ_STT_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL);

                        if (filter.INSTRUCTION_DATE__FROM.HasValue)
                        {
                            reqFilter.INTRUCTION_TIME_FROM = filter.INSTRUCTION_DATE__FROM.Value;
                        }
                        if (filter.INSTRUCTION_DATE__TO.HasValue)
                        {
                            reqFilter.INTRUCTION_TIME_TO = filter.INSTRUCTION_DATE__TO.Value;
                        }

                        List<HIS_SERVICE_REQ> lstServiceReq = this.Get(reqFilter);

                        if (!IsNotNullOrEmpty(lstServiceReq))
                        {
                            return result;
                        }

                        HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                        ssFilter.SERVICE_REQ_IDs = lstServiceReq.Select(s => s.ID).ToList();
                        ssFilter.TDL_SERVICE_TYPE_IDs = new List<long>();
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA);
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS);
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA);
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN);
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN);
                        ssFilter.TDL_SERVICE_TYPE_IDs.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT);

                        List<HIS_SERE_SERV> lstSereServ = new HisSereServGet().Get(ssFilter);

                        List<HIS_SERE_SERV> lstTest = lstSereServ != null ? lstSereServ.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN).ToList() : null;

                        List<HIS_SERE_SERV_EXT> lstSereServExt = null;
                        List<V_HIS_SERE_SERV_TEIN> lstSereServTein = null;

                        if (!IsNotNullOrEmpty(lstSereServ))
                        {
                            return result;
                        }

                        HisSereServExtFilterQuery extFilter = new HisSereServExtFilterQuery();
                        extFilter.SERE_SERV_IDs = lstSereServ.Select(p => p.ID).Distinct().ToList();
                        lstSereServExt = new HisSereServExtGet().Get(extFilter);

                        if (IsNotNullOrEmpty(lstTest))
                        {
                            HisSereServTeinViewFilterQuery teinFilter = new HisSereServTeinViewFilterQuery();
                            teinFilter.SERE_SERV_IDs = lstTest.Select(p => p.ID).Distinct().ToList();
                            teinFilter.ORDER_FIELD = "NUM_ORDER";
                            teinFilter.ORDER_DIRECTION = "DESC";
                            teinFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;

                            lstSereServTein = new HisSereServTeinGet().GetView(teinFilter);
                        }

                        var Groups = lstSereServ.GroupBy(g => g.SERVICE_REQ_ID.Value).ToList();
                        foreach (var group in Groups)
                        {
                            HIS_SERVICE_REQ req = lstServiceReq.FirstOrDefault(o => o.ID == group.Key);
                            HisServiceReqTDO reqTdo = new HisServiceReqTDO();
                            reqTdo.IcdCode = req.ICD_CODE;
                            reqTdo.IcdName = req.ICD_NAME;
                            reqTdo.IcdSubCode = req.ICD_SUB_CODE;
                            reqTdo.IcdText = req.ICD_TEXT;
                            reqTdo.InstructionDate = req.INTRUCTION_DATE;
                            reqTdo.InstructionTime = req.INTRUCTION_TIME;
                            reqTdo.Note = req.NOTE;
                            reqTdo.ServiceReqCode = req.SERVICE_REQ_CODE;
                            reqTdo.ServiceReqTypeId = req.SERVICE_REQ_TYPE_ID;
                            reqTdo.TreatmentCode = req.TDL_TREATMENT_CODE;
                            result.Add(reqTdo);

                            List<HIS_SERE_SERV> sereServs = group.ToList();
                            reqTdo.Services = new List<HisSereServTDO>();
                            foreach (HIS_SERE_SERV item in sereServs)
                            {
                                HisSereServTDO serviceTdo = new HisSereServTDO();
                                serviceTdo.SereServId = item.ID;
                                serviceTdo.ServiceCode = item.TDL_SERVICE_CODE;
                                serviceTdo.ServiceName = item.TDL_SERVICE_NAME;
                                serviceTdo.ServiceReqCode = req.SERVICE_REQ_CODE;
                                serviceTdo.ServiceTypeId = item.TDL_SERVICE_TYPE_ID;
                                var serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == item.TDL_SERVICE_UNIT_ID);
                                serviceTdo.ServiceUnitName = serviceUnit != null ? serviceUnit.SERVICE_UNIT_NAME : null;
                                var type = HisServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == item.TDL_SERVICE_TYPE_ID);
                                serviceTdo.ServiceTypeName = type != null ? type.SERVICE_TYPE_NAME : null;

                                reqTdo.Services.Add(serviceTdo);

                                if (item.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN)
                                {
                                    HIS_SERE_SERV_EXT ext = lstSereServExt != null ? lstSereServExt.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                                    serviceTdo.Result = ext != null ? ext.CONCLUDE : null;
                                    serviceTdo.Description = ext != null ? ext.DESCRIPTION : null;
                                }
                                else
                                {
                                    if (req.EXE_SERVICE_MODULE_ID.HasValue
                                    && req.EXE_SERVICE_MODULE_ID.Value != IMSys.DbConfig.HIS_RS.HIS_EXE_SERVICE_MODULE.ID__XN)
                                    {
                                        HIS_SERE_SERV_EXT ext = lstSereServExt != null ? lstSereServExt.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                                        serviceTdo.Result = ext != null ? ext.CONCLUDE : null;
                                        serviceTdo.Description = ext != null ? ext.DESCRIPTION : null;
                                    }
                                    else
                                    {
                                        List<V_HIS_SERE_SERV_TEIN> sereServTeins = lstSereServTein != null ? lstSereServTein.Where(o => o.SERE_SERV_ID == item.ID && o.TEST_INDEX_ID.HasValue).ToList() : null;
                                        if (IsNotNullOrEmpty(sereServTeins))
                                        {
                                            serviceTdo.TestIndexs = new List<HisSereServTeinTDO>();
                                            foreach (V_HIS_SERE_SERV_TEIN tein in sereServTeins)
                                            {
                                                HisSereServTeinTDO teinTdo = new HisSereServTeinTDO();
                                                teinTdo.Description = tein.DESCRIPTION;
                                                teinTdo.Result = tein.VALUE;
                                                teinTdo.SereServId = item.ID;
                                                teinTdo.TestIndexCode = tein.TEST_INDEX_CODE;
                                                teinTdo.TestIndexName = tein.TEST_INDEX_NAME;
                                                teinTdo.TestIndexUnitName = tein.TEST_INDEX_UNIT_NAME;
                                                teinTdo.IsImportant = tein.IS_IMPORTANT == Constant.IS_TRUE;
                                                this.ProcessCheckNormal(teinTdo, tein, req);
                                                serviceTdo.TestIndexs.Add(teinTdo);
                                            }
                                        }
                                    }
                                }
                            }
                            reqTdo.Services = reqTdo.Services.OrderBy(o => o.ServiceTypeId != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN).ToList();
                        }
                        result = result.OrderByDescending(o => o.InstructionDate).ThenBy(t => !(t.Services != null && t.Services.Any(a => a.ServiceTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN))).ToList();
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

        private void ProcessCheckNormal(HisSereServTeinTDO teinTdo, V_HIS_SERE_SERV_TEIN tein, HIS_SERVICE_REQ req)
        {
            try
            {
                HIS_TEST_INDEX_RANGE testIndexRange = this.GetTestIndexRange(req.TDL_PATIENT_DOB, req.TDL_PATIENT_GENDER_ID ?? 0, tein.TEST_INDEX_ID.Value);
                if (testIndexRange != null)
                {
                    AssignNormal(teinTdo, testIndexRange);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void AssignNormal(HisSereServTeinTDO ti, HIS_TEST_INDEX_RANGE testIndexRange)
        {
            try
            {
                double? minValue = null, maxValue = null, value = null;
                if (ti != null && testIndexRange != null)
                {
                    ti.Description = "";
                    if (!String.IsNullOrWhiteSpace(testIndexRange.MIN_VALUE))
                    {
                        double v = 0;
                        if (double.TryParse((testIndexRange.MIN_VALUE), out v))
                        {
                            minValue = v;
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(testIndexRange.MAX_VALUE))
                    {
                        double v = 0;
                        if (double.TryParse((testIndexRange.MAX_VALUE), out v))
                        {
                            maxValue = v;
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(ti.Result))
                    {
                        double v = 0;
                        if (double.TryParse((ti.Result), out v))
                        {
                            value = v;
                        }
                    }

                    if (!String.IsNullOrEmpty(testIndexRange.NORMAL_VALUE))
                    {
                        ti.Description = testIndexRange.NORMAL_VALUE;
                        if (!String.IsNullOrWhiteSpace(ti.Description) && !String.IsNullOrWhiteSpace(ti.Result) && ti.Result.ToUpper() == ti.Description.ToUpper())
                        {
                            ti.IsNormal = true;
                        }
                        else if (!String.IsNullOrWhiteSpace(ti.Description) && !String.IsNullOrWhiteSpace(ti.Result) && ti.Result.ToUpper() != ti.Description.ToUpper())
                        {
                            ti.IsLower = true;
                            ti.IsHigher = true;
                        }
                    }
                    else
                    {
                        if (testIndexRange.IS_ACCEPT_EQUAL_MIN == 1 && testIndexRange.IS_ACCEPT_EQUAL_MAX == null)
                        {
                            if (testIndexRange.MIN_VALUE != null)
                            {
                                ti.Description += testIndexRange.MIN_VALUE + "<= ";
                            }

                            ti.Description += "X";

                            if (testIndexRange.MAX_VALUE != null)
                            {
                                ti.Description += " < " + testIndexRange.MAX_VALUE;
                            }

                            if (value.HasValue && minValue.HasValue && minValue.Value <= value.Value && maxValue.HasValue && value.Value < maxValue.Value)
                            {
                                ti.IsNormal = true;
                            }
                            else if (value.HasValue && minValue.HasValue && value.Value < minValue.Value)
                            {
                                ti.IsLower = true;
                            }
                            else if (value.HasValue && maxValue.HasValue && maxValue.Value <= value.Value)
                            {
                                ti.IsHigher = true;
                            }
                        }
                        else if (testIndexRange.IS_ACCEPT_EQUAL_MIN == 1 && testIndexRange.IS_ACCEPT_EQUAL_MAX == 1)
                        {
                            if (testIndexRange.MIN_VALUE != null)
                            {
                                ti.Description += testIndexRange.MIN_VALUE + "<= ";
                            }

                            ti.Description += "X";

                            if (testIndexRange.MAX_VALUE != null)
                            {
                                ti.Description += " <= " + testIndexRange.MAX_VALUE;
                            }

                            if (value.HasValue && minValue.HasValue && minValue.Value <= value.Value && maxValue.HasValue && value.Value <= maxValue.Value)
                            {
                                ti.IsNormal = true;
                            }
                            else if (value.HasValue && minValue.HasValue && value.Value < minValue.Value)
                            {
                                ti.IsLower = true;
                            }
                            else if (value.HasValue && maxValue.HasValue && maxValue.Value < value.Value)
                            {
                                ti.IsHigher = true;
                            }
                        }
                        else if (testIndexRange.IS_ACCEPT_EQUAL_MIN == null && testIndexRange.IS_ACCEPT_EQUAL_MAX == 1)
                        {
                            if (testIndexRange.MIN_VALUE != null)
                            {
                                ti.Description += testIndexRange.MIN_VALUE + "< ";
                            }

                            ti.Description += "X";

                            if (testIndexRange.MAX_VALUE != null)
                            {
                                ti.Description += " <= " + testIndexRange.MAX_VALUE;
                            }

                            if (value.HasValue && minValue.HasValue && minValue.Value < value.Value && maxValue.HasValue && value.Value <= maxValue.Value)
                            {
                                ti.IsNormal = true;
                            }
                            else if (value.HasValue && minValue.HasValue && value.Value < minValue.Value)
                            {
                                ti.IsLower = true;
                            }
                            else if (value.HasValue && maxValue.HasValue && maxValue.Value < value.Value)
                            {
                                ti.IsHigher = true;
                            }
                        }
                        else if (testIndexRange.IS_ACCEPT_EQUAL_MIN == null && testIndexRange.IS_ACCEPT_EQUAL_MAX == null)
                        {
                            if (testIndexRange.MIN_VALUE != null)
                            {
                                ti.Description += testIndexRange.MIN_VALUE + "< ";
                            }

                            ti.Description += "X";

                            if (testIndexRange.MAX_VALUE != null)
                            {
                                ti.Description += " < " + testIndexRange.MAX_VALUE;
                            }
                            if (value.HasValue && minValue.HasValue && minValue.Value < value.Value && maxValue.HasValue && value.Value < maxValue.Value)
                            {
                                ti.IsNormal = true;
                            }
                            else if (value.HasValue && minValue.HasValue && value.Value <= minValue.Value)
                            {
                                ti.IsLower = true;
                            }
                            else if (value.HasValue && maxValue.HasValue && maxValue.Value <= value.Value)
                            {
                                ti.IsHigher = true;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private HIS_TEST_INDEX_RANGE GetTestIndexRange(long dob, long genderId, long testIndexId)
        {
            HIS_TEST_INDEX_RANGE testIndexRange = null;
            try
            {
                if (HisTestIndexRangeCFG.DATA != null)
                {
                    long age = Inventec.Common.DateTime.Calculation.Age(dob);

                    var query = HisTestIndexRangeCFG.DATA.Where(o => o.TEST_INDEX_ID == testIndexId
                            && ((o.AGE_FROM.HasValue && o.AGE_FROM.Value <= age) || !o.AGE_FROM.HasValue)
                            && ((o.AGE_TO.HasValue && o.AGE_TO.Value >= age) || !o.AGE_TO.HasValue));

                    if (genderId == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE)
                    {
                        query = query.Where(o => o.IS_MALE == 1);
                    }
                    else if (genderId == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE)
                    {
                        query = query.Where(o => o.IS_FEMALE == 1);
                    }
                    testIndexRange = query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return testIndexRange;
        }
    }
}
