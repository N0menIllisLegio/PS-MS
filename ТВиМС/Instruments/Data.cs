using System.Collections.Generic;

namespace Instruments
{
    public static class Data
    {
        public static Dictionary<byte, string> States = new Dictionary<byte, string>();

        public static byte[] IdDescreteRow;
        public static int[] DeathsIntervalRow;
        public static int[] DeathRateIntervalRow;
    }
}
