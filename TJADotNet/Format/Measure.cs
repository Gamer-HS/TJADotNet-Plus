namespace TJADotNet.Format
{
    public class Measure
    {
        public Measure(double part, double beat)
        {
            Part = part;
            Beat = beat;
        }
        /// <summary>
        /// 分子。
        /// </summary>
        public double Part { get; set; }
        /// <summary>
        /// 分母。
        /// </summary>
        public double Beat { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Part, Beat);
        }

        public double GetRate()
        {
            return 240 * (Part / Beat);
        }
    }
}
