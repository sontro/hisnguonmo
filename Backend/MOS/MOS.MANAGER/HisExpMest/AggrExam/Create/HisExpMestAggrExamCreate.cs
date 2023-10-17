using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisPriorityType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Create
{
    class HisExpMestAggrExamCreate : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestAggrExamCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExamCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HisExpMestAggrSDO data, ref List<HIS_EXP_MEST> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> children = null;
                List<HIS_EXP_MEST> aggrs = null;
                bool valid = true;
                HisExpMestAggrExamCreateCheck checker = new HisExpMestAggrExamCreateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref children);
                if (valid)
                {
                    this.ProcessCreate(children, data.ReqRoomId, data.Description, ref aggrs);
                    resultData = aggrs;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollBack();
                result = false;
            }
            return result;
        }

        internal bool RunAuto(List<HIS_EXP_MEST> childExpMests, long reqRoomId)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> aggrs = null;
                this.ProcessCreate(childExpMests, reqRoomId, null, ref aggrs);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessCreate(List<HIS_EXP_MEST> childExpMests, long reqRoomId, string description, ref List<HIS_EXP_MEST> resultData)
        {
            List<HIS_EXP_MEST> aggrs = null;
            Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> dicExpMestAggr = new Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>>();
            this.CreateExpMestAggr(reqRoomId, null, childExpMests, dicExpMestAggr, ref aggrs);
            this.UpdateChildExpMest(dicExpMestAggr);

            resultData = aggrs;

            HisAggrExpMestLog.Run(dicExpMestAggr, LibraryEventLog.EventLog.Enum.HisExpMest_TongHopDonPhongKham);
        }

        private void CreateExpMestAggr(long reqRoomId, string description, List<HIS_EXP_MEST> children, Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> dicExpMestAggr, ref List<HIS_EXP_MEST> aggrs)
        {
            HisPriorityTypeFilterQuery filter = new HisPriorityTypeFilterQuery();
            filter.IS_FOR_PRESCRIPTION = true;
            List<HIS_PRIORITY_TYPE> pTypes = new HisPriorityTypeGet().Get(filter);

            var groups = children.GroupBy(o => new { o.MEDI_STOCK_ID, o.TDL_TREATMENT_ID });
            foreach (var g in groups)
            {
                HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                expMest.MEDI_STOCK_ID = g.Key.MEDI_STOCK_ID;
                expMest.TDL_TREATMENT_ID = g.Key.TDL_TREATMENT_ID;
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK;
                expMest.REQ_ROOM_ID = reqRoomId;
                expMest.DESCRIPTION = description;
                expMest.TDL_TREATMENT_CODE = g.FirstOrDefault().TDL_TREATMENT_CODE;
                expMest.TDL_PATIENT_ADDRESS = g.FirstOrDefault().TDL_PATIENT_ADDRESS;
                expMest.TDL_PATIENT_CODE = g.FirstOrDefault().TDL_PATIENT_CODE;
                expMest.TDL_PATIENT_DOB = g.FirstOrDefault().TDL_PATIENT_DOB;
                expMest.TDL_PATIENT_FIRST_NAME = g.FirstOrDefault().TDL_PATIENT_FIRST_NAME;
                expMest.TDL_PATIENT_GENDER_ID = g.FirstOrDefault().TDL_PATIENT_GENDER_ID;
                expMest.TDL_PATIENT_GENDER_NAME = g.FirstOrDefault().TDL_PATIENT_GENDER_NAME;
                expMest.TDL_PATIENT_ID = g.FirstOrDefault().TDL_PATIENT_ID;
                expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = g.FirstOrDefault().TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                expMest.TDL_PATIENT_LAST_NAME = g.FirstOrDefault().TDL_PATIENT_LAST_NAME;
                expMest.TDL_PATIENT_NAME = g.FirstOrDefault().TDL_PATIENT_NAME;
                expMest.TDL_PATIENT_TYPE_ID = g.FirstOrDefault().TDL_PATIENT_TYPE_ID;
                expMest.TDL_HEIN_CARD_NUMBER = g.FirstOrDefault().TDL_HEIN_CARD_NUMBER;
                expMest.EXP_MEST_REASON_ID = g.OrderBy(o => o.ID).FirstOrDefault().EXP_MEST_REASON_ID;
                // Lay thong tin uu tien
                HIS_EXP_MEST priority = IsNotNullOrEmpty(g.ToList()) ? g.ToList().FirstOrDefault(o => o.PRIORITY.HasValue) : null;
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
                HIS_EXP_MEST priorityType = IsNotNullOrEmpty(g.ToList()) ? g.ToList().FirstOrDefault(o => o.PRIORITY_TYPE_ID.HasValue) : null;
                if (priorityType != null)
                {
                    expMest.PRIORITY_TYPE_ID = priorityType.PRIORITY_TYPE_ID;
                }

                dicExpMestAggr.Add(expMest, g.ToList());
            }

            aggrs = dicExpMestAggr.Keys.ToList();
            if (!this.hisExpMestCreate.CreateList(aggrs))
            {
                throw new Exception("Tao phieu tong hop kham that bai. Ket thuc nghiep vu");
            }
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

        private void UpdateChildExpMest(Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> dicExpMestAggr)
        {
            List<string> sqls = new List<string>();

            List<HIS_EXP_MEST> aggrsExpMests = dicExpMestAggr.Keys.ToList();

            foreach (HIS_EXP_MEST aggr in aggrsExpMests)
            {
                List<HIS_EXP_MEST> tmp = dicExpMestAggr[aggr];

                //update exp_mest con
                string sqlExpMest = "UPDATE HIS_EXP_MEST SET AGGR_EXP_MEST_ID = {0}, TDL_AGGR_EXP_MEST_CODE = '{1}' WHERE %IN_CLAUSE%";
                sqlExpMest = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlExpMest, "ID");
                sqlExpMest = string.Format(sqlExpMest, aggr.ID, aggr.EXP_MEST_CODE);
                sqls.Add(sqlExpMest);

                //update exp_mest_medicine
                string sqlMedicine = "UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                sqlMedicine = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlMedicine, "EXP_MEST_ID");
                sqlMedicine = string.Format(sqlMedicine, aggr.ID);
                sqls.Add(sqlMedicine);

                //update exp_mest_material
                string sqlMaterial = "UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                sqlMaterial = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlMaterial, "EXP_MEST_ID");
                sqlMaterial = string.Format(sqlMaterial, aggr.ID);
                sqls.Add(sqlMaterial);
            }

            if (IsNotNullOrEmpty(sqls))
            {
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Update child exp mest that bai. Rollback du lieu");
                }
            }
        }

        private void RollBack()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
