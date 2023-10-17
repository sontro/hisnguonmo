using MOS.UTILITY;
using MOS.TDO;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;

using Inventec.Core;
using Inventec.Common.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisLabconn
{
    class LisLabconnProcessor : BusinessBase, ILisProcessor
    {
        internal LisLabconnProcessor()
            : base()
        {
        }

        internal LisLabconnProcessor(CommonParam param)
            : base(param)
        {
        }

        public bool RequestOrder(OrderData data, ref List<string> messages)
        {
            return true;
        }


        public bool DeleteOrder(OrderData data, ref List<string> messages)
        {
            return true;
        }

        public bool UpdatePatientInfo(HIS_PATIENT patient, ref List<string> messages)
        {
            bool result = false;
            try
            {
                LogSystem.Info("Batdau goi api gui thong tin hanh chinh");
                if (IsNotNull(patient))
                {
                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    List<long> executeRoomIds = DataPrepare.GetExecuteRoomId();
                    LogSystem.Info(LogUtil.TraceData("Data executeRoomIds query by exe_room_code send to Labconn:", executeRoomIds));
                    if (IsNotNullOrEmpty(executeRoomIds))
                    {
                        HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                        filter.TDL_PATIENT_ID = patient.ID;
                        filter.EXECUTE_ROOM_IDs = executeRoomIds;
                        filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                        filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN;
                        filter.IS_NOT_SENT__OR__UPDATED = false;

                        serviceReqs = new HisServiceReqGet().Get(filter);
                    }

                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        var data = serviceReqs.GroupBy(o => o.ASSIGN_TURN_CODE);
                        LogSystem.Info(LogUtil.TraceData("data after group by TDL_PATIENT_CODE ASSIGN_TURN_CODE:", data));
                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                UpdatePatientInfoTDO tdo = new UpdatePatientInfoTDO();
                                tdo.MaHoSo = item.First().ASSIGN_TURN_CODE;
                                tdo.NgayChiDinh = (item.First().INTRUCTION_DATE / 1000000).ToString();
                                tdo.SoBH = item.First().TDL_HEIN_CARD_NUMBER;
                                tdo.MaBN = item.First().TDL_PATIENT_CODE;
                                tdo.HoTen = item.First().TDL_PATIENT_NAME;
                                tdo.NamSinh = long.Parse(item.First().TDL_PATIENT_DOB.ToString().Substring(0, 4));
                                if (item.First().TDL_PATIENT_GENDER_ID.HasValue && item.First().TDL_PATIENT_GENDER_ID.Value == 1)
                                {
                                    tdo.GioiTinh = "F";
                                }
                                else if (item.First().TDL_PATIENT_GENDER_ID.HasValue && item.First().TDL_PATIENT_GENDER_ID.Value == 2)
                                {
                                    tdo.GioiTinh = "M";
                                }
                                else
                                {
                                    tdo.GioiTinh = "O";
                                }
                                tdo.DiaChi = item.First().TDL_PATIENT_ADDRESS;
                                tdo.ChanDoan = item.First().ICD_NAME;
                                tdo.CapCuu = item.First().IS_EMERGENCY == Constant.IS_TRUE ? true : false;

                                LogSystem.Info(LogUtil.TraceData("Data tdo send to Labconn:", tdo));
                                V_HIS_EXECUTE_ROOM executeRooom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == item.First().EXECUTE_ROOM_ID);
                                var urlAddress = executeRooom != null ? LisCFG.LIS_ADDRESSES.FirstOrDefault(o => o.RoomCode == executeRooom.EXECUTE_ROOM_CODE) : null;
                                LogSystem.Info(LogUtil.TraceData("Data url server send to Labconn:", urlAddress));
                                if (urlAddress != null && !string.IsNullOrWhiteSpace(urlAddress.Url))
                                {
                                    if (new SendDataToLabconnProcessor().SendPatiInfoToLabconn(tdo, urlAddress.Url))
                                    {
                                        LogSystem.Info("Gui sang labconn thanh cong");
                                        result = true;
                                    }
                                }
                                else
                                {
                                    LogSystem.Error("Gui yeu cau sang LisLabconn khong lay duoc thong tin dia chi server");
                                }
                            }
                        }
                    }
                }
                LogSystem.Info("Kethuc goi api gui thong tin hanh chinh");
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
