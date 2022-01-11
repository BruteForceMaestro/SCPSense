using System;

namespace SCPSense
{
    [Serializable]
    internal class TextConfig
    {
        public int size { get; set; } = 60;
        public string align { get; set; } = "left";
        public TextConfig(int SIZE)
        {
            size = SIZE;
        }
        public TextConfig(string ALIGN)
        {
            align = ALIGN;
        }
    }
}
