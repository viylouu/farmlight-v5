partial class farmlight {
    static ITexture atlas;
    static listTS<listTS<listTS<chunk>>> world;
    static int chunksize = 12;

    //cam
    static Vector2 cam;
    static float zoom = 1;
    static float zoomact = 1;

    //performance
    static int maxasynccalls = 96;

    //data
    static uint drawcalls;

    //worldgen
    static perlin perl;
    static int seed;
    static bool canexpand = true;

    static int chunklodsizex = chunksize*12+16;
    static int chunklodsizey = chunksize*12+16;
 }