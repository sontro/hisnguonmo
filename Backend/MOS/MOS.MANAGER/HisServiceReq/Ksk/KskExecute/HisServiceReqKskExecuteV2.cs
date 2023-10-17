using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisKskDriverCar;
using MOS.MANAGER.HisKskGeneral;
using MOS.MANAGER.HisKskOverEighteen;
using MOS.MANAGER.HisKskOther;
using MOS.MANAGER.HisKskPeriodDriver;
using MOS.MANAGER.HisKskUnderEighteen;
using MOS.MANAGER.HisKskUneiVaty;
using MOS.MANAGER.HisPeriodDriverDity;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ksk.KskExecute
{
    class HisServiceReqKskExecuteV2 : BusinessBase
    {
        private HisServiceReqUpdateStart hisServiceReqUpdateStart;
        private HisDhstCreate hisDhstCreate;
        private HisDhstUpdate hisDhstUpdate;
        private HisKskGeneralUpdate hisKskGeneralUpdate;
        private HisKskGeneralCreate hisKskGeneralCreate;
        private HisKskDriverCarUpdate hisKskDriverCarUpdate;
        private HisKskDriverCarCreate hisKskDriverCarCreate;
        private HisKskOverEighteenUpdate hisKskOverEighteenUpdate;
        private HisKskOverEighteenCreate hisKskOverEighteenCreate;
        private HisKskPeriodDriverUpdate hisKskPeriodDriverUpdate;
        private HisKskPeriodDriverCreate hisKskPeriodDriverCreate;
        private HisKskUnderEighteenUpdate hisKskUnderEighteenUpdate;
        private HisKskUnderEighteenCreate hisKskUnderEighteenCreate;
        private HisPeriodDriverDityCreate hisPeriodDriverDityCreate;
        private HisKskUneiVatyCreate hisKskUneiVatyCreate;
        private HisKskOtherUpdate hisKskOtherUpdate;
        private HisKskOtherCreate hisKskOtherCreate;

        internal HisServiceReqKskExecuteV2()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqKskExecuteV2(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdateStart = new HisServiceReqUpdateStart(param);
            this.hisDhstCreate = new HisDhstCreate(param);
            this.hisDhstUpdate = new HisDhstUpdate(param);
            this.hisKskGeneralUpdate = new HisKskGeneralUpdate(param);
            this.hisKskGeneralCreate = new HisKskGeneralCreate(param);
            this.hisKskDriverCarUpdate = new HisKskDriverCarUpdate(param);
            this.hisKskDriverCarCreate = new HisKskDriverCarCreate(param);
            this.hisKskOtherUpdate = new HisKskOtherUpdate(param);
            this.hisKskOtherCreate = new HisKskOtherCreate(param);
            this.hisKskOverEighteenUpdate = new HisKskOverEighteenUpdate(param);
            this.hisKskOverEighteenCreate = new HisKskOverEighteenCreate(param);
            this.hisKskPeriodDriverUpdate = new HisKskPeriodDriverUpdate(param);
            this.hisKskPeriodDriverCreate = new HisKskPeriodDriverCreate(param);
            this.hisKskUnderEighteenUpdate = new HisKskUnderEighteenUpdate(param);
            this.hisKskUnderEighteenCreate = new HisKskUnderEighteenCreate(param);
            this.hisPeriodDriverDityCreate = new HisPeriodDriverDityCreate(param);
            this.hisKskUneiVatyCreate = new HisKskUneiVatyCreate(param);
        }

        public bool Run(HisServiceReqKskExecuteV2SDO data, ref KskExecuteResultV2SDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;

                HisServiceReqKskExecuteCheck checker = new HisServiceReqKskExecuteCheck(param);
                HisServiceReqCheck reqCheck = new HisServiceReqCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && reqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && checker.VerifyServiceReq(serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (valid)
                {
                    HIS_KSK_OTHER kskOther = null;
                    HIS_KSK_GENERAL kskGeneral = null;
                    HIS_KSK_DRIVER_CAR kskDriverCar = null;
                    HIS_KSK_OVER_EIGHTEEN kskOverEighteen = null;
                    HIS_KSK_PERIOD_DRIVER kskPeriodDriver = null;
                    HIS_KSK_UNDER_EIGHTEEN kskUnderEighteen = null;

                    if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        hisServiceReqUpdateStart.Start(serviceReq, false, ref serviceReqRaw, null, null);
                    }

                    ProcessKskOther(data.HisKskOther, serviceReq, ref kskOther);
                    ProcessKskDriverCar(data.HisKskDriverCar, serviceReq, ref kskDriverCar);
                    ProcessKskGeneral(data.KskGeneral, serviceReq, workPlace, ref kskGeneral);
                    ProcessKskOverEighteen(data.KskOverEighteen, serviceReq, workPlace, ref kskOverEighteen);
                    ProcessKskPeriodDriver(data.KskPeriodDriver, serviceReq, workPlace, ref kskPeriodDriver);
                    ProcessKskUnderEighteen(data.KskUnderEighteen, serviceReq, workPlace, ref kskUnderEighteen);

                    resultData = new KskExecuteResultV2SDO();
                    resultData.HisKskGeneral = kskGeneral;
                    resultData.HisKskDriverCar = kskDriverCar;
                    resultData.HisKskOverEighteen = kskOverEighteen;
                    resultData.HisKskPeriodDriver = kskPeriodDriver;
                    resultData.HisKskUnderEighteen = kskUnderEighteen;
                    resultData.HisKskOther = kskOther;
                    resultData.HisServiceReq = new HisServiceReqGet().GetViewById(serviceReq.ID);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessKskOther(HIS_KSK_OTHER data, HIS_SERVICE_REQ serviceReq, ref HIS_KSK_OTHER kskOther)
        {
            if (IsNotNull(data))
            {
                data.SERVICE_REQ_ID = serviceReq.ID;
                data.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                data.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;

                List<HIS_KSK_OTHER> kskOthers = new HisKskOtherGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskOthers))
                {
                    data.ID = kskOthers.First().ID;
                    if (!this.hisKskOtherUpdate.Update(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else if (!this.hisKskOtherCreate.Create(data))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                kskOther = data;
            }
        }

        private void ProcessKskDriverCar(HIS_KSK_DRIVER_CAR data, HIS_SERVICE_REQ serviceReq, ref HIS_KSK_DRIVER_CAR kskDriverCar)
        {
            if (IsNotNull(data))
            {
                data.SERVICE_REQ_ID = serviceReq.ID;
                data.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                data.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;

                List<HIS_KSK_DRIVER_CAR> kskDriverCars = new HisKskDriverCarGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskDriverCars))
                {
                    data.ID = kskDriverCars.First().ID;
                    if (!this.hisKskDriverCarUpdate.Update(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else if (!this.hisKskDriverCarCreate.Create(data))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                kskDriverCar = data;
            }
        }

        private void ProcessKskOverEighteen(KskOverEighteenV2SDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_OVER_EIGHTEEN kskOverEighteen)
        {
            if (IsNotNull(data) && IsNotNull(data.HisKskOverEighteen))
            {
                this.ProcessDhst(data.HisDhst, serviceReq.TREATMENT_ID, workPlace);

                data.HisKskOverEighteen.SERVICE_REQ_ID = serviceReq.ID;
                data.HisKskOverEighteen.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;
                data.HisKskOverEighteen.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;

                if (data.HisDhst != null)
                {
                    data.HisKskOverEighteen.DHST_ID = data.HisDhst.ID;
                }

                List<HIS_KSK_OVER_EIGHTEEN> kskOverEighteens = new HisKskOverEighteenGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskOverEighteens))
                {
                    data.HisKskOverEighteen.ID = kskOverEighteens.First().ID;
                    if (!this.hisKskOverEighteenUpdate.Update(data.HisKskOverEighteen))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else if (!this.hisKskOverEighteenCreate.Create(data.HisKskOverEighteen))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                kskOverEighteen = data.HisKskOverEighteen;
            }
        }

        private void ProcessKskPeriodDriver(KskPeriodDriverV2SDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_PERIOD_DRIVER kskPeriodDriver)
        {
            if (IsNotNull(data) && IsNotNull(data.HisKskPeriodDriver))
            {
                data.HisKskPeriodDriver.SERVICE_REQ_ID = serviceReq.ID;
                data.HisKskPeriodDriver.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;
                data.HisKskPeriodDriver.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;

                if (IsNotNullOrEmpty(data.HisPeriodDriverDitys))
                {
                    data.HisPeriodDriverDitys.ForEach(o => o.KSK_PERIOD_DRIVER_ID = 0);//reset ID (de phong client gui len ID dan den loi)
                    data.HisKskPeriodDriver.HIS_PERIOD_DRIVER_DITY = data.HisPeriodDriverDitys;
                }

                List<HIS_KSK_PERIOD_DRIVER> kskPeriodDrivers = new HisKskPeriodDriverGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskPeriodDrivers))
                {
                    data.HisKskPeriodDriver.ID = kskPeriodDrivers.First().ID;
                    data.HisKskPeriodDriver.HIS_PERIOD_DRIVER_DITY = null;

                    //Xoa du lieu cu
                    if (!new HisPeriodDriverDityTruncate().TruncateByKskPeriodDriverId(kskPeriodDrivers.First().ID))
                    {
                        throw new Exception("Xoa du lieu cu that bai");
                    }

                    if (!this.hisKskPeriodDriverUpdate.Update(data.HisKskPeriodDriver))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(data.HisPeriodDriverDitys))
                    {
                        data.HisPeriodDriverDitys.ForEach(o => o.KSK_PERIOD_DRIVER_ID = kskPeriodDrivers.First().ID);
                        if (!this.hisPeriodDriverDityCreate.CreateList(data.HisPeriodDriverDitys))
                        {
                            throw new Exception("hisPeriodDriverDityCreate Ket thuc nghiep vu");
                        }
                    }
                }
                else if (!this.hisKskPeriodDriverCreate.Create(data.HisKskPeriodDriver))
                {
                    throw new Exception("hisKskPeriodDriverCreate Ket thuc nghiep vu");
                }

                kskPeriodDriver = data.HisKskPeriodDriver;
            }
        }

        private void ProcessKskUnderEighteen(KskUnderEighteenV2SDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_UNDER_EIGHTEEN kskUnderEighteen)
        {
            if (IsNotNull(data) && IsNotNull(data.HisKskUnderEighteen))
            {
                this.ProcessDhst(data.HisDhst, serviceReq.TREATMENT_ID, workPlace);

                data.HisKskUnderEighteen.SERVICE_REQ_ID = serviceReq.ID;
                data.HisKskUnderEighteen.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                data.HisKskUnderEighteen.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;

                if (data.HisDhst != null)
                {
                    data.HisKskUnderEighteen.DHST_ID = data.HisDhst.ID;
                }

                if (IsNotNullOrEmpty(data.HisKskUneiVatys))
                {
                    data.HisKskUneiVatys.ForEach(o => o.KSK_UNDER_EIGHTEEN_ID = 0);//reset ID (de phong client gui len ID dan den loi)
                    data.HisKskUnderEighteen.HIS_KSK_UNEI_VATY = data.HisKskUneiVatys;
                }

                List<HIS_KSK_UNDER_EIGHTEEN> kskUnderEighteens = new HisKskUnderEighteenGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskUnderEighteens))
                {
                    data.HisKskUnderEighteen.ID = kskUnderEighteens.First().ID;
                    data.HisKskUnderEighteen.HIS_KSK_UNEI_VATY = null;

                    //Xoa du lieu cu
                    if (!new HisKskUneiVatyTruncate().TruncateByKskUnderEighteenId(kskUnderEighteens.First().ID))
                    {
                        throw new Exception("Xoa du lieu cu that bai");
                    }

                    if (!this.hisKskUnderEighteenUpdate.Update(data.HisKskUnderEighteen))
                    {
                        throw new Exception("hisKskUnderEighteenUpdate Ket thuc nghiep vu");
                    }


                    if (IsNotNullOrEmpty(data.HisKskUneiVatys))
                    {
                        data.HisKskUneiVatys.ForEach(o => o.KSK_UNDER_EIGHTEEN_ID = kskUnderEighteens.First().ID);
                        if (!this.hisKskUneiVatyCreate.CreateList(data.HisKskUneiVatys))
                        {
                            throw new Exception("hisKskUneiVatyCreate Ket thuc nghiep vu");
                        }
                    }
                }
                else if (!this.hisKskUnderEighteenCreate.Create(data.HisKskUnderEighteen))
                {
                    throw new Exception("hisKskUnderEighteenCreate Ket thuc nghiep vu");
                }

                kskUnderEighteen = data.HisKskUnderEighteen;
            }
        }

        private void ProcessKskGeneral(KskGeneralV2SDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_GENERAL kskGeneral)
        {
            if (IsNotNull(data) && IsNotNull(data.HisKskGeneral))
            {
                this.ProcessDhst(data.HisDhst, serviceReq.TREATMENT_ID, workPlace);

                data.HisKskGeneral.SERVICE_REQ_ID = serviceReq.ID;

                if (data.HisDhst != null)
                {
                    data.HisKskGeneral.DHST_ID = data.HisDhst.ID;
                }

                List<HIS_KSK_GENERAL> kskGenerals = new HisKskGeneralGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskGenerals))
                {
                    data.HisKskGeneral.ID = kskGenerals.First().ID;
                    if (!this.hisKskGeneralUpdate.Update(data.HisKskGeneral))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else if (!this.hisKskGeneralCreate.Create(data.HisKskGeneral))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                kskGeneral = data.HisKskGeneral;
            }
        }

        private void ProcessDhst(HIS_DHST data, long treatmentId, WorkPlaceSDO workPlace)
        {
            if (data != null)
            {
                data.TREATMENT_ID = treatmentId;
                data.EXECUTE_ROOM_ID = workPlace.RoomId;
                data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                if (data.ID > 0)
                {
                    if (!this.hisDhstUpdate.Update(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else
                {
                    if (!this.hisDhstCreate.Create(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void Rollback()
        {
            try
            {
                hisDhstCreate.RollbackData();
                hisDhstUpdate.RollbackData();
                hisKskGeneralUpdate.RollbackData();
                hisKskGeneralCreate.RollbackData();
                hisKskDriverCarUpdate.RollbackData();
                hisKskDriverCarCreate.RollbackData();
                hisKskOverEighteenUpdate.RollbackData();
                hisKskOverEighteenCreate.RollbackData();
                hisKskPeriodDriverUpdate.RollbackData();
                hisKskPeriodDriverCreate.RollbackData();
                hisKskUnderEighteenUpdate.RollbackData();
                hisKskUnderEighteenCreate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
