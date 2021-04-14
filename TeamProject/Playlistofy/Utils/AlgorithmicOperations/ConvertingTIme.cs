using System;

namespace Playlistofy.Models
{
    public class ConvertingTIme
    {
        public ConvertingTIme()
        {
        }

        public double Milliseconds_ToMinutes(int milliseconds)
        {
            double seconds = milliseconds / 1000;
            double minutes = Math.Round((seconds / 60), 2);
            return minutes;
        }
    }
}
