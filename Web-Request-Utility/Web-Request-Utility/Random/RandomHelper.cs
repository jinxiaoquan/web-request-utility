using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.Random
{
    public class RandomHelper
    {
        private static readonly System.Random Random = new System.Random();
        private static readonly long interval = 30 * 1000 * 10000; //cache ids for 30s

        private static readonly List<string> RandomIds = new List<string>();
        private static readonly long lastTime = 0;
        private static long _now = DateTime.Now.Ticks;

        public static string GenerateRandomId(int length)
        {
            var maxValue = 1;
            var strFormat = "";
            for (int i = 0; i < length; i++)
            {
                maxValue = maxValue * 10;
                strFormat = strFormat + "0";
            }

            var randomNum = Random.Next(maxValue).ToString(strFormat);
            lock (RandomIds)
            {
                try
                {
                    while (RandomIds.Contains(randomNum))
                    {
                        randomNum = Random.Next(1000000).ToString("000000");
                    }

                    if (lastTime - _now > interval && RandomIds.Count > 0)
                    {
                        RandomIds.Clear();
                        _now = DateTime.Now.Ticks;
                    }

                    RandomIds.Add(randomNum);
                }
                catch (System.Exception e)
                {
                    randomNum = Random.Next(1000000).ToString("000000");
                }
            }

            return randomNum;
        }
    }
}
