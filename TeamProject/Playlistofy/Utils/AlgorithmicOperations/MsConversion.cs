using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Utils.AlgorithmicOperations
{
    public class MsConversion
    {
        public static string ConvertMsToMinSec(double timeInMs)
        {
            string str;
            if (timeInMs < 0)
            {
                str = "00:00";
            }
            else
            {
                try
                {
                    TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInMs);
                    str = timeSpan.ToString(@"mm\:ss");
                }
                catch (OverflowException)
                {
                    str = "Track Length Too Long";
                }
                catch (ArgumentException)
                {
                    str = "Length Not in Correct Format";
                }
            }
            return str;
        }
    }
}
