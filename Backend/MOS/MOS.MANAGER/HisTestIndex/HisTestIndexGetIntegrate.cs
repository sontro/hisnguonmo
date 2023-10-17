using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTestIndexRange;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestIndex
{
    partial class HisTestIndexGet : GetBase
    {
        /// <summary>
        /// Lay du lieu cho he thong tich hop ngoai (3rd-party)
        /// </summary>
        /// <returns></returns>
        internal List<HisTestIndexTDO> GetTDO()
        {
            HisTestIndexViewFilterQuery filter = new HisTestIndexViewFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            List<V_HIS_TEST_INDEX> hisTestIndexDTOs = this.GetView(filter);

            HisTestIndexRangeFilterQuery rangeFilter = new HisTestIndexRangeFilterQuery();
            rangeFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            List<HIS_TEST_INDEX_RANGE> hisTestIndexRangeDTOs = new HisTestIndexRangeGet().Get(rangeFilter);

            List<HisTestIndexTDO> result = null;
            if (IsNotNullOrEmpty(hisTestIndexDTOs))
            {
                result = new List<HisTestIndexTDO>();
                foreach (var testIndex in hisTestIndexDTOs)
                {
                    List<HIS_TEST_INDEX_RANGE> hisTestIndexRange = null;
                    if (IsNotNullOrEmpty(hisTestIndexRangeDTOs))
                    {
                        hisTestIndexRange = hisTestIndexRangeDTOs.Where(o => o.TEST_INDEX_ID == testIndex.ID).ToList();
                    }

                    HisTestIndexTDO dto = new HisTestIndexTDO
                    {
                        TestIndexCode = testIndex.TEST_INDEX_CODE,
                        TestIndexName = testIndex.TEST_INDEX_NAME,
                        TestServiceTypeCode = testIndex.SERVICE_CODE,
                        TestServiceTypeName = testIndex.SERVICE_NAME,
                        TestIndexUnitCode = testIndex.TEST_INDEX_UNIT_CODE,
                        TestIndexUnitName = testIndex.TEST_INDEX_UNIT_NAME,
                        TestIndexUnitSymbol = testIndex.TEST_INDEX_UNIT_SYMBOL
                    };

                    if (IsNotNullOrEmpty(hisTestIndexRange))
                    {
                        dto.TestIndexRange = hisTestIndexRange.Select(o =>
                        new HisTestIndexRangeTDO
                        {
                            AgeFrom = o.AGE_FROM,
                            AgeTo = o.AGE_TO,
                            AgeTypeId = o.AGE_TYPE_ID,
                            IsFemale = o.IS_FEMALE,
                            IsMale = o.IS_MALE,
                            MaxValue = o.MAX_VALUE,
                            MinValue = o.MIN_VALUE,
                            NormalValue = o.NORMAL_VALUE
                        }).ToList();
                    }

                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
