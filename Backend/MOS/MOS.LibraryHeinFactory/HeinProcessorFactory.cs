using MOS.LibraryHein.Aia;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Factory
{
    internal class HeinProcessorFactory
    {
        internal static IHeinProcessor GetHeinProcessor(string libCode)
        {
            IHeinProcessor result = null;
            if (!string.IsNullOrWhiteSpace(libCode))
            {
                switch (libCode)
                {
                    case HeinLibCodeData.BHYT:
                        result = new BhytHeinProcessor();
                        break;
                    case HeinLibCodeData.AIA:
                        result = new AiaHeinProcessor();
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
