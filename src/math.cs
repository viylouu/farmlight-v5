partial class farmlight {
    static float floor(float x) => MathF.Floor(x);
    static int floor(int x) => (int)MathF.Floor(x);
    static float cube(float x) => x * x * x;
    static int cube(int x) => x * x * x;
    static float lerp(float a, float b, float t) => a + t * (b - a);
}