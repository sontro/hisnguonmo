using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
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
                valid = valid && checker.IsPause(treatment);
                valid = valid && checker.IsExamType(treatment);
                valid = valid && commonChecker.IsValidExpMest(treatment.ID, data.MediStockId, ref children);
                if (valid)
                {

                    var aggr = children.FirstOrDefault(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK);
                    if (aggr != null)
                    {
                        resultData = aggr;
                    }
                    else
                    {
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
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK;

                        // Lay thong tin uu tien
                        List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByTreatmentId(treatment.ID);
                        HIS_SERVICE_REQ priorityReq = IsNotNullOrEmpty(reqs) ? reqs.FirstOrDefault(o => o.PRIORITY.HasValue) : null;
                        if (priorityReq != null)
                        {
                            expMest.PRIORITY = priorityReq.PRIORITY;
                        }
                        HIS_SERVICE_REQ priorityTypeReq = IsNotNullOrEmpty(reqs) ? reqs.FirstOrDefault(o => o.PRIORITY_TYPE_ID.HasValue) : null;
                        if (priorityReq != null)
                        {
                            expMest.PRIORITY_TYPE_ID = priorityReq.PRIORITY_TYPE_ID;
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

        private void RollBack()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}