partial class farmlight {
    class perlin {
        FastNoiseLite fnl;

        public perlin(int seed) {
            fnl = new FastNoiseLite(seed);
            fnl.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        }

        public perlin() => fnl = new FastNoiseLite();

        public float get(float x, float y) => (fnl.GetNoise(x,y)+1)/2f;
        public float get(float x, float y, float z) => (fnl.GetNoise(x,y,z)+1)/2f;
        public void setfreq(float freq = .01f) => fnl.SetFrequency(freq);
        public void setseed(int seed = 0) => fnl.SetSeed(seed);
    }
}