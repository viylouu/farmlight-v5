partial class farmlight {
    static void update() {
        //if (canexpand)
        //    mapexpand();
        genallchunks();

        if (Keyboard.IsKeyDown(Key.W))
            cam.Y -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.S))
            cam.Y += 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.A))
            cam.X -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.D))
            cam.X += 8 * Time.DeltaTime;

        if (Keyboard.IsKeyDown(Key.UpArrow))
            player.Z -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.DownArrow))
            player.Z += 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.LeftArrow))
            player.X -= 8 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.RightArrow))
            player.X += 8 * Time.DeltaTime;

        zoomact += Mouse.ScrollWheelDelta/3;
        zoomact = clamp(zoomact,1,6);
        zoom = dtween(zoom, zoomact, 12);

        if (Keyboard.IsKeyPressed(Key.C))
            view3D = !view3D;
    }
}