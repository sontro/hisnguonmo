using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisPriorityType;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.ByTreatAndStock
{
    class HisExpMestAggrExamByTreatAndStock : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestAggrExamByTreatAndStock()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExamByTreatAndStock(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(AggrExamByTreatAndStockSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                List<HIS_EXP_MEST> children = null;
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisExpMestAggrExamByTreatAndStockCheck commonChecker = new HisExpMestAggrExamByTreatAndStockCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.IsValidTreatmentCode(data.TreatmentCode, ref treatment);
                valid = valid && commonChecker.IsPause(treatment);
                //valid = valid && checker.IsExamType(treatment);
                valid = valid && commonChecker.IsValidExpMest(treatment, data.MediStockId, ref children);
                if (valid)
                {
                    HIS_EXP_MEST aggr = children.FirstOrDefault(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK || o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL);
                    if (aggr != null)
                    {
                        resultData = aggr;
                    }
                    else
                    {
                        // loc lai children lay ra cac don chua xuat
                        children = children.Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE && o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT).ToList();

                        HisPriorityTypeFilterQuery filter = new HisPriorityTypeFilterQuery();
                        filter.IS_FOR_PRESCRIPTION = true;
                        List<HIS_PRIORITY_TYPE> pTypes = new HisPriorityTypeGet().Get(filter);

                        // Tao don tong hop phong kham moi
                        HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                        expMest.MEDI_STOCK_ID = data.MediStockId;
                        expMest.TDL_TREATMENT_ID = treatment.ID;
                        expMest.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                        expMest.TDL_PATIENT_ADDRESS = treatment.TDL_PATIENT_ADDRESS;
                        expMest.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                        expMest.TDL_PATIENT_DOB = treatment.TDL_PATIENT_DOB;
                        expMest.TDL_PATIENT_FIRST_NAME = treatment.TDL_PATIENT_FIRST_NAME;
                        expMest.TDL_PATIENT_GENDER_ID = treatment.TDL_PATIENT_GENDER_ID;
                        expMest.TDL_PATIENT_GENDER_NAME = treatment.TDL_PATIENT_GENDER_NAME;
                        expMest.TDL_PATIENT_ID = treatment.PATIENT_ID;
                        expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                        expMest.TDL_PATIENT_LAST_NAME = treatment.TDL_PATIENT_LAST_NAME;
                        expMest.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                        expMest.TDL_PATIENT_TYPE_ID = treatment.TDL_PATIENT_TYPE_ID;
                        expMest.TDL_HEIN_CARD_NUMBER = treatment.TDL_HEIN_CARD_NUMBER;
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                        if (children.Exists(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT))
                        {
                            expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL;
                        }
                        else
                            expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK;
                        expMest.EXP_MEST_REASON_ID = children.OrderBy(o => o.ID).FirstOrDefault().EXP_MEST_REASON_ID;
                        // Lay thong tin uu tien
                        HIS_EXP_MEST priority = IsNotNullOrEmpty(children) ? children.FirstOrDefault(o => o.PRIORITY.HasValue) : null;
                        if (priority != null)
                        {
                            expMest.PRIORITY = priority.PRIORITY;
                        }
                        if (IsNotNullOrEmpty(pTypes))
                        {
                            var dob = expMest.TDL_PATIENT_DOB.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.TDL_PATIENT_DOB.Value) : null;
                            long? patientAge = dob.HasValue ? (long?)(DateTime.Today.Year - dob.Value.Year) : null;
                            pTypes = pTypes.Where(o =>
                                    (o.AGE_FROM == null || o.AGE_FROM <= patientAge)
                                    && (o.AGE_TO == null || o.AGE_TO >= patientAge)
                                    && (String.IsNullOrEmpty(o.BHYT_PREFIXS) || (!String.IsNullOrEmpty(o.BHYT_PREFIXS) && StartIn(o.BHYT_PREFIXS, expMest.TDL_HEIN_CARD_NUMBER)))
                                    && ((o.AGE_FROM.HasValue && o.AGE_FROM > 0) || (o.AGE_TO.HasValue && o.AGE_TO > 0) || (!String.IsNullOrEmpty(o.BHYT_PREFIXS)))
                                    ).ToList();
                            if (IsNotNullOrEmpty(pTypes))
                            {
                                expMest.PRIORITY = Constant.IS_TRUE;
                            }
                        }
                        HIS_EXP_MEST priorityType = IsNotNullOrEmpty(children) ? children.FirstOrDefault(o => o.PRIORITY_TYPE_ID.HasValue) : null;
                        if (priorityType != null)
                        {
                            expMest.PRIORITY_TYPE_ID = priorityType.PRIORITY_TYPE_ID;
                        }

                        var stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.MediStockId);
                        if (stock != null)
                        {
                            expMest.REQ_ROOM_ID = stock.ROOM_ID;
                        }

                        if (!this.hisExpMestCreate.Create(expMest))
                        {
                            throw new Exception("Tao phieu tong hop kham that bai. Ket thuc nghiep vu");
                        }

                        List<string> sqls = new List<string>();
                        //update exp_mest con
                        string sqlExpMest = "UPDATE HIS_EXP_MEST SET AGGR_EXP_MEST_ID = {0}, TDL_AGGR_EXP_MEST_CODE = '{1}' WHERE %IN_CLAUSE%";
                        sqlExpMest = DAOWorker.SqlDAO.AddInClause(children.Select(o => o.ID).ToList(), sqlExpMest, "ID");
                        sqlExpMest = string.Format(sqlExpMest, expMest.ID, expMest.EXP_MEST_CODE);
                        sqls.Add(sqlExpMest);

                        //update exp_mest_medicine
                        string sqlMedicine = "UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                        sqlMedicine = DAOWorker.SqlDAO.AddInClause(children.Select(o => o.ID).ToList(), sqlMedicine, "EXP_MEST_ID");
                        sqlMedicine = string.Format(sqlMedicine, expMest.ID);
                        sqls.Add(sqlMedicine);

                        //update exp_mest_material
                        string sqlMaterial = "UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                        sqlMaterial = DAOWorker.SqlDAO.AddInClause(children.Select(o => o.ID).ToList(), sqlMaterial, "EXP_MEST_ID");
                        sqlMaterial = string.Format(sqlMaterial, expMest.ID);
                        sqls.Add(sqlMaterial);

                        if (IsNotNullOrEmpty(sqls))
                        {
                            if (!DAOWorker.SqlDAO.Execute(sqls))
                            {
                                throw new Exception("Update child exp mest that bai. Rollback du lieu");
                            }
                        }

                        resultData = new HisExpMestGet().GetById(expMest.ID);
                    }

                    // Nhat ky tac dong
                    Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> dicExpMestAggr = new Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>>();
                    dicExpMestAggr.Add(resultData, children);
                    HisAggrExpMestLog.Run(dicExpMestAggr, LibraryEventLog.EventLog.Enum.HisExpMest_TongHopDonPhongKham);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private bool StartIn(string BHYT_PREFIXS, string heincardnumber)
        {
            bool valid = false;
            try
            {
                List<string> checkData = null;
                if (!String.IsNullOrEmpty(BHYT_PREFIXS) && !String.IsNullOrEmpty(heincardnumber))
                {
                    var arrPrefixs = BHYT_PREFIXS.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrPrefixs != null && arrPrefixs.Count() > 0)
                    {
                        checkData = arrPrefixs.ToList().Where(o => heincardnumber.StartsWith(o)).ToList();
                        valid = (checkData != null && checkData.Count > 0) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }

        private void RollBack()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}