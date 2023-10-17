using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccountBook;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCaroAccountBook
{
    class HisCaroAccountBookCopyByAccountBook : BusinessBase
    {
        internal HisCaroAccountBookCopyByAccountBook()
            : base()
        {

        }

        internal HisCaroAccountBookCopyByAccountBook(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisCaroAcboCopyByAccountBookSDO data, ref List<HIS_CARO_ACCOUNT_BOOK> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyAccountBookId);
                valid = valid && IsGreaterThanZero(data.PasteAccountBookId);
                if (valid)
                {
                    List<HIS_CARO_ACCOUNT_BOOK> newCaroAccountBooks = new List<HIS_CARO_ACCOUNT_BOOK>();

                    List<HIS_CARO_ACCOUNT_BOOK> copyCaroAccountBooks = DAOWorker.SqlDAO.GetSql<HIS_CARO_ACCOUNT_BOOK>("SELECT * FROM HIS_CARO_ACCOUNT_BOOK WHERE ACCOUNT_BOOK_ID = :param1", data.CopyAccountBookId);
                    List<HIS_CARO_ACCOUNT_BOOK> pasteCaroAccountBooks = DAOWorker.SqlDAO.GetSql<HIS_CARO_ACCOUNT_BOOK>("SELECT * FROM HIS_CARO_ACCOUNT_BOOK WHERE ACCOUNT_BOOK_ID = :param1", data.PasteAccountBookId);
                    if (!IsNotNullOrEmpty(copyCaroAccountBooks))
                    {
                        HIS_ACCOUNT_BOOK acc = new HisAccountBookGet().GetById(data.CopyAccountBookId);
                        string name = acc != null ? acc.ACCOUNT_BOOK_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisAccountBook_SoBienLaiChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyCaroAccountBooks");
                    }

                    foreach (HIS_CARO_ACCOUNT_BOOK copyData in copyCaroAccountBooks)
                    {
                        HIS_CARO_ACCOUNT_BOOK mestMaty = pasteCaroAccountBooks != null ? pasteCaroAccountBooks.FirstOrDefault(o => o.CASHIER_ROOM_ID == copyData.CASHIER_ROOM_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_CARO_ACCOUNT_BOOK();
                            mestMaty.ACCOUNT_BOOK_ID = data.PasteAccountBookId;
                            mestMaty.CASHIER_ROOM_ID = copyData.CASHIER_ROOM_ID;
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
