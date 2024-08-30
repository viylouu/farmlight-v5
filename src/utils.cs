global using i8 = System.SByte;
global using i16 = System.Int16;
global using i32 = System.Int32;
global using i64 = System.Int64;
global using i128 = System.Int128;
global using ui8 = System.Byte;
/*global using ui16 = System.UInt16;
global using ui32 = System.UInt32;
global using ui64 = System.UInt64;
global using ui128 = System.UInt128;
global using f32 = System.Single;
global using f64 = System.Double;
global using f128 = System.Decimal;
global using vec2 = System.Numerics.Vector2;
global using vec3 = System.Numerics.Vector3;
global using vec4 = System.Numerics.Vector4;
global using mat4 = System.Numerics.Matrix4x4;*/

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
    static float dtween(float c, float t, float s) => c+(t-c)/(s/(Time.DeltaTime*60));
    static Vector2 dtween(Vector2 c, Vector2 t, float s) => c+(t-c)/(s/(Time.DeltaTime*60));
    static Vector3 dtween(Vector3 c, Vector3 t, float s) => c+(t-c)/(s/(Time.DeltaTime*60));
    static float clamp(float c, float b, float a) => Math.Clamp(c,b,a);
    static int clamp(int c, int b, int a) => Math.Clamp(c, b, a);
}