partial class farmlight {
    static Random r = new Random();

    static float floor(float x) => MathF.Floor(x);
    static int floorI(float x) => (int)MathF.Floor(x);
    static float cube(float x) => x * x * x;
    static int cube(int x) => x * x * x;
    static float lerp(float a, float b, float t) => a + t * (b - a);
    static float random(float a, float b) => r.NextSingle() * (a-b) + a;
    static int random(int a, int b) => r.Next(a, b);
    static float round(float a) => MathF.Round(a);
    static int roundI(float a) => (int)MathF.Round(a);
}