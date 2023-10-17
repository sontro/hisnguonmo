using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCaroAccountBook
{
    class HisCaroAccountBookCopyByCashierRoom : BusinessBase
    {
        internal HisCaroAccountBookCopyByCashierRoom()
            : base()
        {

        }

        internal HisCaroAccountBookCopyByCashierRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisCaroAcboCopyByCashierRoomSDO data, ref List<HIS_CARO_ACCOUNT_BOOK> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyCashierRoomId);
                valid = valid && IsGreaterThanZero(data.PasteCashierRoomId);
                if (valid)
                {
                    List<HIS_CARO_ACCOUNT_BOOK> newCaroAccountBooks = new List<HIS_CARO_ACCOUNT_BOOK>();

                    List<HIS_CARO_ACCOUNT_BOOK> copyCaroAccountBooks = DAOWorker.SqlDAO.GetSql<HIS_CARO_ACCOUNT_BOOK>("SELECT * FROM HIS_CARO_ACCOUNT_BOOK WHERE CASHIER_ROOM_ID = :param1", data.CopyCashierRoomId);
                    List<HIS_CARO_ACCOUNT_BOOK> pasteCaroAccountBooks = DAOWorker.SqlDAO.GetSql<HIS_CARO_ACCOUNT_BOOK>("SELECT * FROM HIS_CARO_ACCOUNT_BOOK WHERE CASHIER_ROOM_ID = :param1", data.PasteCashierRoomId);
                    if (!IsNotNullOrEmpty(copyCaroAccountBooks))
                    {
                        V_HIS_CASHIER_ROOM stock = Config.HisCashierRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyCashierRoomId);
                        string name = stock != null ? stock.CASHIER_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyCaroAccountBooks");
                    }

                    foreach (HIS_CARO_ACCOUNT_BOOK copyData in copyCaroAccountBooks)
                    {
                        HIS_CARO_ACCOUNT_BOOK mestMaty = pasteCaroAccountBooks != null ? pasteCaroAccountBooks.FirstOrDefault(o => o.ACCOUNT_BOOK_ID == copyData.ACCOUNT_BOOK_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_CARO_ACCOUNT_BOOK();
                            mestMaty.CASHIER_ROOM_ID = data.PasteCashierRoomId;
                            mestMaty.ACCOUNT_BOOK_ID = copyData.ACCOUNT_BOOK_ID;
                            newCaroAccountBooks.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newCaroAccountBooks))
                    {
                        if (!DAOWorker.HisCaroAccountBookDAO.CreateList(newCaroAccountBooks))
                        {
                            throw new Exception("Khong tao duoc HIS_CARO_ACCOUNT_BOOK");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_CARO_ACCOUNT_BOOK>();
                    if (IsNotNullOrEmpty(newCaroAccountBooks))
                    {
                        resultData.AddRange(newCaroAccountBooks);
                    }
                    if (IsNotNullOrEmpty(pasteCaroAccountBooks))
                    {
                        resultData.AddRange(pasteCaroAccountBooks);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }
    }
}
