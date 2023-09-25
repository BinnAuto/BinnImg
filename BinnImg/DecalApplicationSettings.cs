namespace BinnImg
{
    public class DecalApplicationSettings
    {
        public (int, int) Coordinates { get; set; } = (0, 0);
        public (int, int) Size { get; set; } = (0, 0);
        public float Rotation { get; set; } = 0;
        public bool MirrorHorizontally { get; set; } = false;
        public bool MirrorVertically { get; set; } = false;
    }
}
