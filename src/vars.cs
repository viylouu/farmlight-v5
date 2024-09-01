partial class farmlight {
    //misc
    static ITexture atlas;

    //player
    static Vector3 player = new Vector3(24,239,24);
    static Vector3 playervel;

    //cam
    static Vector2 cam;
    static float zoom = 128;
    static float zoomact = 6;

    //performance
    static int maxasynccalls = 96;
    static int renderdist = 6;

    //worldgen
    static int chunksize = 12;
    static perlin perl;
    static int seed;
    static bool canexpand = true;

    static int chunklodsizex = chunksize*12+16;
    static int chunklodsizey = chunksize*12+16;

    static int worldsizex = 8;
    static int worldsizey = 20;
    static int worldsizez = 8;

    static listTS<listTS<listTS<chunk>>> world;

    //sky
    static Color skyL = new Color(111,183,171);
    static Color skyD = new Color(83,151,153);

    //shading
    static bool hshaded = false;
    static float shadowopac = 1;

    //3D
    static bool view3D;

    //pause menu
    static bool pmopen;

    //settings
    static bool setopen;

    static string[] resses = {"3840x2160","2560x1440","1920x1080","1600x900","1366x768","1280x720","1024x576","854x480"};
    static Vector2[] ressesV = {new(3840,2160),new(2560,1440),new(1920,1080),new(1600,900),new(1366,768),new(1280,720),new(1024,576),new(854,480)};
    static Vector2 res = new(1920,1080);
    static int resI = 2;
    static float spritescale=1;
}