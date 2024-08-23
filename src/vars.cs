partial class farmlight {
    //misc
    static ITexture atlas;

    //player
    static Vector3 player;

    //cam
    static Vector2 cam;
    static float zoom = 128;
    static float zoomact = 6;

    //performance
    static int maxasynccalls = 96;
    static int renderdist = 4;

    //worldgen
    static int chunksize = 12;
    static perlin perl;
    static int seed;
    static bool canexpand = true;

    static int chunklodsizex = chunksize*12+16;
    static int chunklodsizey = chunksize*12+16;

    static int worldsizex = 16;
    static int worldsizey = 20;
    static int worldsizez = 16;

    static listTS<listTS<listTS<chunk>>> world;

    //sky
    static Color skyL = new Color(111,183,171);
    static Color skyD = new Color(83,151,153);

    //shading
    static bool hshaded = false;

    //3D
    static bool view3D;
}