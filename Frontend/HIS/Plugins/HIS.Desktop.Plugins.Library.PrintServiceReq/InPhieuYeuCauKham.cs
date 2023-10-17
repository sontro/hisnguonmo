using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Print;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HIS.Desktop.Plugins.Library.PrintServiceReq
{
    class InPhieuYeuCauKham
    {
        public InPhieuYeuCauKham(string printTypeCode, string fileName, ADO.ChiDinhDichVuADO chiDinhDichVuADO,
            Dictionary<long, List<MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ>> dicServiceReqData,
            Dictionary<long, List<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV>> dicSereServData,
            bool printNow, ref bool result, long? roomId, MPS.ProcessorBase.PrintConfig.PreviewType? PreviewType,
            List<HisServiceReqMaxNumOrderSDO> ReqMaxNumOrderSDO, Action<int, Inventec.Common.FlexCelPrint.Ado.PrintMergeAdo> savedData,
            Action<string> cancelPrint,List<HIS_TRANS_REQ> TransReq,List<HIS_CONFIG> Configs, Action<Inventec.Common.SignLibrary.DTO.DocumentSignedUpdateIGSysResultDTO> DlgSendResultSigned)
        {
            try
            {
                var lstServieReq_1 = dicServiceReqData[IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH].ToList();
                if (lstServieReq_1 == null || lstServieReq_1.Count <= 0) return;

                var hisPatient = PrintGlobalStore.GetPatientById(chiDinhDichVuADO.treament.PATIENT_ID);
                var tranInReason = BackendDataWorker.Get<HIS_TRAN_PATI_REASON>().FirstOrDefault(o => o.ID == chiDinhDichVuADO.treament.TRANSFER_IN_REASON_ID);
                var inroom = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(o => o.ID == chiDinhDichVuADO.treament.IN_ROOM_ID);

                HIS_DHST _HIS_DHST = chiDinhDichVuADO._HIS_DHST;
                HIS_WORK_PLACE _WORK_PLACE = chiDinhDichVuADO._WORK_PLACE;

                MPS.Processor.Mps000001.PDO.Mps000001ADO ado = new MPS.Processor.Mps000001.PDO.Mps000001ADO();
                ado.firstExamRoomName = chiDinhDichVuADO.FirstExamRoomName;
                ado.ratio_text = chiDinhDichVuADO.Ratio * 100 + " %";
                ado.TRANSFER_IN_REASON_NAME = tranInReason != null ? tranInReason.TRAN_PATI_REASON_NAME : "";
                ado.IN_DEPARTMENT_NAME = inroom != null ? inroom.DEPARTMENT_NAME : "";
                ado.IN_ROOM_NAME = inroom != null ? inroom.ROOM_NAME : "";

                foreach (var serviceReq in lstServieReq_1)
                {
                    ado.CURRENT_EXECUTE_ROOM_NUM_ORDER = null;
                    if (ReqMaxNumOrderSDO != null && ReqMaxNumOrderSDO.Count > 0)
                    {
                        var roomSdo = ReqMaxNumOrderSDO.FirstOrDefault(o => o.EXECUTE_ROOM_ID == serviceReq.EXECUTE_ROOM_ID);
                        if (roomSdo != null)
                        {
                            ado.CURRENT_EXECUTE_ROOM_NUM_ORDER = roomSdo.MAX_NUM_ORDER;
                        }
                    }

                    List<V_HIS_SERE_SERV> sereServ = new List<V_HIS_SERE_SERV>();
                    if (dicSereServData != null && dicSereServData.ContainsKey(serviceReq.ID))
                    {
                        sereServ = dicSereServData[serviceReq.ID];
                    }

                    var listSereServ = new List<MPS.Processor.Mps000001.PDO.Mps000001_ListSereServs>();
                    if (sereServ != null && sereServ.Count > 0)
                    {
                        foreach (var item in sereServ)
                        {
                            listSereServ.Add(new MPS.Processor.Mps000001.PDO.Mps000001_ListSereServs(item));
                        }

                        List<long> _ssIds = sereServ.Select(p => p.SERVICE_ID).Distinct().ToList();
                        var dataSS = BackendDataWorker.Get<V_HIS_SERVICE>().Where(p => _ssIds.Contains(p.ID)).ToList();
                        if (dataSS != null && dataSS.Count > 0)
                        {
                            var _service = dataSS.FirstOrDefault(p => p.PARENT_ID != null);
                            if (_service != null)
                            {
                                var serviceN = ProcessDictionaryData.GetService(_service.PARENT_ID.Value);
                                ado.PARENT_NAME = serviceN != null ? serviceN.SERVICE_NAME : "";
                            }
                        }
                    }

                    V_HIS_ROOM room = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(o => o.ID == serviceReq.EXECUTE_ROOM_ID);
                    if (room != null)
                    {
                        ado.ExecuteRoom = room;
                    }
                    else
                    {
                        ado.ExecuteRoom = null;
                    }

                    List<V_HIS_TRANSACTION> transaction = null;
                    if (chiDinhDichVuADO.ListTransaction != null && chiDinhDichVuADO.ListTransaction.Count > 0)
                    {
                        List<long> transactionIds = new List<long>();
                        if (chiDinhDichVuADO.ListSereServBill != null && chiDinhDichVuADO.ListSereServBill.Count > 0)
                        {
                            transactionIds.AddRange(chiDinhDichVuADO.ListSereServBill.Where(o => listSereServ.Exists(e => e.ID == o.SERE_SERV_ID)).Select(s => s.BILL_ID));
                        }

                        if (chiDinhDichVuADO.ListSereServDeposit != null && chiDinhDichVuADO.ListSereServDeposit.Count > 0)
                        {
                            transactionIds.AddRange(chiDinhDichVuADO.ListSereServDeposit.Where(o => listSereServ.Exists(e => e.ID == o.SERE_SERV_ID)).Select(s => s.DEPOSIT_ID));
                        }

                        transaction = chiDinhDichVuADO.ListTransaction.Where(o => transactionIds.Contains(o.ID)).ToList();
                    }

                    MPS.Processor.Mps000001.PDO.Mps000001PDO mps000001RDO = new MPS.Processor.Mps000001.PDO.Mps000001PDO(
                        serviceReq,
                        chiDinhDichVuADO.patientTypeAlter,
                        hisPatient,
                        listSereServ,
                        chiDinhDichVuADO.treament,
                        ado,
                        _HIS_DHST,
                        _WORK_PLACE,
                        chiDinhDichVuADO.ListSereServDeposit,
                        chiDinhDichVuADO.ListSereServBill,
                        transaction,
                        chiDinhDichVuADO.Gate,
                        TransReq != null && TransReq.Count > 0 ?  TransReq.FirstOrDefault(o=>o.ID == serviceReq.TRANS_REQ_ID) : null,
                        Configs,
                        chiDinhDichVuADO.ListCard);

                    Print.PrintData(printTypeCode, fileName, mps000001RDO, printNow, ref result, roomId, false, PreviewType, listSereServ.Count, savedData, serviceReq.TREATMENT_CODE, DlgSendResultSigned);
                }
            }
            catch (Exception ex)
            {
                cancelPrint(printTypeCode);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
