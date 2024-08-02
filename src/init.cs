partial class farmlight {
    static void init() {
        Window.Title = "farmlight";
        atlas = Graphics.LoadTexture(@"assets\atlas.png");
        world = new listTS<listTS<listTS<chunk>>>();
        Simulation.SetFixedResolution(1920,1080,Color.Black,false,false,true);
    }
}