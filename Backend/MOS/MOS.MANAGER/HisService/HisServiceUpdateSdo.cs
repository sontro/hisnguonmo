using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisService
{
    partial class HisServiceUpdate : BusinessBase
    {
        class ThreadHisServiceLog
        {
            public HIS_SERVICE EditData { get; set; }
            public HIS_SERVICE OldData { get; set; }
            public HisServiceSDO sdo { get; set; }
            public EventLog.Enum logEnum { get; set; }
        }
        internal bool UpdateSdo(HisServiceSDO data)
        {
            HIS_SERVICE oldData = null;
            return this.UpdateSdo(data, ref oldData);
        }
        ///
        ///DUNGLH CAN KIEM TRA LAI
        ///
        internal bool UpdateSdo(HisServiceSDO data, ref HIS_SERVICE oldData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceCheck checker = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data.HisService);
                HIS_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.HisService.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HisService.SERVICE_CODE, data.HisService.ID);
                valid = valid && checker.IsValidData(data.HisService);
                if (valid)
                {
                    HIS_SERVICE parent = null;

                    this.beforeUpdateHisServices.Add(raw);

                    bool updateIsLeaf = (data.HisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                        || data.HisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                        || data.HisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT);

                    if (data.HisService.PARENT_ID.HasValue)
                    {
                        parent = new HisServiceGet().GetById(data.HisService.PARENT_ID.Value);
                        if (parent == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ParentId invalid:" + LogUtil.TraceData("data", data.HisService.PARENT_ID));
                        }
                        if (parent.SERVICE_TYPE_ID != data.HisService.SERVICE_TYPE_ID)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ServiceTypeId cua parent khac ServiceTypeId cua con:" + LogUtil.TraceData("data", data.HisService));
                        }
                    }
                    if (!updateIsLeaf)
                    {
                        List<HIS_SERVICE> existChildren = new HisServiceGet().GetByParentId(data.HisService.ID);
                        if (IsNotNullOrEmpty(existChildren))
                        {
                            data.HisService.IS_LEAF = null;
                        }
                        else
                        {
                            data.HisService.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        }
                    }

                    if (!DAOWorker.HisServiceDAO.Update(data.HisService))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisService that bai." + LogUtil.TraceData("data", data));
                    }
                    if (!updateIsLeaf)
                    {
                        //set lai is_leaf = null cho dich vu parent
                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            parent.IS_LEAF = null;
                            if (!new HisServiceUpdate(param).Update(parent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                        //set lai is_leaf = null cho dich vu parent cu
                        if (raw.PARENT_ID.HasValue)
                        {
                            List<HIS_SERVICE> children = new HisServiceGet().GetByParentId(raw.PARENT_ID.Value);
                            if (!IsNotNullOrEmpty(children))
                            {
                                HIS_SERVICE oldParent = new HisServiceGet().GetById(raw.PARENT_ID.Value);
                                oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                                if (!new HisServiceUpdate(param).Update(oldParent))
                                {
                                    throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                                }
                            }
                        }
                    }
                    this.UpdateTdlService(data);
                    result = true;
                    oldData = data.HisService;
                    HisServiceLog.Run(data.HisService, raw, data, LibraryEventLog.EventLog.Enum.HisService_SuaDanhMucKyThuat);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void UpdateTdlService(HisServiceSDO data)
        {
            try
            {
                if (data.UpdateSereServ.HasValue)
                {
                    List<long> listTreatmentId = new List<long>();
                    V_HIS_SERVICE service = new HisServiceGet().GetViewById(data.HisService.ID);
                    if (service == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc V_HIS_SERVICE theo Id");
                    }

                    string storedSql = "PKG_UPDATE_TDL_SERVICE.PRO_UPDATE_TDL_SERVICE";

                    OracleParameter isUpdateLockTreatmentPar = null;
                    long updateLockTreatment = data.UpdateSereServ.Value ? 1 : 0;

                    isUpdateLockTreatmentPar = new OracleParameter("P_IS_UPDATE_LOCK_TREATMENT", OracleDbType.Int64, updateLockTreatment, ParameterDirection.Input);

                    string modifier = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    OracleParameter serviceIdPar = new OracleParameter("P_ID", OracleDbType.Int64, service.ID, ParameterDirection.Input);
                    OracleParameter serviceCodePar = new OracleParameter("P_SERVICE_CODE", OracleDbType.Varchar2, service.SERVICE_CODE, ParameterDirection.Input);
                    OracleParameter serviceNamePar = new OracleParameter("P_SERVICE_NAME", OracleDbType.Varchar2, service.SERVICE_NAME, ParameterDirection.Input);
                    OracleParameter heinServiceBHYTCodePar = new OracleParameter("P_HEIN_SERVICE_BHYT_CODE", OracleDbType.Varchar2, service.HEIN_SERVICE_BHYT_CODE, ParameterDirection.Input);
                    OracleParameter heinServiceBHYTNamePar = new OracleParameter("P_HEIN_SERVICE_BHYT_NAME", OracleDbType.Varchar2, service.HEIN_SERVICE_BHYT_NAME, ParameterDirection.Input);
                    OracleParameter heinOrderPar = new OracleParameter("P_HEIN_ORDER", OracleDbType.Varchar2, service.HEIN_ORDER, ParameterDirection.Input);
                    OracleParameter serviceTypeIdPar = new OracleParameter("P_SERVICE_TYPE_ID", OracleDbType.Int64, service.SERVICE_TYPE_ID, ParameterDirection.Input);
                    OracleParameter serviceUnitIdPar = new OracleParameter("P_SERVICE_UNIT_ID", OracleDbType.Int64, service.SERVICE_UNIT_ID, ParameterDirection.Input);
                    OracleParameter heinServiceTypeIdPar = new OracleParameter("P_HEIN_SERVICE_TYPE_ID", OracleDbType.Int64, service.HEIN_SERVICE_TYPE_ID, ParameterDirection.Input);
                    OracleParameter activeIngrBhytCodePar = new OracleParameter("P_ACTIVE_INGR_BHYT_CODE", OracleDbType.Varchar2, service.ACTIVE_INGR_BHYT_CODE, ParameterDirection.Input);
                    OracleParameter activeIngrBhytNamePar = new OracleParameter("P_ACTIVE_INGR_BHYT_NAME", OracleDbType.Varchar2, service.ACTIVE_INGR_BHYT_NAME, ParameterDirection.Input);
                    OracleParameter specialityCodePar = new OracleParameter("P_SPECIALITY_CODE", OracleDbType.Varchar2, service.SPECIALITY_CODE, ParameterDirection.Input);
                    OracleParameter hstBhytCodePar = new OracleParameter("P_HST_BHYT_CODE", OracleDbType.Varchar2, service.HEIN_SERVICE_TYPE_BHYT_CODE, ParameterDirection.Input);
                    OracleParameter pacsTypeCodePar = new OracleParameter("P_PACS_TYPE_CODE", OracleDbType.Varchar2, service.PACS_TYPE_CODE, ParameterDirection.Input);
                    OracleParameter modifierPar = new OracleParameter("P_MODIFIER", OracleDbType.Varchar2, modifier, ParameterDirection.Input);
                    OracleParameter packageIdPar = new OracleParameter("P_PACKAGE_ID", OracleDbType.Int64, service.PACKAGE_ID, ParameterDirection.Input);
                    OracleParameter isOtherSourcePaidPar = new OracleParameter("P_IS_OTHER_SOURCE_PAID", OracleDbType.Int64, service.IS_OTHER_SOURCE_PAID, ParameterDirection.Input);
                    OracleParameter taxRateType = new OracleParameter("P_TAX_RATE_TYPE", OracleDbType.Int64, service.TAX_RATE_TYPE, ParameterDirection.Input);
					OracleParameter descriptionPar = new OracleParameter("P_DESCRIPTION", OracleDbType.Varchar2, service.DESCRIPTION, ParameterDirection.Input);
                    OracleParameter vaccine = new OracleParameter("P_IS_VACCINE", OracleDbType.Int64, service.IS_VACCINE, ParameterDirection.Input);
                    OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int64, ParameterDirection.Output);


                    object resultHolder = null;
                    if (!DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, isUpdateLockTreatmentPar, serviceIdPar, serviceCodePar, serviceNamePar, heinServiceBHYTCodePar, heinServiceBHYTNamePar, heinOrderPar, serviceTypeIdPar, serviceUnitIdPar, heinServiceTypeIdPar, activeIngrBhytCodePar, activeIngrBhytNamePar, specialityCodePar, hstBhytCodePar, pacsTypeCodePar, modifierPar, packageIdPar, isOtherSourcePaidPar, taxRateType, descriptionPar, vaccine, resultPar))
                    {
                        throw new Exception("ExecuteStored PKG_UPDATE_TDL_SERVICE faild");
                    }
                    if (resultHolder == null)
                    {
                        throw new Exception("ExecuteStored PKG_UPDATE_TDL_SERVICE faild. resultHolder is null");
                    }
                    var result = (long)resultHolder;
                    if (result != 1)
                    {
                        throw new Exception("Cap nhat sere_serv that bai:" + LogUtil.TraceData("service", service));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                //Tham so thu 18 chua output
                if (parameters.Last() != null && parameters.Last().Value != null)
                {
                    long id = long.Parse(parameters.Last().Value.ToString());
                    resultHolder = id;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Tu dong xuat XML thong tuyen
        /// </summary>
        /// <param name="treatment"></param>
        //private void InitThreadServicelog(HIS_TREATMENT treatment, HisTreatmentFinishSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas)
        //{
        //    try
        //    {
        //        if (IsNotNull(treatment) && IsNotNullOrEmpty(ptas))
        //        {
        //            HIS_PATIENT_TYPE_ALTER lastPta = ptas.OrderByDescending(t => t.LOG_TIME).ThenByDescending(t => t.ID).FirstOrDefault();
        //            if (data.IsExpXml4210Collinear && lastPta != null && lastPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
        //            {
        //                Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXml4210Collinear));
        //                thread.Priority = ThreadPriority.Lowest;
        //                Thread.Sleep(2000); // Doi cac tien trinh khac thuc hien xong tranh bi update de
        //                ThreadExportXmlData threadData = new ThreadExportXmlData();
        //                threadData.Branch = new TokenManager().GetBranch();
        //                threadData.PatientTypeAlter = lastPta;
        //                threadData.TreatmentCode = treatment.TREATMENT_CODE;
        //                threadData.TreatmentId = treatment.ID;
        //                threadData.PatientCode = treatment.TDL_PATIENT_CODE;
        //                thread.Start(threadData);
        //            }

        //            if (lastPta != null)
        //            {
        //                Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXml2076));
        //                thread.Priority = ThreadPriority.Lowest;

        //                thread.Start(treatment.ID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //    }
        //}

        //private void ExportHisService(object data)
        //{
        //    try
        //    {
        //        ThreadHisServiceLog d = (ThreadHisServiceLog)data;
        //        new HisServiceLog().Run(d.EditData, d.OldData, d.sdo, d.logEnum);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //    }
        //}

        //private void ExportXml2076(object data)
        //{
        //    try
        //    {
        //        long treatId = (long)data;
        //        HisServiceLog.Run(data.HisService, raw, data, LibraryEventLog.EventLog.Enum.HisService_SuaDanhMucKyThuat);
        //        new HisServiceLog().Run(data.HisService, raw, data, LibraryEventLog.EventLog.Enum.HisService_SuaDanhMucKyThuat);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //    }
        //}
    }
}
