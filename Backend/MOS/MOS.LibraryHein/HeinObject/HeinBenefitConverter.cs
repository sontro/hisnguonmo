using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Bhyt.HeinObject
{
    class HeinBenefitConverter
    {
        private static readonly Dictionary<string, string> HEIN_OBJECT_BENEFIT_MAPPER = new Dictionary<string, string>()
        {
            {"CA3","CA5"},
            {"BT4","BT2"},
            {"KC7","KC2"},
            {"HT5","HT3"},
            {"DN7","DN4"},
            {"CH7","CH4"},
            {"TK7","TK4"},
            {"XK7","XK4"},
            {"NO7","NO4"},
            {"TN7","TN4"},
            {"MS7","MS4"},
            {"TQ7","TQ4"},
            {"TY7","TY4"},
            {"LS7","LS4"},
            {"GD7","GD4"},
            {"TL7","GD4"},
            {"HN4","HN2"},
            {"CB7","CB2"},
            {"TC7","TC3"},
            {"CN6","CN3"},
            {"HX7","HX4"},
            {"NN7","NN4"},
            {"HC7","HC4"},
            {"TB7","TB4"},
            {"XB7","XB4"},
            {"XN7","XN4"},
            {"HD7","HD4"},
            {"TA7","TA4"},
            {"HG7","HG4"},
            {"HS7","HS4"},
            {"XV7","GD4"},
        };

        public static string Convert(string heinCardNumber)
        {
            if (!string.IsNullOrWhiteSpace(heinCardNumber))
            {
                var keys = HEIN_OBJECT_BENEFIT_MAPPER.Keys;
                foreach (string k in keys)
                {
                    int pos = heinCardNumber.IndexOf(k);
                    if (pos == 0)
                    {
                        return heinCardNumber.Substring(0, pos) + HEIN_OBJECT_BENEFIT_MAPPER[k] + heinCardNumber.Substring(pos + k.Length);
                    }
                }
            }
            return heinCardNumber;
        }
    }
}
