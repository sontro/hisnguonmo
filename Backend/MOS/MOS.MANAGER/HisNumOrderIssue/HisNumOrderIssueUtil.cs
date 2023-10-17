using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisNumOrderBlock;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisNumOrderIssue
{
    internal class HisNumOrderIssueUtil : BusinessBase
    {		
        internal HisNumOrderIssueUtil()
            : base()
        {

        }

        internal HisNumOrderIssueUtil(CommonParam param)
            : base(param)
        {

        }

        public void Issue(long issueTime, long roomId, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, ref long? resultNumOrder, ref long? resultNumOrderIssueId, ref string fromTime, ref string toTime)
        {
            //Trong truong hop cau hinh cap STT theo block thoi gian
            if (HisRoomCFG.BLOCK_NUM_ORDER_ROOM_IDs != null && HisRoomCFG.BLOCK_NUM_ORDER_ROOM_IDs.Contains(roomId))
            {
                long issueDate = Inventec.Common.DateTime.Get.StartDay(issueTime).Value;

                //Neu da co STT thi lay theo STT duoc cap
                //Ko can verify lai de toi uu hieu nang (trong DB da dat constraint)
                if (numOrder.HasValue && numOrderIssueId.HasValue)
                {
                    resultNumOrder = numOrder.Value;
                }
                //Neu chon block thi kiem tra xem block da co nguoi dat chua, chua dat thi tao "issue" de lay STT
                else if (numOrderBlockId.HasValue)
                {
                    HisNumOrderBlockOccupiedStatusFilter filter = new HisNumOrderBlockOccupiedStatusFilter();
                    filter.NUM_ORDER_BLOCK_ID = numOrderBlockId.Value;
                    filter.ISSUE_DATE = issueDate;
                    filter.ROOM_ID = roomId;
                    List<HisNumOrderBlockSDO> data = new HisNumOrderBlockGet().GetOccupiedStatus(filter);

                    if (!IsNotNullOrEmpty(data))
                    {
                        string date = Inventec.Common.DateTime.Convert.TimeNumberToDateString(issueDate);
                        V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == roomId).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisNumOrderIssue_KhungGioKhamKhongHopLe, room.ROOM_NAME, date);
                        throw new Exception("Ket thuc xu ly");
                    }

                    List<HisNumOrderBlockSDO> availables = data != null ? data.Where(o => o.IS_ISSUED != Constant.IS_TRUE).ToList() : null;
                    if (!IsNotNullOrEmpty(availables))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisNumOrderIssue_KhungGioKhamNayDaDuocDangKy);
                        throw new Exception("Ket thuc xu ly");
                    }

                    HIS_NUM_ORDER_ISSUE numOrderIssue = new HIS_NUM_ORDER_ISSUE();
                    numOrderIssue.NUM_ORDER_BLOCK_ID = numOrderBlockId.Value;
                    numOrderIssue.ISSUE_DATE = issueDate;

                    if (!new HisNumOrderIssueCreate().Create(numOrderIssue))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisNumOrderIssue_KhungGioKhamNayDaDuocDangKy);
                        throw new Exception("Ket thuc xu ly");
                    }

                    resultNumOrder = availables[0].NUM_ORDER;
                    resultNumOrderIssueId = numOrderIssue.ID;
                    fromTime = availables[0].FROM_TIME;
                    toTime = availables[0].TO_TIME;
                }
                //Neu ko chon thi tu dong lay block con trong nho nhat
                else
                {
                    string toTimeFrom = issueTime.ToString().Substring(8, 6);

                    HisNumOrderBlockOccupiedStatusFilter filter = new HisNumOrderBlockOccupiedStatusFilter();
                    filter.ISSUE_DATE = issueDate;
                    filter.ROOM_ID = roomId;
                    filter.TO_TIME__FROM = toTimeFrom;
                    List<HisNumOrderBlockSDO> sdo = new HisNumOrderBlockGet().GetOccupiedStatus(filter);
                    HisNumOrderBlockSDO available = sdo != null ? sdo.Where(o => o.IS_ISSUED != Constant.IS_TRUE).OrderBy(o => o.FROM_TIME).FirstOrDefault() : null;
                    if (available == null)
                    {
                        string date = Inventec.Common.DateTime.Convert.TimeNumberToDateString(issueDate);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisNumOrderIssue_NgayNayDaDuocDangKyHet, date);
                        throw new Exception("Ket thuc xu ly");
                    }

                    HIS_NUM_ORDER_ISSUE numOrderIssue = new HIS_NUM_ORDER_ISSUE();
                    numOrderIssue.NUM_ORDER_BLOCK_ID = available.NUM_ORDER_BLOCK_ID;
                    numOrderIssue.ISSUE_DATE = issueDate;

                    if (!new HisNumOrderIssueCreate().Create(numOrderIssue))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisNumOrderIssue_KhungGioKhamNayDaDuocDangKy);
                        throw new Exception("Ket thuc xu ly");
                    }

                    resultNumOrder = available.NUM_ORDER;
                    fromTime = available.FROM_TIME;
                    toTime = available.TO_TIME;
                    resultNumOrderIssueId = numOrderIssue.ID;
                }
            }
        }

        public void SetNumOrder(HIS_SERVICE_REQ serviceReq)
        {
            this.SetNumOrder(serviceReq, null, null, null);
        }

        public void SetNumOrder(HIS_SERVICE_REQ serviceReq, long? numOrderBlockId, long? numOrderIssueId, long? numOrder)
        {
            //Xu ly de lay STT kham
            long? resultNumOrder = null;
            long? resultNumOrderIssueId = null;
            string fromTime = null;
            string toTime = null;
            new HisNumOrderIssueUtil(param).Issue(serviceReq.INTRUCTION_TIME, serviceReq.EXECUTE_ROOM_ID, numOrderBlockId, numOrderIssueId, numOrder, ref resultNumOrder, ref resultNumOrderIssueId, ref fromTime, ref toTime);

            serviceReq.NUM_ORDER = resultNumOrder;
            serviceReq.NUM_ORDER_ISSUE_ID = resultNumOrderIssueId;
        }
    }
}
