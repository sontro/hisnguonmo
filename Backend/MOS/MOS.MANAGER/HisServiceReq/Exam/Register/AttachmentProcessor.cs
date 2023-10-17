using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register
{
    /// <summary>
    /// Xu ly de tu dong bo sung cac dich vu duoc thiet lap dinh kem
    /// </summary>
    class AttachmentProcessor: BusinessBase
    {
        public static void AddAttachmentService(AssignServiceSDO data, long branchId, long requestDepartmentId)
        {
            try
            {
                if (data != null && data.ServiceReqDetails != null && data.ServiceReqDetails.Count > 0)
                {
                    List<long> serviceIds = data.ServiceReqDetails.Select(o => o.ServiceId).ToList();
                    
                    //Lay cac dich vu dinh kem tuong ung voi cac dich vu duoc dang ky va co phong xu ly
                    List<HIS_SERVICE_FOLLOW> availableFollows = HisServiceFollowCFG.DATA != null ? HisServiceFollowCFG.DATA
                        .Where(o => serviceIds.Contains(o.SERVICE_ID)
                            && HisServiceRoomCFG.DATA_VIEW.Exists(t => t.SERVICE_ID == o.FOLLOW_ID && t.IS_ACTIVE == Constant.IS_TRUE)).ToList() : null;

                    if (availableFollows != null && availableFollows.Count > 0)
                    {
                        long dummyId = 0;

                        List<ServiceReqDetailSDO> attachments = new List<ServiceReqDetailSDO>();

                        foreach (ServiceReqDetailSDO sdo in data.ServiceReqDetails)
                        {
                            sdo.DummyId = ++dummyId;//tao id "fake" de phuc vu xu ly nghiep vu gan "y lenh dinh kem" phia sau

                            List<long> allowPatientTypeIds = HisPatientTypeAllowCFG.DATA != null ?
                                HisPatientTypeAllowCFG.DATA
                                .Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.PATIENT_TYPE_ALLOW_ID != sdo.PatientTypeId)
                                .Select(o => o.PATIENT_TYPE_ALLOW_ID).ToList() : null;

                            //Lay cac dich vu dinh kem tuong ung voi dich vu kham dang duyet
                            List<HIS_SERVICE_FOLLOW> follows = availableFollows.Where(o => o.SERVICE_ID == sdo.ServiceId).ToList();

                            if (follows != null && follows.Count > 0)
                            {
                                foreach (HIS_SERVICE_FOLLOW f in follows)
                                {
                                    //Lay thong tin chinh sach gia tuong ung voi patient_type_id
                                    //Neu ko thi lay theo doi tuong duoc phep chuyen doi
                                    V_HIS_SERVICE_PATY sp = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, branchId, null, data.RequestRoomId, requestDepartmentId, data.InstructionTime, data.InstructionTime, f.FOLLOW_ID, sdo.PatientTypeId, null, null);

                                    long? acceptedPatientTypeId = null;
                                    if (sp != null)
                                    {
                                        acceptedPatientTypeId = sdo.PatientTypeId;
                                    }
                                    else if (allowPatientTypeIds != null && allowPatientTypeIds.Count > 0)
                                    {
                                        foreach (long allowPatientTypeId in allowPatientTypeIds)
                                        {
                                            V_HIS_SERVICE_PATY s = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, branchId, null, data.RequestRoomId, requestDepartmentId, data.InstructionTime, data.InstructionTime, f.FOLLOW_ID, allowPatientTypeId, null, null);
                                            if (s != null)
                                            {
                                                acceptedPatientTypeId = allowPatientTypeId;
                                                break;
                                            }
                                        }
                                    }

                                    if (acceptedPatientTypeId.HasValue)
                                    {
                                        ServiceReqDetailSDO service = new ServiceReqDetailSDO();
                                        service.DummyId = ++dummyId; //tao id "fake"
                                        service.AttachedDummyId = sdo.DummyId; //gan id "fake" cua dich vu kham
                                        service.Amount = f.AMOUNT;
                                        service.ServiceId = f.FOLLOW_ID;
                                        service.IsExpend = f.IS_EXPEND;
                                        service.PatientTypeId = acceptedPatientTypeId.Value;
                                        //Chi gan doi tuong phu thu khi bat cau hinh
                                        if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO)
                                        {
                                            service.PrimaryPatientTypeId = sdo.PrimaryPatientTypeId;
                                        }
                                        attachments.Add(service);
                                    }
                                }
                            }
                        }

                        if (attachments != null && attachments.Count > 0)
                        {
                            data.ServiceReqDetails.AddRange(attachments);

                            if (HisServiceReqCFG.KIOT_AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT)
                            {
                                data.IsNotRequireFee = null;
                            }
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
        /// Tu du lieu anh xa giua HIS_SERVICE_REQ va ServiceReqDetail de suy ra anh xa giua y lenh (HIS_SERVICE_REQ) chinh va ban ghi y lenh dinh kem
        /// </summary>
        /// <param name="serviceReqMapping"></param>
        /// <param name="serviceReqDetails"></param>
        /// <returns></returns>
        public static List<AttachmentServiceReqMapping> GetAttachmentMapping(List<ServiceReqServiceDetailMapping> serviceReqMapping, List<ServiceReqDetailSDO> serviceReqDetails)
        {
            try
            {
                if (serviceReqDetails != null && serviceReqDetails.Count > 0 && serviceReqMapping != null && serviceReqMapping.Count > 0)
                {
                    List<AttachmentServiceReqMapping> mappingList = new List<AttachmentServiceReqMapping>();
                    foreach (ServiceReqDetailSDO sdo in serviceReqDetails)
                    {
                        if (sdo.AttachedDummyId.HasValue && sdo.DummyId.HasValue)
                        {
                            ServiceReqServiceDetailMapping mappingChild = serviceReqMapping.Where(o => o.ServiceDetailDummyIds != null && o.ServiceDetailDummyIds.Contains(sdo.DummyId.Value)).FirstOrDefault();
                            ServiceReqServiceDetailMapping mappingParent = serviceReqMapping.Where(o => o.ServiceDetailDummyIds != null && o.ServiceDetailDummyIds.Contains(sdo.AttachedDummyId.Value)).FirstOrDefault();
                            if (mappingChild != null && mappingParent != null)
                            {
                                AttachmentServiceReqMapping mapping = new AttachmentServiceReqMapping();
                                mapping.Child = mappingChild.ServiceReq;
                                mapping.Parent = mappingParent.ServiceReq;
                                mappingList.Add(mapping);
                            }
                        }
                    }
                    return mappingList;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Tu du lieu anh xa truoc do, thuc hien cap nhat ATTACHED_ID trong HIS_SERVICE_REQ theo gia tri ID cua y lenh tuong ung
        /// </summary>
        /// <param name="mappingList"></param>
        public static void UpdateAttachedId(List<AttachmentServiceReqMapping> mappingList)
        {
            try
            {
                if (mappingList != null && mappingList.Count > 0)
                {
                    List<long> childIds = new List<long>();
                    List<string> sqls = new List<string>();
                    List<object> paramList = new List<object>();

                    long index = 0;

                    foreach (AttachmentServiceReqMapping mapping in mappingList)
                    {
                        if (mapping.Child != null && mapping.Child.ID > 0 && mapping.Parent != null && mapping.Parent.ID > 0 && !childIds.Contains(mapping.Child.ID))
                        {
                            string sql = string.Format("UPDATE HIS_SERVICE_REQ SET ATTACHED_ID = :param{0} WHERE ID = :param{1}", ++index, ++index);

                            //bo sung cau sql
                            sqls.Add(sql);

                            //bo sung param tuong ung
                            paramList.Add(mapping.Parent.ID);
                            paramList.Add(mapping.Child.ID);

                            //add vao d/s id duoc cap nhat de tranh sinh ra 2 cau sql update vao cung 1 ban ghi
                            childIds.Add(mapping.Child.ID);
                        }
                    }

                    //Neu co sql can update thi thuc hien update
                    if (sqls != null && sqls.Count > 0)
                    {
                        if (!DAOWorker.SqlDAO.Execute(sqls, paramList.ToArray()))
                        {
                            LogSystem.Error("Cap nhat y lenh dinh kem (ATTACHED_ID trong HIS_SERVICE_REQ) that bai");
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
