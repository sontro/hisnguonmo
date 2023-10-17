using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTestSampleType;
using MOS.MANAGER.HisTreatment;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisServiceReq.Test
{
    partial class HisServiceReqTestUpdate : BusinessBase
    {
        /// <summary>
        /// Cap nhat thong tin ket qua cua yeu cau xet nghiem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool UpdateResult(HisTestResultTDO data)
        {
            LogSystem.Debug("Input tra ket qua tu he thong LIS: " + LogUtil.TraceData("data", data));
            bool useBarcode = LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE;
            bool result = this.UpdateResult(data, useBarcode);
            LogSystem.Debug("Ket qua cap nhat ket qua XN tu he thong LIS: " + LogUtil.TraceData("result", result));
            return result;
        }

        /// <summary>
        /// Cap nhat dich vu da lay mau hay chua
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UpdateSpecimen(HisTestServiceReqTDO data)
        {
            LogSystem.Debug("Input 'cap nhat trang thai lay mau' tu he thong LIS: " + LogUtil.TraceData("data", data));
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(data.ServiceReqCode);
                valid = valid && IsNotNullOrEmpty(data.TestServiceTypeList);
                if (valid)
                {
                    HIS_SERVICE_REQ hisServiceReq = null;

                    //Lay thong tin HisTestServiceReq tuong ung voi barcode
                    if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
                    {
                        hisServiceReq = new HisServiceReqGet().GetByBarcode(data.ServiceReqCode);
                    }
                    else
                    {
                        hisServiceReq = new HisServiceReqGet().GetByCode(data.ServiceReqCode);
                    }

                    if (hisServiceReq == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay HIS_SERVICE_REQ tuong ung voi ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    if (hisServiceReq.IS_NO_EXECUTE.HasValue && hisServiceReq.IS_NO_EXECUTE.Value == Constant.IS_TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongThucHien);
                        throw new Exception("HIS_SERVICE_REQ khong thuc hien, ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    //Lay du lieu HisSereServ tuong ung voi ServiceReq o tren
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(hisServiceReq.ID);
                    if (!IsNotNullOrEmpty(sereServs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay sereServs tuong ung voi SERVICE_REQ_ID: " + hisServiceReq.ID);
                    }

                    if (LisCFG.LIS_CHECK_FEE_WHEN_SPECIMEN)
                    {
                        HIS_TREATMENT treatment = new HisTreatmentGet().GetById(hisServiceReq.TREATMENT_ID);

                        List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                        if (sereServIds == null)
                        {
                            throw new Exception("sereServIds dang bi null ");
                        }
                        List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                        List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                        List<HIS_SERE_SERV_DEBT> depts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                        List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                        List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);
                        V_HIS_TREATMENT_FEE_1 treat = new HisTreatmentGet().GetFeeView1ById(treatment.ID);

                        //Neu dich vu ko cho phep bat dau thi bo qua
                        if (!SendIntegratorCheck.IsAllowSend(treat, hisServiceReq, sereServs, bills, deposits, repays, depts))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_NoVienPhi);
                            return false;
                        }
                    }

                    List<HIS_SERE_SERV> listToUpdate = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> beforeUpdate = new List<HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    foreach (HisTestServiceTypeTDO t in data.TestServiceTypeList)
                    {
                        List<HIS_SERE_SERV> existedList = sereServs.Where(o => o.TDL_SERVICE_CODE.Equals(t.TestServiceTypeCode)).ToList();
                        List<HIS_SERE_SERV> newList = Mapper.Map<List<HIS_SERE_SERV>>(existedList);
                        List<HIS_SERE_SERV> oldList = Mapper.Map<List<HIS_SERE_SERV>>(existedList);
                        short? isSpecimen = null;
                        if (t.IsSpecimen)
                        {
                            isSpecimen = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        newList.ForEach(o => o.IS_SPECIMEN = isSpecimen);
                        listToUpdate.AddRange(newList);
                        beforeUpdate.AddRange(oldList);
                    }

                    //Cap nhat trang thai dang xu ly cho phieu yeu cau xet nghiem
                    long? startTime = null;
                    if (HisServiceReqCFG.TEST_START_TIME_OPTION == HisServiceReqCFG.TestStartTimeOption.BY_SAMPLE_TIME)
                    {
                        startTime = data.SampleTime;
                    }
                    else if (HisServiceReqCFG.TEST_START_TIME_OPTION == HisServiceReqCFG.TestStartTimeOption.BY_RECEIVE_SAMPLE_TIME)
                    {
                        startTime = data.ReceiveSampleTime;
                    }
                    bool startTimeByStart = false;
                    string tomTatLoaiXuLyXetNghiem = MessageUtil.GetMessage(LibraryMessage.Message.Enum.XuLyXetNghiem, param.LanguageCode);
                    if (listToUpdate.Exists(e => e.IS_SPECIMEN == MOS.UTILITY.Constant.IS_TRUE) && hisServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        HIS_SERVICE_REQ rs = null;
                        if (!new HisServiceReqUpdateStart().Start(hisServiceReq, false, ref rs, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName(), Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName(), startTime))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'dang thuc hien' that bai." + LogUtil.TraceData("hisServiceReq", hisServiceReq));
                        }
                    }
                    else if (hisServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && !listToUpdate.Exists(e => e.IS_SPECIMEN == MOS.UTILITY.Constant.IS_TRUE))
                    {
                        tomTatLoaiXuLyXetNghiem = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HuyXuLyXetNghiem, param.LanguageCode);
                        if (!new HisServiceReqUpdateStart().Unstart(hisServiceReq.ID))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'chua thuc hien' that bai." + LogUtil.TraceData("hisServiceReq", hisServiceReq));
                        }
                    }

                    //dong ho so dieu tri van cho cap nhat ket qua XN
                    result = new HisSereServUpdate(param).UpdateList(listToUpdate, beforeUpdate, false);
                    if (startTime.HasValue && hisServiceReq.START_TIME != null
                            && (HisServiceReqCFG.TEST_START_TIME_OPTION == HisServiceReqCFG.TestStartTimeOption.BY_SAMPLE_TIME || HisServiceReqCFG.TEST_START_TIME_OPTION == HisServiceReqCFG.TestStartTimeOption.BY_RECEIVE_SAMPLE_TIME))
                    {
                        startTimeByStart = true;
                    }

                    if (hisServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && !listToUpdate.Exists(e => e.IS_SPECIMEN == MOS.UTILITY.Constant.IS_TRUE))
                    {
                        //huy xu ly thi update ve null
                        //trong buong khong update ve null
                        V_HIS_ROOM reqRoom = HisRoomCFG.DATA.Where(o => o.ID == hisServiceReq.REQUEST_ROOM_ID).FirstOrDefault();
                        if (IsNotNull(reqRoom) && reqRoom.ROOM_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG)
                        {
                            hisServiceReq.SAMPLER_LOGINNAME = data.SampleLoginName;
                            hisServiceReq.SAMPLER_USERNAME = data.SampleUserName;
                            hisServiceReq.SAMPLE_TIME = null;
                        }

                        hisServiceReq.RECEIVE_SAMPLE_LOGINNAME = data.ReceiveSampleLoginname;
                        hisServiceReq.RECEIVE_SAMPLE_USERNAME = data.ReceiveSampleUsername;
                        hisServiceReq.RECEIVE_SAMPLE_TIME = data.ReceiveSampleTime;
                        hisServiceReq.TEST_SAMPLE_TYPE_ID = null;

                        string sql = "UPDATE HIS_SERVICE_REQ SET SAMPLER_LOGINNAME = :param1, SAMPLER_USERNAME = :param2, RECEIVE_SAMPLE_LOGINNAME = :param3, RECEIVE_SAMPLE_USERNAME  = :param4, RECEIVE_SAMPLE_TIME = :param5, TEST_SAMPLE_TYPE_ID = :param6, SAMPLE_TIME = :param7 WHERE ID = :param8 ";

                        ///execute sql se thuc hien cuoi cung
                        if (!DAOWorker.SqlDAO.Execute(sql, hisServiceReq.SAMPLER_LOGINNAME, hisServiceReq.SAMPLER_USERNAME, hisServiceReq.RECEIVE_SAMPLE_LOGINNAME, hisServiceReq.RECEIVE_SAMPLE_USERNAME, hisServiceReq.RECEIVE_SAMPLE_TIME, hisServiceReq.TEST_SAMPLE_TYPE_ID, hisServiceReq.SAMPLE_TIME, hisServiceReq.ID))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("sqls Failed", sql));
                            throw new Exception("Cap nhat approver info cho HisServiceReq that bai");
                        }
                    }
                    else if (data.SampleTime.HasValue || data.ReceiveSampleTime.HasValue || !String.IsNullOrWhiteSpace(data.TestSampleTypeCode) || startTime.HasValue)
                    {
                        if (data.SampleTime.HasValue)
                        {
                            hisServiceReq.SAMPLER_LOGINNAME = data.SampleLoginName;
                            hisServiceReq.SAMPLER_USERNAME = data.SampleUserName;
                            hisServiceReq.SAMPLE_TIME = data.SampleTime;
                        }

                        if (data.ReceiveSampleTime.HasValue)
                        {
                            hisServiceReq.RECEIVE_SAMPLE_LOGINNAME = data.ReceiveSampleLoginname;
                            hisServiceReq.RECEIVE_SAMPLE_USERNAME = data.ReceiveSampleUsername;
                            hisServiceReq.RECEIVE_SAMPLE_TIME = data.ReceiveSampleTime;
                        }

                        if (!String.IsNullOrWhiteSpace(data.TestSampleTypeCode))
                        {
                            HIS_TEST_SAMPLE_TYPE testSampleType = new HisTestSampleTypeGet().GetByCode(data.TestSampleTypeCode);
                            if (testSampleType != null)
                                hisServiceReq.TEST_SAMPLE_TYPE_ID = testSampleType.ID;
                        }

                        StringBuilder str = new StringBuilder("UPDATE HIS_SERVICE_REQ SET");
                        List<object> listParam = new List<object>();
                        str.Append(" SAMPLER_LOGINNAME = :param1,");
                        listParam.Add(hisServiceReq.SAMPLER_LOGINNAME);

                        str.Append(" SAMPLER_USERNAME = :param2,");
                        listParam.Add(hisServiceReq.SAMPLER_USERNAME);

                        str.Append(" RECEIVE_SAMPLE_LOGINNAME = :param3,");
                        listParam.Add(hisServiceReq.RECEIVE_SAMPLE_LOGINNAME);

                        str.Append(" RECEIVE_SAMPLE_USERNAME = :param4,");
                        listParam.Add(hisServiceReq.RECEIVE_SAMPLE_USERNAME);

                        str.Append(" RECEIVE_SAMPLE_TIME = :param5,");
                        listParam.Add(hisServiceReq.RECEIVE_SAMPLE_TIME);

                        str.Append(" TEST_SAMPLE_TYPE_ID = :param6,");
                        listParam.Add(hisServiceReq.TEST_SAMPLE_TYPE_ID);

                        str.Append(" SAMPLE_TIME = :param7");
                        listParam.Add(hisServiceReq.SAMPLE_TIME);

                        if (startTimeByStart == true)
                        {
                            str.Append(", START_TIME = :param8");
                            listParam.Add(startTime);
                        }
                        str.Append(" WHERE ID = :param9");
                        listParam.Add(hisServiceReq.ID);
                        object[] prs = listParam.ToArray();

                        ///execute sql se thuc hien cuoi cung
                        if (!DAOWorker.SqlDAO.Execute(str.ToString(), prs))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("sqls Failed", str.ToString()));
                            throw new Exception("Cap nhat approver info cho HisServiceReq that bai");
                        }
                    }

                    if (Config.LisCFG.LIS_INTEGRATE_OPTION != (int)LisCFG.LisIntegrateOption.LIS
                        && Config.Lis2CFG.LIS_INTEGRATION_TYPE != Lis2CFG.LisIntegrationType.INVENTEC)
                    {
                        hisServiceReq = new HisServiceReqGet().GetById(hisServiceReq.ID);
                        string thoiGianLayMau = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(hisServiceReq.SAMPLE_TIME.HasValue ? hisServiceReq.SAMPLE_TIME.Value : 0);
                        string nguoiLayMau = hisServiceReq.SAMPLER_LOGINNAME + "-" + hisServiceReq.SAMPLER_USERNAME;
                        string thoiGianTiepNhanMau = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(hisServiceReq.RECEIVE_SAMPLE_TIME.HasValue ? hisServiceReq.RECEIVE_SAMPLE_TIME.Value : 0);
                        string nguoiTiepNhanMau = hisServiceReq.RECEIVE_SAMPLE_LOGINNAME + "-" + hisServiceReq.RECEIVE_SAMPLE_USERNAME;
                        HIS_TEST_SAMPLE_TYPE testSampleType = hisServiceReq.TEST_SAMPLE_TYPE_ID.HasValue ? new HisTestSampleTypeGet().GetById(hisServiceReq.TEST_SAMPLE_TYPE_ID.Value) : null;
                        string loaiMau = testSampleType != null ? testSampleType.TEST_SAMPLE_TYPE_NAME : "";

                        HisServiceReqLog.Run(hisServiceReq, LibraryEventLog.EventLog.Enum.HisServiceReq_CapNhatTrangThaiTiepNhanMau, tomTatLoaiXuLyXetNghiem, thoiGianLayMau, nguoiLayMau, thoiGianTiepNhanMau, nguoiTiepNhanMau, loaiMau);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            LogSystem.Debug("Ket qua 'cap nhat trang thai lay mau' tu he thong LIS: " + LogUtil.TraceData("result", result));
            return result;
        }

        internal bool UpdateBarcode(HisTestUpdateBarcodeTDO data)
        {
            LogSystem.Debug("Input 'cap nhat barcode' tu he thong LIS: " + LogUtil.TraceData("data", data));
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(data.ServiceReqCode);
                valid = valid && IsNotNullOrEmpty(data.Barcode);
                if (valid)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.INTRUCTION_TIME_FROM = data.TimeFrom;
                    filter.INTRUCTION_TIME_TO = data.TimeTo;
                    filter.TDL_PATIENT_CODE__EXACT = data.PatientCode;
                    filter.TREATMENT_CODE__EXACT = data.TreatmentCode;
                    filter.ASSIGN_TURN_CODE__EXACT = data.TurnCode;

                    //Lay thong tin HisTestServiceReq tuong ung voi barcode
                    if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
                    {
                        filter.BARCODE__EXACT = data.ServiceReqCode;
                    }
                    else
                    {
                        filter.SERVICE_REQ_CODE__EXACT = data.ServiceReqCode;
                    }

                    List<HIS_SERVICE_REQ> listServiceReq = new HisServiceReqGet().Get(filter);
                    HIS_SERVICE_REQ hisServiceReq = IsNotNullOrEmpty(listServiceReq) ? listServiceReq[0] : null;

                    if (hisServiceReq == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoDuLieuChiTiet);
                        throw new Exception("Ko tim thay HIS_SERVICE_REQ tuong ung voi ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    if (hisServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepCapNhatKhiChuaHoanThanh);
                        throw new Exception("HIS_SERVICE_REQ hoan thanh, ServiceReqCode: " + hisServiceReq.SERVICE_REQ_CODE);
                    }

                    if (hisServiceReq.IS_NO_EXECUTE.HasValue && hisServiceReq.IS_NO_EXECUTE.Value == Constant.IS_TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongThucHien);
                        throw new Exception("HIS_SERVICE_REQ khong thuc hien, ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(hisServiceReq.ID);
                    if (!IsNotNullOrEmpty(hisSereServs) || hisSereServs.Exists(o => o.IS_SPECIMEN == Constant.IS_TRUE) || hisServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_KhongCoDuLieuChiTietHoacDaDuocLayMau);
                        throw new Exception("HIS_SERVICE_REQ da lay mau, ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    //kiem tra trung barcode
                    HIS_SERVICE_REQ ServiceReqSampleBarcode = null;

                    HisServiceReqFilterQuery sampleBarcodeFilter = new HisServiceReqFilterQuery();
                    sampleBarcodeFilter.BARCODE__EXACT = data.Barcode;
                    if (HisServiceReqCFG.GENERATE_BARCODE_OPTION == HisServiceReqCFG.GenrateBarcodeOption.DAY_WITH_NUMBER)
                    {
                        sampleBarcodeFilter.INTRUCTION_DATE__EQUAL = Inventec.Common.DateTime.Get.StartDay();
                    }
                    List<HIS_SERVICE_REQ> listServiceReqSampleBarcode = new HisServiceReqGet().Get(sampleBarcodeFilter);
                    ServiceReqSampleBarcode = IsNotNullOrEmpty(listServiceReqSampleBarcode) ? listServiceReqSampleBarcode[0] : null;

                    if (IsNotNull(ServiceReqSampleBarcode))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_BarcodeDaDuocSuDung, data.Barcode);
                        throw new Exception("Barcode da trung, ServiceReqCode: " + data.ServiceReqCode + ". Barcode updte: " + data.Barcode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    //cap nhat barcode
                    string sql = string.Format("UPDATE HIS_SERVICE_REQ SET BARCODE = '{0}' WHERE ID = {1}", data.Barcode, hisServiceReq.ID);

                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat barcode cho HisServiceReq that bai");
                    }

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_SuaBarcode, this.LogData(hisServiceReq.BARCODE), this.LogData(data.Barcode))
                            .TreatmentCode(hisServiceReq.TDL_TREATMENT_CODE)
                            .ServiceReqCode(hisServiceReq.SERVICE_REQ_CODE)
                            .Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            LogSystem.Debug("Ket qua 'cap nhat barcode' tu he thong LIS: " + LogUtil.TraceData("result", result));
            return result;
        }

        internal bool ConfirmNoExcute(HisTestConfirmNoExcuteTDO data)
        {
            LogSystem.Debug("Input 'xac nhan trang thai khong thuc hien' tu he thong LIS: " + LogUtil.TraceData("data", data));
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(data.ServiceReqCode);
                valid = valid && IsNotNullOrEmpty(data.TestServiceTypeList);
                if (valid)
                {
                    HIS_SERVICE_REQ hisServiceReq = null;

                    //Lay thong tin HisTestServiceReq tuong ung voi barcode
                    if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
                    {
                        hisServiceReq = new HisServiceReqGet().GetByBarcode(data.ServiceReqCode);
                    }
                    else
                    {
                        hisServiceReq = new HisServiceReqGet().GetByCode(data.ServiceReqCode);
                    }

                    if (hisServiceReq == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay HIS_SERVICE_REQ tuong ung voi ServiceReqCode: " + data.ServiceReqCode + ". Luu y: cau hinh MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE duoc bat thi se lay HIS_SERVICE_REQ theo truong BARCODE");
                    }

                    //Lay du lieu HisSereServ tuong ung voi ServiceReq o tren
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(hisServiceReq.ID);
                    if (!IsNotNullOrEmpty(sereServs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay sereServs tuong ung voi SERVICE_REQ_ID: " + hisServiceReq.ID);
                    }

                    List<HIS_SERE_SERV> listToUpdate = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> beforeUpdate = new List<HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    foreach (HisTestServiceTypeTDO t in data.TestServiceTypeList)
                    {
                        List<HIS_SERE_SERV> existedList = sereServs.Where(o => o.TDL_SERVICE_CODE.Equals(t.TestServiceTypeCode)).ToList();
                        List<HIS_SERE_SERV> newList = Mapper.Map<List<HIS_SERE_SERV>>(existedList);
                        List<HIS_SERE_SERV> oldList = Mapper.Map<List<HIS_SERE_SERV>>(existedList);
                        short? confirmNoExcute = null;
                        if (t.ConfirmNoExcute)
                        {
                            confirmNoExcute = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        newList.ForEach(o => o.IS_CONFIRM_NO_EXCUTE = confirmNoExcute);
                        listToUpdate.AddRange(newList);
                        beforeUpdate.AddRange(oldList);
                    }

                    result = new HisSereServUpdate(param).UpdateList(listToUpdate, beforeUpdate, false);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            LogSystem.Debug("Ket qua 'xac nhan trang thai khong thuc hien' tu he thong LIS: " + LogUtil.TraceData("result", result));
            return result;
        }

        private string LogData(string data)
        {
            string barcode = LogCommonUtil.GetEventLogContent(EventLog.Enum.Barcode);
            return string.Format("{0}: {1}", barcode, data);
        }
    }
}
