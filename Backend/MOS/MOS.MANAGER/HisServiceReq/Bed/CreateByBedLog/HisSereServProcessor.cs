using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog
{
    class HisSereServProcessor: BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisSereServPackage37 hisSereServPackage37;
        private HisSereServPackageBirth hisSereServPackageBirth;
        private HisSereServPackagePttm hisSereServPackagePttm;

        internal HisSereServProcessor()
            : base()
        {
            this.Init();
        }

        internal HisSereServProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
        }

        internal void Run(HIS_TREATMENT treatment, Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> dicData, WorkPlaceSDO workPlace, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            if (dicData != null && dicData.Count > 0)
            {
                List<HIS_SERE_SERV> listSereServs = new HisSereServGet().GetHasExecuteByTreatmentId(treatment.ID);

                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                this.hisSereServPackage37 = new HisSereServPackage37(param, treatment.ID, workPlace.RoomId, workPlace.DepartmentId, listSereServs);
                this.hisSereServPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, listSereServs);
                this.hisSereServPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, listSereServs);

                List<HIS_SERVICE_REQ> serviceReqs = dicData.Keys.ToList();
                List<HIS_SERE_SERV> toInserts = new List<HIS_SERE_SERV>();

                foreach (HIS_SERVICE_REQ sr in serviceReqs)
                {
                    List<HIS_SERE_SERV> ss = dicData[sr];
                    if (ss != null && ss.Count > 0)
                    {
                        foreach (HIS_SERE_SERV sereServ in ss)
                        {
                            sereServ.SERVICE_REQ_ID = sr.ID;
                            //sereServ.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;
                            HisSereServUtil.SetTdl(sereServ, sr);
                            if (!priceAdder.AddPrice(sereServ, sereServ.TDL_INTRUCTION_TIME, sereServ.TDL_EXECUTE_BRANCH_ID, sereServ.TDL_REQUEST_ROOM_ID, sereServ.TDL_REQUEST_DEPARTMENT_ID, sereServ.TDL_EXECUTE_ROOM_ID))
                            {
                                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                            }
                        }
                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        this.hisSereServPackage37.Apply3Day7Day(ss, sr.INTRUCTION_TIME);
                        //Xu ly de ap dung goi de
                        this.hisSereServPackageBirth.Run(ss, ss[0].PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        this.hisSereServPackagePttm.Run(ss, ss[0].PARENT_ID, sr.INTRUCTION_TIME);

                        toInserts.AddRange(ss);
                    }
                }


                //Insert thong tin sere_serv vao CSDL
                if (!this.hisSereServCreate.CreateList(toInserts, serviceReqs, false))
                {
                    throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                }

                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);
                //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang
                if (!this.hisSereServUpdateHein.UpdateDb())
                {
                    throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisSereServCreate.RollbackData();
        }
    }
}
