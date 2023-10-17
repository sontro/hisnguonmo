using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisSereServ.Update.Package
{
    /// <summary>
    /// Goi de
    /// </summary>
    partial class HisSereServPackageBirth : BusinessBase
    {
        private long requestDepartmentId;
        private List<HIS_SERE_SERV> existedSereServs;

        internal HisSereServPackageBirth(CommonParam param, long requestDepartmentId, List<HIS_SERE_SERV> exists)
            : base(param)
        {
            this.requestDepartmentId = requestDepartmentId;
            this.existedSereServs = exists;
        }

        internal HisSereServPackageBirth(CommonParam param)
            : base(param)
        {

        }

        internal void Run(HIS_SERE_SERV sereServ, long? sereServParentId)
        {
            this.Run(new List<HIS_SERE_SERV>() { sereServ }, sereServParentId);
        }

        internal void Run(List<HIS_SERE_SERV> sereServs, long? sereServParentId)
        {
            try
            {
                HIS_SERE_SERV birthSereServ = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasBirth(sereServParentId, ref birthSereServ)
                        && this.IsInBirthPackage(birthSereServ, this.requestDepartmentId);

                if (birthSereServ != null && IsNotNullOrEmpty(sereServs))
                {
                    foreach (HIS_SERE_SERV ss in sereServs)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            ss.PARENT_ID = birthSereServ.ID;
                            ss.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (ss.PARENT_ID == birthSereServ.ID)
                        {
                            ss.IS_OUT_PARENT_FEE = Constant.IS_TRUE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Run(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, long? sereServParentId)
        {
            try
            {
                HIS_SERE_SERV birthSereServ = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasBirth(sereServParentId, ref birthSereServ)
                        && this.IsInBirthPackage(birthSereServ, this.requestDepartmentId);

                if (birthSereServ != null)
                {
                    foreach (HIS_EXP_MEST_MATERIAL material in expMestMaterials)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            material.SERE_SERV_PARENT_ID = birthSereServ.ID;
                            material.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (material.SERE_SERV_PARENT_ID == birthSereServ.ID)
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

        internal void Run(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, long? sereServParentId)
        {
            try
            {
                HIS_SERE_SERV birthSereServ = null;

                //Kiem tra xem y lenh dang xu ly co thuoc dien duoc gan vao goi PTTM hay ko
                bool isInPackage = this.HasBirth(sereServParentId, ref birthSereServ)
                        && this.IsInBirthPackage(birthSereServ, this.requestDepartmentId);

                if (birthSereServ != null)
                {
                    foreach (HIS_EXP_MEST_MEDICINE medicine in expMestMedicines)
                    {
                        //neu duoc phep vao goi --> thi gan sere_serv_parent_id
                        if (isInPackage)
                        {
                            medicine.SERE_SERV_PARENT_ID = birthSereServ.ID;
                            medicine.IS_OUT_PARENT_FEE = null;
                        }
                        //neu ko duoc phep vao goi, ma lai dang gan vao "parent_id"
                        //thi danh dau "chi phi ngoai goi" (IS_OUT_PARENT_FEE = 1)
                        else if (medicine.SERE_SERV_PARENT_ID == birthSereServ.ID)
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
        /// <param name="birthSereServ"></param>
        /// <returns></returns>
        private bool HasBirth(long? sereServParentId, ref HIS_SERE_SERV birthSereServ)
        {
            //Chi kiem tra khi co cau hinh nay (tranh hieu nang doi voi cac vien ko su dung nghiep vu nay)
            if (HisPackageCFG.PACKAGE_ID__DE.HasValue)
            {
                //Neu luc chi dinh co thong tin "dich vu chinh" (parent) thi kiem tra xem dich vu chinh nay
                //co thong tin goi PTTM ko
                if (sereServParentId.HasValue && IsNotNullOrEmpty(this.existedSereServs))
                {
                    var parentSereServ = this.existedSereServs.Where(o => o.ID == sereServParentId.Value).FirstOrDefault();
                    if (parentSereServ != null && parentSereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__DE)
                    {
                        birthSereServ = parentSereServ;
                        return true;
                    }
                }
                //Neu ko thi lay theo dich vu duoc chi dinh gan nhat thuoc goi PTTM
                else
                {
                    HIS_SERE_SERV parentSereServ = this.existedSereServs
                        .Where(o => o.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__DE
                            && o.IS_NO_EXECUTE == null && o.SERVICE_REQ_ID.HasValue)
                        .OrderByDescending(o => o.TDL_INTRUCTION_TIME).FirstOrDefault();
                    if (parentSereServ != null)
                    {
                        birthSereServ = parentSereServ;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Kiem tra xem y lenh dang xu ly co thuoc dien duoc vao goi đẻ hay ko
        /// </summary>
        /// <param name="birthSereServ"></param>
        /// <param name="instructionTime"></param>
        /// <param name="requestDepartmentId"></param>
        /// <returns></returns>
        private bool IsInBirthPackage(HIS_SERE_SERV birthSereServ, long requestDepartmentId)
        {
            try
            {
                return birthSereServ != null
                    && (birthSereServ.TDL_REQUEST_DEPARTMENT_ID == requestDepartmentId
                    || birthSereServ.TDL_EXECUTE_DEPARTMENT_ID == requestDepartmentId);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
