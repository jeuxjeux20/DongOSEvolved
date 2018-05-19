using DongOSEvolved.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved
{
    public static class Extensions
    {
        public static List<Pixel> GetMissingElements(this List<Pixel> source, List<Pixel> pixels)
        {
            Kernel.PrintDebug("wow i got called. source length : " + source.Count + " pixels length : " + pixels.Count);
            var result = new List<Pixel>();
            foreach (var element in source)
            {
                if (!ContainsPixel(source, element))
                {
                    result.Add(element);
                }
            }
            return result;
        }
        private static bool ContainsPixel(List<Pixel> source, Pixel pixel)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var element = source[i];
                if (element.Coords == pixel.Coords)
                {
                    Kernel.PrintDebug("It is the same coords");
                    return true;
                }
                else
                {
                    Kernel.PrintDebug("Not same : " + element.Coords.X + ";" + element.Coords.Y + " | " + pixel.Coords.X + ";" + pixel.Coords.Y);
                }
            }
            return false;
        }
    }
}
