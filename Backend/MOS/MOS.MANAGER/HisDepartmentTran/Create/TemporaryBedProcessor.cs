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

namespace MOS.MANAGER.HisDepartmentTran.Create
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

        public void Run(HisDepartmentTranSDO data, HIS_TREATMENT treatment, WorkPlaceSDO workPlace)
        {
            try
            {
                if (treatment != null && treatment.CLINICAL_IN_TIME.HasValue
                    && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                    && treatment.IS_OLD_TEMP_BED != Constant.IS_TRUE //chi ap dung voi cac ban ghi moi (cac ban ghi cu truoc day van ap dung co che nhu cu)
                    && HisSereServCFG.IS_USING_BED_TEMP)
                {
                    List<string> sqls = new List<string>();

                    string sqlTmp = "UPDATE HIS_BED_LOG B SET B.FINISH_TIME = {0} "
                        + " WHERE B.FINISH_TIME IS NULL AND EXISTS (SELECT 1 FROM HIS_SERVICE_REQ REQ WHERE REQ.BED_LOG_ID = B.ID AND REQ.REQUEST_DEPARTMENT_ID = {1})";

                    string sqlUpdateFinishTime = string.Format(sqlTmp, data.Time, workPlace.DepartmentId);
                    sqls.Add(sqlUpdateFinishTime);

                    //Lay ra cac ban ghi chi dinh giuong chua duoc xu ly cua khoa nay
                    HisSereServView16FilterQuery filter = new HisSereServView16FilterQuery();
                    filter.TREATMENT_ID = treatment.ID;
                    filter.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G;
                    filter.AMOUNT_TEMP = 0;
                    filter.HAS_AMOUNT_TEMP = true;
                    filter.HAS_BED_LOG_ID = true;
                    filter.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;

                    List<V_HIS_SERE_SERV_16> sereServs = new HisSereServGet().GetView16(filter);

                    if (IsNotNullOrEmpty(sereServs))
                    {
                        //Ngay nhap vien
                        long dateN = Inventec.Common.DateTime.Get.StartDay(treatment.CLINICAL_IN_TIME.Value).Value;
                        //Ngay chuyen khoa
                        long dateH = Inventec.Common.DateTime.Get.StartDay(data.Time).Value;

                        foreach (V_HIS_SERE_SERV_16 s in sereServs)
                        {
                            long finishDateInstructionTime = Inventec.Common.DateTime.Get.EndDay(s.INTRUCTION_TIME).Value;
                            
                            //So sanh cuoi ngay cua thoi gian chi dinh va thoi gian hien tai, thoi gian nao nho hon thi lay thoi gian do de tinh toan
                            //Chi lay den don vi phut, ko lay don vi giay (vì BHYT ko lay giay)
                            long tmp = finishDateInstructionTime > data.Time ? (data.Time - data.Time%100) : finishDateInstructionTime;
                            DateTime x = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(tmp).Value;
                            
                            //Ngay vao giuong
                            long dateG = Inventec.Common.DateTime.Get.StartDay(s.START_TIME.Value).Value;

                            long itmp = s.INTRUCTION_TIME - s.INTRUCTION_TIME % 100; //Chi lay den don vi phut, ko lay don vi giay (vì BHYT ko lay giay)
                            DateTime i = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(itmp).Value;

                            //Neu ngay nam giuong = ngay nhap vien
                            if (dateG == dateN && dateG == dateH)
                            {
                                if ((x - i).TotalHours < BhytConstant.CLINICAL_TIME_FOR_EMERGENCY)
                                {
                                    s.AMOUNT_TEMP = 0;
                                }
                                else
                                {
                                    s.AMOUNT_TEMP = 0.5m;
                                }
                            }
                            else if ((dateG == dateN && dateG < dateH) || (dateG > dateN))
                            {
                                //Neu khoa chi dinh ko phai la khoa cap cuu
                                if (s.IS_EMERGENCY != Constant.IS_TRUE)
                                {
                                    if ((x - i).TotalHours < 24)
                                    {
                                        s.AMOUNT_TEMP = 0.5m;
                                    }
                                    else
                                    {
                                        s.AMOUNT_TEMP = 1m;
                                    }
                                }
                                else
                                {
                                    if ((x - i).TotalHours < BhytConstant.CLINICAL_TIME_FOR_EMERGENCY)
                                    {
                                        s.AMOUNT_TEMP = 0;
                                    }
                                    else if ((x - i).TotalHours < 24 && (x - i).TotalHours >= BhytConstant.CLINICAL_TIME_FOR_EMERGENCY)
                                    {
                                        s.AMOUNT_TEMP = 0.5m;
                                    }
                                    else
                                    {
                                        s.AMOUNT_TEMP = 1;
                                    }
                                }
                            }

                            //Neu co thay doi so luong thi tao sql de cap nhat lai vao DB
                            if (s.AMOUNT_TEMP > 0)
                            {
                                string sql = string.Format("UPDATE HIS_SERE_SERV SET AMOUNT_TEMP = {0} WHERE ID = {1}", s.AMOUNT_TEMP.Value, s.ID);
                                sqls.Add(sql);
                            }
                        }
                    }
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            LogSystem.Error("Cap nhat lai thoi gian ket thuc giuong + so luong giuong tam that bai.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
