using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class TemporaryBedProcessor : BusinessBase
    {
        internal TemporaryBedProcessor()
            : base()
        {
        }

        internal TemporaryBedProcessor(CommonParam param)
            : base(param)
        {
        }

        public void Run(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs, WorkPlaceSDO workPlace)
        {
            try
            {
                if (treatment != null && treatment.CLINICAL_IN_TIME.HasValue
                    && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                    && treatment.IS_OLD_TEMP_BED != Constant.IS_TRUE //chi ap dung voi cac ban ghi moi (cac ban ghi cu truoc day van ap dung co che nhu cu)
                    && HisSereServCFG.IS_USING_BED_TEMP)
                {
                    HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
                    filter.TREATMENT_ID = treatment.ID;
                    filter.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G;
                    filter.HAS_AMOUNT_TEMP = true;
                    filter.HAS_BED_LOG_ID = true;

                    List<V_HIS_SERE_SERV_16> sereServs = new HisSereServGet().GetView16(filter);

                    if (IsNotNullOrEmpty(sereServs))
                    {
                        long outDate = data.TreatmentFinishTime - data.TreatmentFinishTime % 1000000;

                        //Lay ra cac dich vu giuong do khoa ket thuc dieu tri chi dinh va chua co thoi gian ra giuong de thuc hien cap nhat thoi gian ra giuong
                        List<V_HIS_SERE_SERV_16> inDepartments = sereServs.Where(o => o.TDL_REQUEST_DEPARTMENT_ID == workPlace.DepartmentId && !o.FINISH_TIME.HasValue).ToList();

                        ////B1: cap nhat thoi gian ra khoi giuong cua giuong duoc chi dinh boi khoa ra vien
                        //Neu co thi thuc hien cap nhat thoi gian ra khoi giuong
                        if (IsNotNullOrEmpty(inDepartments))
                        {
                            List<long> ids = inDepartments.Select(o => o.BED_LOG_ID.Value).ToList();
                            string idStr = string.Join(",", ids);
                            string sqlUpdateBedLog = string.Format("UPDATE HIS_BED_LOG SET FINISH_TIME = {0} WHERE ID IN ({1})", data.TreatmentFinishTime, idStr);
                            if (!DAOWorker.SqlDAO.Execute(sqlUpdateBedLog))
                            {
                                throw new Exception("Cap nhat thoi gian ket thuc giuong that bai");
                            }

                            //Cap nhat thoi gian ra khoi giuong cho cac du lieu lay ve de tranh phai truy van lai vao CSDL
                            inDepartments.ForEach(o => o.FINISH_TIME = data.TreatmentFinishTime);
                        }

                        //Sap xep lai theo thoi gian y lenh
                        sereServs = sereServs.OrderBy(o => o.TDL_INTRUCTION_TIME).ToList();

                        ////B2: Duyet lai tat ca cac ban ghi chi dinh giuong theo tung ngay xem co ngay nao co tong so luong lon hon 1 hay khong (ko tinh ngay cuoi cung)
                        ////neu co ngay co tong so luong > 1 thi xu ly de cap nhat lai de khong vuot qua 1
                        var groups = sereServs.Where(o => o.TDL_INTRUCTION_DATE < outDate).GroupBy(o => o.TDL_INTRUCTION_DATE);

                        foreach(var g in groups)
                        {
                            decimal total = g.Sum(o => o.AMOUNT_TEMP.Value);

                            if (total > 1)
                            {
                                List<V_HIS_SERE_SERV_16> list = g.ToList();

                                //Neu ton tai so luong >= 1 thi cap nhat ve 1, cac chi dinh con lai cap nhat so luong ve 0
                                if (list.Exists(t => t.AMOUNT_TEMP >= 1))
                                {
                                    V_HIS_SERE_SERV_16 max = list.OrderByDescending(o => o.AMOUNT_TEMP).FirstOrDefault();
                                    max.AMOUNT_TEMP = 1;

                                    foreach (V_HIS_SERE_SERV_16 ss in list)
                                    {
                                        if (ss.ID != max.ID)
                                        {
                                            ss.AMOUNT_TEMP = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    //Neu ton tai it nhat 2 ban ghi co so luong = 0.5, thi lay ra gia cao nhat va thap nhat, cac ban ban ghi con lai cap nhat ve 0
                                    List<V_HIS_SERE_SERV_16> halfs = list.Where(o => o.AMOUNT_TEMP == 0.5m).ToList();
                                    if (halfs.Count >= 2)
                                    {
                                        V_HIS_SERE_SERV_16 minPrice = halfs.OrderBy(o => o.PRICE).FirstOrDefault();
                                        V_HIS_SERE_SERV_16 maxPrice = halfs.OrderByDescending(o => o.PRICE).FirstOrDefault();

                                        foreach (V_HIS_SERE_SERV_16 ss in list)
                                        {
                                            if (ss.ID != minPrice.ID && ss.ID != maxPrice.ID)
                                            {
                                                ss.AMOUNT_TEMP = 0;
                                            }
                                        }
                                    }
                                    //Neu khong thuoc cac truong hop tren thi bao loi chi dinh giuong de nguoi dung chu dong sua lai so luong
                                    else
                                    {
                                        List<string> codes = list.Select(o => o.TDL_SERVICE_REQ_CODE).Distinct().ToList();
                                        string codeStr = string.Join(",", codes);
                                        string date = Inventec.Common.DateTime.Convert.TimeNumberToDateString(g.Key);
                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DuLieuChiDinhGiuongTrongNgayKhongHopLe, date, codeStr);
                                        return;
                                    }
                                }
                            }

                        }

                        ////B3: Tinh so luong tam tinh cua giuong cua ngay cuoi cung ma khoa ket thuc dieu tri chi dinh
                        //// = so ngay dieu tri tinh theo cong thuc cua BHYT - tong so ngay giuong cua cac ngay truoc do
                        decimal treatmentDayCount = this.GetTreatmentDayCount(data.TreatmentFinishTime, treatment.CLINICAL_IN_TIME.Value, data.TreatmentResultId);
                        V_HIS_SERE_SERV_16 lastBed = sereServs.Where(o => o.TDL_INTRUCTION_DATE == outDate).LastOrDefault();
                        decimal totalBefore = sereServs.Where(o => o.ID != lastBed.ID).Sum(o => o.AMOUNT_TEMP ?? 0);
                        lastBed.AMOUNT_TEMP = treatmentDayCount >= totalBefore ? treatmentDayCount - totalBefore : 0;

                        ////B4: Cap nhat lai so luong theo so luong tam tinh, va cap nhat so luong tam tinh ve null
                        ////Dong thoi cap nhat lai du lieu trong existsSereServs de phuc vu tinh toan phia sau ma ko can cap nhat lai du lieu
                        List<string> sqls = new List<string>();
                        foreach (V_HIS_SERE_SERV_16 s in sereServs)
                        {
                            string sql = string.Format("UPDATE HIS_SERE_SERV SET AMOUNT = {0}, AMOUNT_TEMP = NULL, IS_TEMP_BED_PROCESSED = 1 WHERE ID = {1}", s.AMOUNT_TEMP.Value, s.ID);
                            sqls.Add(sql);
                            HIS_SERE_SERV exist = existsSereServs != null ? existsSereServs.Where(t => t.ID == s.ID).FirstOrDefault() : null;
                            if (exist != null)
                            {
                                exist.AMOUNT = s.AMOUNT_TEMP.Value;
                                exist.AMOUNT_TEMP = null;
                            }
                        }
                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Cap nhat lai so luong tam tinh cho dich vu giuong that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Tinh so ngay dieu tri dua vao ngay vao, ngay ra, ket qua dieu tri theo cong van cua BHYT
        /// </summary>
        /// <param name="outTime"></param>
        /// <param name="inTime"></param>
        /// <param name="treatmentResultId"></param>
        /// <returns></returns>
        private decimal GetTreatmentDayCount(long outTime, long inTime, long? treatmentResultId)
        {
            //Thoi gian ra vien, ko tinh don vi giay (vi BHYT cung ko tinh den don vi giay)
            DateTime outTmp = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(outTime - outTime % 100).Value;
            DateTime inTmp = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(inTime - inTime % 100).Value;

            double totalHours = (outTmp - inTmp).TotalHours;

            //Nho hon 4 tieng thi tinh la 0
            if (totalHours < BhytConstant.CLINICAL_TIME_FOR_EMERGENCY)
            {
                return 0;
            }
            //Lon hon 4 tieng va cung 1 ngay thi tinh la 1 
            else if (outTmp.Date == inTmp.Date)
            {
                return 1;
            }
            //con lai tinh theo ngay ra - ngay vao
            else
            {
                //Neu la nang hoac tu vong thi cong them 1 ngay
                if (treatmentResultId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__NANG 
                    || treatmentResultId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__CHET
                    || treatmentResultId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KTD)
                {
                    return (decimal) (outTmp.Date - inTmp.Date).TotalDays + 1;
                }
                else
                {
                    return (decimal) (outTmp.Date - inTmp.Date).TotalDays;
                }
            }
        }
    }
}
