using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.OutPres
{
    class HisImpMestMobaOutPresCheck : BusinessBase
    {
        internal HisImpMestMobaOutPresCheck()
            : base()
        {

        }

        internal HisImpMestMobaOutPresCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisImpMestMobaOutPresSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ServiceReqId <= 0) throw new ArgumentNullException("data.ServiceReqId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (!IsNotNullOrEmpty(data.MobaPresMedicines) && !IsNotNullOrEmpty(data.MobaPresMaterials)) throw new ArgumentNullException("data.MobaPresMedicines && data.MobaPresMaterials");

                if (IsNotNullOrEmpty(data.MobaPresMedicines))
                {
                    if (data.MobaPresMedicines.Exists(e => e.SereServId <= 0 || e.Amount <= 0))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu khong hop le." + LogUtil.TraceData("data.MobaPresMedicines", data.MobaPresMedicines));
                    }
                }

                if (IsNotNullOrEmpty(data.MobaPresMaterials))
                {
                    if (data.MobaPresMaterials.Exists(e => e.SereServId <= 0 || e.Amount <= 0))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Du lieu khong hop le." + LogUtil.TraceData("data.MobaPresMaterials", data.MobaPresMaterials));
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyServiceReqId(long serviceReqId, ref HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                serviceReq = new HisServiceReqGet().GetById(serviceReqId);
                if (serviceReq == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("serviceReqId invalid");
                }

                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("SERVICE_REQ_TYPE_ID invalid");
                }

                expMest = new HisExpMestGet().GetByServiceReqId(serviceReqId);
                if (expMest == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("serviceReqId invalid. Khong lay duoc expMest");
                }

                if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuChuaThucXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifySereServ(HisImpMestMobaOutPresSDO data, ref List<HIS_SERE_SERV> mobaSereServMedicines, ref List<HIS_SERE_SERV> mobaSereServMaterials)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(data.ServiceReqId);

                if (!IsNotNullOrEmpty(sereServs))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc HIS_SERE_SERV theo SERVICE_RE_ID: " + data.ServiceReqId);
                }
                List<HIS_SERE_SERV> medicines = null;
                List<HIS_SERE_SERV> materials = null;
                if (IsNotNullOrEmpty(data.MobaPresMedicines))
                {
                    medicines = sereServs.Where(o => o.MEDICINE_ID.HasValue && data.MobaPresMedicines.Exists(e => e.SereServId == o.ID)).ToList();
                    if (medicines == null || medicines.Count != data.MobaPresMedicines.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SereServMedicine != MobaPresMedicines");
                    }

                    if (medicines.Exists(e => e.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || e.AMOUNT <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SereServMedicine ton tai PATIENT_TYPE_ID =  bhyt hoac AMOUNT <= 0");
                    }
                }

                if (IsNotNullOrEmpty(data.MobaPresMaterials))
                {
                    materials = sereServs.Where(o => o.MATERIAL_ID.HasValue && data.MobaPresMaterials.Exists(e => e.SereServId == o.ID)).ToList();
                    if (materials == null || materials.Count != data.MobaPresMaterials.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SereServMaterial != MobaPresMaterials");
                    }

                    if (materials.Exists(e => e.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || e.AMOUNT <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SereServMaterial ton tai PATIENT_TYPE_ID <>  vien phi hoac AMOUNT <= 0");
                    }
                }
                mobaSereServMaterials = materials;
                mobaSereServMedicines = medicines;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool VerifyExpMest(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> allExpMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> allExpMestMaterials)
        {
            bool valid = true;
            try
            {
                allExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                allExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                allExpMestMaterials = allExpMestMaterials != null ? allExpMestMaterials.Where(o => o.IS_EXPORT == Constant.IS_TRUE).ToList() : null;
                allExpMestMedicines = allExpMestMedicines != null ? allExpMestMedicines.Where(o => o.IS_EXPORT == Constant.IS_TRUE).ToList() : null;

                if (!IsNotNullOrEmpty(allExpMestMaterials) && !IsNotNullOrEmpty(allExpMestMedicines))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xuat khong co thuoc vat tu nao da thuc xuat");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckValidRequestRoom(HIS_IMP_MEST data, HIS_EXP_MEST expMest, long requestRoomId)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(requestRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }

                data.REQ_ROOM_ID = workPlace.RoomId;
                data.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
