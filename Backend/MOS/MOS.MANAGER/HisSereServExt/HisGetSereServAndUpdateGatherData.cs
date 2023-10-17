using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisSurgRemuDetail;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExecuteRole;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEkip;

namespace MOS.MANAGER.HisSereServExt
{
    public class HisGetSereServAndUpdateGatherData : BusinessBase
    {
        private static bool IS_SENDING = false;

        internal HisGetSereServAndUpdateGatherData()
            : base()
        {
            this.Init();
        }

        internal HisGetSereServAndUpdateGatherData(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Debug("Tien trinh tu dong UpdateFeeAndGatherData dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                if (DateTime.Now.Hour > 0 || HisSereServExtCFG.DATE_AUTO_GATHER_DATA_SURG_DAY <= 0 || DateTime.Now.Day != HisSereServExtCFG.DATE_AUTO_GATHER_DATA_SURG_DAY)
                {
                    LogSystem.Debug("Khong phai thoi gian chay tien trinh");
                    return;
                }

                IS_SENDING = true;

                //Ngày thực hiện: END_TIME (HIS_SERE_SERV_EXT) có tháng = tháng hiện tại -1
                //Trạng thái y lệnh: Đã hoàn thành
                //Có kíp thực hiện
                //chưa chốt Lấy dữ liệu, tính chi phí
                HisSereServView8FilterQuery filter = new HisSereServView8FilterQuery();
                filter.END_TIME_FROM = Inventec.Common.DateTime.Get.StartMonth(DateTime.Now.AddMonths(-1));
                filter.END_TIME_TO = Inventec.Common.DateTime.Get.StartMonth(DateTime.Now);
                filter.IS_FEE = false;
                filter.IS_GATHER_DATA = false;
                filter.HAS_EKIP = true;
                filter.SERVICE_REQ_STT_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT };

                List<V_HIS_SERE_SERV_8> sereServ8 = new HisSereServGet().GetView8(filter);

                if (sereServ8 != null && sereServ8.Count > 0)
                {
                    foreach (var item in sereServ8)
                    {
                        CommonParam paramSetFee = new CommonParam();
                        HIS_SERE_SERV_EXT ext = null;

                        HisSereServExtIsFeeSDO sdo = new HisSereServExtIsFeeSDO();
                        sdo.IsFee = true;
                        sdo.SereServId = item.ID;

                        //goi ham xu ly tinh chi phi
                        //neu "co lay chi phi" thi bat buoc phai vao bao cao
                        bool checkSetFee = new HisSereServExtSetIsFee(paramSetFee).Run(sdo, ref ext);
                        if (!checkSetFee)
                        {
                            Inventec.Common.Logging.LogSystem.Error(string.Format("Khong thiet lap chi phi cho dich vu {0} thuoc y lenh {1}", item.TDL_SERVICE_CODE, item.SERVICE_REQ_CODE));
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => paramSetFee), paramSetFee));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                LogSystem.Error(ex);
            }
            finally
            {
                IS_SENDING = false;
            }
        }
    }
}
