using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.Package
{
    /// <summary>
    /// Nghiep vu goi phau thuat tham my (vien 108)
    /// </summary>
    partial class HisSereServPackagePttm : BusinessBase
    {
        private const long PTTM_DAY_IN_MINUTE = 1440;

        private long requestDepartmentId;
        private List<HIS_SERE_SERV> existedSereServs;

        internal HisSereServPackagePttm(CommonParam param, long requestDepartmentId, List<HIS_SERE_SERV> exists)
            : base(param)
        {
            this.requestDepartmentId = requestDepartmentId;
            this.existedSereServs = exists;
        }

        internal HisSereServPackagePttm(CommonParam param)
            : base(param)
        {

        }

        internal void Run(HIS_SERE_SERV sereServ, long? sereServParentId, long instructionTime)
        {
            this.Run(new List<HIS_SERE_SERV>() { sereServ }, sereServParentId, instructionTime);
        }

        internal void Run(List<HIS_SERE_SERV> sereServs, long? sereServParentId, long instructionTime)
        {
            try
            {
                HIS_SERE_SERV pttmSereServ = null;
                HIS_SERVICE_REQ pttmSr = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasPttm(sereServParentId, ref pttmSereServ)
                        && this.IsInPttmPackage(pttmSereServ, instructionTime, this.requestDepartmentId, ref pttmSr);

                if (pttmSereServ != null)
                {
                    foreach (HIS_SERE_SERV sereServ in sereServs)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            sereServ.PARENT_ID = pttmSereServ.ID;
                            sereServ.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (sereServ.PARENT_ID == pttmSereServ.ID)
                        {
                            sereServ.IS_OUT_PARENT_FEE = Constant.IS_TRUE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Run(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, long? sereServParentId, long instructionTime)
        {
            try
            {
                HIS_SERE_SERV pttmSereServ = null;
                HIS_SERVICE_REQ pttmSr = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasPttm(sereServParentId, ref pttmSereServ)
                        && this.IsInPttmPackage(pttmSereServ, instructionTime, this.requestDepartmentId, ref pttmSr);

                if (pttmSereServ != null)
                {
                    foreach (HIS_EXP_MEST_MATERIAL material in expMestMaterials)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            material.SERE_SERV_PARENT_ID = pttmSereServ.ID;
                            material.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (material.SERE_SERV_PARENT_ID == pttmSereServ.ID)
                        {
                            material.IS_OUT_PARENT_FEE = Constant.IS_TRUE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Run(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, long? sereServParentId, long instructionTime)
        {
            try
            {
                HIS_SERE_SERV pttmSereServ = null;
                HIS_SERVICE_REQ pttmSr = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasPttm(sereServParentId, ref pttmSereServ)
                        && this.IsInPttmPackage(pttmSereServ, instructionTime, this.requestDepartmentId, ref pttmSr);

                if (pttmSereServ != null)
                {
                    foreach (HIS_EXP_MEST_MEDICINE medicine in expMestMedicines)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            medicine.SERE_SERV_PARENT_ID = pttmSereServ.ID;
                            medicine.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (medicine.SERE_SERV_PARENT_ID == pttmSereServ.ID)
                        {
                            medicine.IS_OUT_PARENT_FEE = Constant.IS_TRUE;
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
        /// Kiem tra xem ho so co ton tai dich vu nao la goi PTTM ko
        /// </summary>
        /// <param name="sereServParentId"></param>
        /// <param name="pttmSereServ"></param>
        /// <returns></returns>
        private bool HasPttm(long? sereServParentId, ref HIS_SERE_SERV pttmSereServ)
        {
            //Chi kiem tra khi co cau hinh nay (tranh hieu nang doi voi cac vien ko su dung nghiep vu nay)
            if (HisPackageCFG.PACKAGE_ID__PTTM.HasValue)
            {
                //Neu luc chi dinh co thong tin "dich vu chinh" (parent) thi kiem tra xem dich vu chinh nay
                //co thong tin goi PTTM ko
                if (sereServParentId.HasValue && IsNotNullOrEmpty(this.existedSereServs))
                {
                    var parentSereServ = this.existedSereServs.Where(o => o.ID == sereServParentId.Value).FirstOrDefault();
                    if (parentSereServ != null && parentSereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__PTTM)
                    {
                        pttmSereServ = parentSereServ;
                        return true;
                    }
                }
                //Neu ko thi lay theo dich vu duoc chi dinh gan nhat thuoc goi PTTM
                else
                {
                    HIS_SERE_SERV parentSereServ = this.existedSereServs
                        .Where(o => o.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__PTTM
                        && o.IS_NO_EXECUTE == null && o.SERVICE_REQ_ID.HasValue)
                        .OrderByDescending(o => o.TDL_INTRUCTION_TIME).FirstOrDefault();
                    if (parentSereServ != null)
                    {
                        pttmSereServ = parentSereServ;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Kiem tra xem y lenh dang xu ly co thuoc dien duoc vao goi PTTM hay ko
        /// </summary>
        /// <param name="pttmSereServ"></param>
        /// <param name="instructionTime"></param>
        /// <param name="requestDepartmentId"></param>
        /// <param name="pttmServiceReq"></param>
        /// <param name="pttmDepartmentTran"></param>
        /// <returns></returns>
        private bool IsInPttmPackage(HIS_SERE_SERV pttmSereServ, long instructionTime, long requestDepartmentId, ref HIS_SERVICE_REQ pttmServiceReq)
        {
            try
            {
                //Chi xu ly neu khoa ra y lenh la khoa chi dinh PTTM
                if (pttmSereServ != null && pttmSereServ.TDL_REQUEST_DEPARTMENT_ID == requestDepartmentId)
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(pttmSereServ.SERVICE_REQ_ID.Value);

                    //Neu y lenh da bat dau thi moi xu ly tiep
                    if (serviceReq != null && serviceReq.START_TIME.HasValue)
                    {
                        //Neu bat dau xu ly vao 9h ngay 13 --> thi cac chi dinh tu 9h13 --> 23h59 ngay 14
                        //se duoc cho vao goi
                        DateTime startTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.START_TIME.Value).Value;
                        DateTime endTime = startTime.AddMinutes(PTTM_DAY_IN_MINUTE);
                        long endTimeNumber = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(endTime).Value;
                        if (instructionTime <= endTimeNumber && instructionTime >= serviceReq.START_TIME.Value)
                        {
                            pttmServiceReq = serviceReq;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
