partial class farmlight {
    static void update() {
        //if (canexpand)
        //    mapexpand();
        genallchunks();

        /*if (Keyboard.IsKeyDown(Key.W))
            cam.Y -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.S))
            cam.Y += 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.A))
            cam.X -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.D))
            cam.X += 8 * Time.DeltaTime;*/

        float size = 16*zoom;
        float screenx = (player.X*6-player.Z*6)/chunksize/1.5f, screeny = (-player.Y*6+player.Z*3+player.X*3)/chunksize;

        cam = dtween(cam, new Vector2(screenx,screeny), 6);

        if (Keyboard.IsKeyDown(Key.W)) {
            player.X -= 16 * Time.DeltaTime*2;
            player.Z -= 16 * Time.DeltaTime*2;
        }
        if (Keyboard.IsKeyDown(Key.S)) {
            player.X += 16 * Time.DeltaTime*2;
            player.Z += 16 * Time.DeltaTime*2;
        }
        if (Keyboard.IsKeyDown(Key.A)) {
            player.X -= 16 * Time.DeltaTime;
            player.Z += 16 * Time.DeltaTime;
        }
        if (Keyboard.IsKeyDown(Key.D)) {
            player.X += 16 * Time.DeltaTime;
            player.Z -= 16 * Time.DeltaTime;
        }
        if (Keyboard.IsKeyDown(Key.LeftShift))
            player.Y -= 16 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.Space))
            player.Y += 16 * Time.DeltaTime;

        zoomact += Mouse.ScrollWheelDelta/3;
        zoomact = clamp(zoomact,1,6);
        zoom = dtween(zoom, zoomact, 12);

        if (Keyboard.IsKeyPressed(Key.C))
            view3D = !view3D;
    }
}