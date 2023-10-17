using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentTruncate : BusinessBase
    {
        internal HisTreatmentTruncate()
            : base()
        {

        }

        internal HisTreatmentTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                HIS_TREATMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnTemporaryLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentDAO.Truncate(data);
                }

                new EventLogGenerator(MOS.LibraryEventLog.EventLog.Enum.HisTreatment_XoaHoSo).ExpMestCode(raw.TREATMENT_CODE).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateTestData(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                HIS_TREATMENT raw = null;
                //valid = valid && CheckAdmin(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && CheckData(param, raw.ID);
                if (valid)
                {
                    string storedSql = "PKG_DELETE_TREATMENT.PRO_RUN";

                    OracleParameter idPar = new OracleParameter("P_ID", OracleDbType.Int32, data.ID, ParameterDirection.Input);
                    OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, resultPar, idPar))
                    {
                        if (resultHolder != null)
                        {
                            result = (bool)resultHolder;
                        };
                    }
                }

                if (result) new EventLogGenerator(MOS.LibraryEventLog.EventLog.Enum.HisTreatment_XoaHoSo).TreatmentCode(raw.TREATMENT_CODE).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                //Tham so thu 1 chua output
                if (parameters[0] != null && parameters[0].Value != null)
                {
                    short isTrue = short.Parse(parameters[0].Value.ToString());
                    resultHolder = isTrue == Constant.IS_TRUE;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private bool CheckData(CommonParam param, long id)
        {
            bool valid = true;
            try
            {
                List<HIS_SERVICE_REQ> hisServiceReqs = new HisServiceReq.HisServiceReqGet().GetByTreatmentId(id);
                List<HIS_SERE_SERV> hisSereServs = new HisSereServ.HisSereServGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    if (HisTreatmentCFG.IS_ALLOW_DELETING_IF_EXIST_SERVICE_REQ)
                    {
                        // Danh sach id y lenh DVKT
                        List<long> reqTypeIds = new List<long>()
                        {
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN,
                            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL
                        };
                        if (hisServiceReqs.Exists(o => !reqTypeIds.Contains(o.SERVICE_REQ_TYPE_ID)))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepXoaKhiTatCaCacYLenhLaDVKT);
                            return false;
                        }
                        else
                        {
                            if (hisServiceReqs.Exists(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL || o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiYLenhDangXuLyHoacHoanThanh);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        var listSereServ = hisSereServs.Where(o => o.IS_NO_EXECUTE != 1).ToList();
                        if (IsNotNullOrEmpty(listSereServ))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_TonTaiDuLieu);
                            return false;
                        }
                    }
                }

                List<HIS_TRANSACTION> hisTransactions = new HisTransaction.HisTransactionGet().GetByTreatmentId(id);
                if (IsNotNullOrEmpty(hisTransactions))
                {
                    List<HIS_TRANSACTION> listCheck = HisTreatmentCFG.DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION ?
                        hisTransactions : hisTransactions.Where(o => o.IS_DELETE != 1 && o.IS_CANCEL != 1).ToList();

                    if (IsNotNullOrEmpty(listCheck))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TonTaiDuLieu);
                        return false;
                    }
                }
                
                if (IsNotNullOrEmpty(hisServiceReqs))
                {
                    HisExpMest.Common.Get.HisExpMestFilterQuery filter = new HisExpMest.Common.Get.HisExpMestFilterQuery();
                    filter.SERVICE_REQ_IDs = hisServiceReqs.Select(o => o.ID).ToList();
                    var hisExpMest = new HisExpMest.Common.Get.HisExpMestGet().Get(filter);
                    if (IsNotNullOrEmpty(hisExpMest))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieu);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }

        internal bool TruncateList(List<HIS_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    foreach (HIS_TREATMENT treatment in listData)
                    {
                        if (!this.Truncate(treatment))
                        {
                            throw new Exception("Truncate du lieu treatment that bai." + LogUtil.TraceData("treatment", treatment));
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
