using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Pacs.PacsThread;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsVietsens
{
    class PacsVietsensProcessor : BusinessBase, IPacsProcessor
    {
        private const string FEMALE = "F";
        private const string MALE = "M";
        private const string OTHER = "O";
        private const int TIME_OUT = 6000;
        private const int SERVICE_REQ_CODE__LENGTH = 9;

        internal PacsVietsensProcessor(CommonParam param)
            : base(param)
        {

        }

        bool IPacsProcessor.SendOrder(PacsOrderData data, ref List<string> sqls)
        {
            bool result = false;
            try
            {

                V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);
                PacsAddress add = PacsCFG.PACS_ADDRESS.FirstOrDefault(o => o.RoomCode == room.ROOM_CODE && !String.IsNullOrWhiteSpace(o.Address));
                if (!IsNotNull(add))
                {
                    LogSystem.Warn(String.Format("Phong xu ly ma {0} khong cua cau hinh dia chi server Pác", room.ROOM_CODE));
                    return false;
                }

                bool? valid = null;
                if (IsNotNullOrEmpty(data.Inserts))
                {
                    foreach (HIS_SERE_SERV sereServ in data.Inserts)
                    {
                        if (this.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, room, add))
                        {
                            if (!valid.HasValue) valid = true;
                            sereServ.IS_SENT_EXT = Constant.IS_TRUE;
                            sqls.Add(String.Format("UPDATE HIS_SERE_SERV SET IS_SENT_EXT = {0} WHERE ID = {1} AND IS_SENT_EXT IS NULL", Constant.IS_TRUE, sereServ.ID));
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }

                if (IsNotNullOrEmpty(data.Deletes))
                {
                    foreach (HIS_SERE_SERV sereServ in data.Deletes)
                    {
                        if (this.SendDeleteOrder(data.Treatment, data.ServiceReq, sereServ, room, add))
                        {
                            if (!valid.HasValue) valid = true;
                            sereServ.IS_SENT_EXT = Constant.IS_TRUE;
                            sqls.Add(String.Format("UPDATE HIS_SERE_SERV SET IS_SENT_EXT = {0} WHERE ID = {1} AND IS_SENT_EXT IS NULL", Constant.IS_TRUE, sereServ.ID));
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }
                data.IsSuccess = valid;
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool SendNewOrder(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM room, PacsAddress add)
        {
            try
            {
                PacsStudyTDO pacsData = this.MakePacsTDO(treatment, serviceReq, room, sereServ);

                ApiConsumer pacsConsumer = new ApiConsumer(add.Address, MOS.UTILITY.Constant.APPLICATION_CODE);

                var ro = pacsConsumer.PostWithouApiParam<PacsResultTDO>("/api/His/GuiThongTinBenhNhan", pacsData, TIME_OUT);

                if (ro != null && ro.TrangThai)
                {
                    return true;
                }
                else
                {
                    LogSystem.Warn(String.Format("Gui Order dich vu cdha sang server Pacs Vietsens that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                        + LogUtil.TraceData("\n Output", ro));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private bool SendDeleteOrder(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM room, PacsAddress add)
        {
            try
            {
                PacsStudyTDO pacsData = this.MakePacsTDO(treatment, serviceReq, room, sereServ);

                ApiConsumer pacsConsumer = new ApiConsumer(add.Address, MOS.UTILITY.Constant.APPLICATION_CODE);

                var ro = pacsConsumer.PostWithouApiParam<PacsResultTDO>("/api/His/XoaThongTinBenhNhan", pacsData, TIME_OUT);

                if (ro != null && ro.TrangThai)
                {
                    return true;
                }
                else
                {
                    LogSystem.Warn(String.Format("Gui Cancel dich vu cdha sang server Pacs Vietsens that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                        + LogUtil.TraceData("\n Output", ro));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private PacsStudyTDO MakePacsTDO(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, V_HIS_ROOM room, HIS_SERE_SERV data)
        {
            PacsStudyTDO study = new PacsStudyTDO();
            study.ChanDoan = serviceReq.ICD_NAME;
            study.DiaChi = serviceReq.TDL_PATIENT_ADDRESS;
            if (treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE)
            {
                study.GioiTinh = FEMALE;
            }
            else if (treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE)
            {
                study.GioiTinh = MALE;
            }
            else
            {
                study.GioiTinh = OTHER;
            }
            study.LoaiDichVu = data.TDL_PACS_TYPE_CODE;
            study.MaDichVu = data.TDL_SERVICE_CODE;
            study.NamSinh = (serviceReq.TDL_PATIENT_DOB - serviceReq.TDL_PATIENT_DOB % 10000000000) / 10000000000;
            study.NgaySinh = ((serviceReq.TDL_PATIENT_DOB - serviceReq.TDL_PATIENT_DOB % 1000000) / 1000000).ToString();
            study.Ngay = (serviceReq.INTRUCTION_DATE / 1000000).ToString();

            HIS_DEPARTMENT reqDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.REQUEST_DEPARTMENT_ID).FirstOrDefault();
            study.NoiGui = reqDepartment.DEPARTMENT_NAME;
            study.SoHoSo = treatment.TREATMENT_CODE;
            study.SoPhieu = this.OrderCode(serviceReq.SERVICE_REQ_CODE, data.TDL_SERVICE_CODE);
            study.TenBacSiCD = serviceReq.REQUEST_USERNAME;
            study.TenBenhNhan = serviceReq.TDL_PATIENT_NAME;
            study.TenDichVu = data.TDL_SERVICE_NAME;
            study.TenDichVuHis = data.TDL_SERVICE_NAME;
            return study;
        }

        private string OrderCode(string serviceReqCode, string serviceCode)
        {
            string tmp = serviceReqCode.Substring(serviceReqCode.Length - SERVICE_REQ_CODE__LENGTH, SERVICE_REQ_CODE__LENGTH);
            return string.Format("{0}-{1}", tmp, serviceCode);
        }

        void IPacsProcessor.UpdateStatus(List<PacsOrderData> listData, List<string> sqls)
        {
            try
            {
                foreach (PacsOrderData item in listData)
                {
                    if (!item.IsSuccess.HasValue) continue;
                    if (item.IsSuccess.Value)
                    {
                        if (!item.ServiceReq.IS_SENT_EXT.HasValue)
                        {
                            sqls.Add(String.Format("UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1 WHERE ID = {0}", item.ServiceReq.ID));
                        }
                        else if (item.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE
                            && item.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE)
                        {
                            sqls.Add(String.Format("UPDATE HIS_SERVICE_REQ SET IS_UPDATED_EXT = NULL WHERE IS_UPDATED_EXT IS NOT NULL ID = {0}", item.ServiceReq.ID));
                        }
                    }
                }

                if (IsNotNullOrEmpty(sqls))
                {
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        bool IPacsProcessor.UpdatePatientInfo(HIS_PATIENT patient, ref List<string> messages)
        {
            bool result = true;
            try
            {
                if (IsNotNull(patient))
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.IS_NOT_SENT__OR__UPDATED = false; //lay cac y lenh chua gui sang PACS hoac co cap nhat
                    filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    filter.ALLOW_SEND_PACS = true;
                    filter.NOT_IN_SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN };
                    filter.TDL_PATIENT_ID = patient.ID;
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
                        List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;
                        if (serviceReqIds != null && serviceReqIds.Count > 0)
                        {
                            HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                            ssFilter.HAS_EXECUTE = true;
                            ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                            //voi sancy thi can lay ca cac du lieu da xoa (is_delete = 1)
                            ssFilter.IS_INCLUDE_DELETED = true;
                            sereServs = new HisSereServGet().Get(ssFilter);
                        }

                        List<PacsOrderData> orderData = GetDataProcessor.Prepare(sereServs, serviceReqs);
                        foreach (var data in orderData)
                        {
                            V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);
                            PacsAddress add = PacsCFG.PACS_ADDRESS.FirstOrDefault(o => o.RoomCode == room.ROOM_CODE && !String.IsNullOrWhiteSpace(o.Address));
                            if (!IsNotNull(add))
                            {
                                LogSystem.Warn(String.Format("Phong xu ly ma {0} khong cua cau hinh dia chi server Pác", room.ROOM_CODE));
                                return false;
                            }

                            if (IsNotNullOrEmpty(data.Availables))
                            {
                                foreach (HIS_SERE_SERV sereServ in data.Availables)
                                {
                                    if (sereServ.IS_DELETE == Constant.IS_TRUE)
                                    {
                                        if (this.SendDeleteOrder(data.Treatment, data.ServiceReq, sereServ, room, add))
                                        {
                                            Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (this.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, room, add))
                                        {
                                            Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
