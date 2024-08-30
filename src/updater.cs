partial class farmlight {
    static void update() {
        //if (canexpand)
        //    mapexpand();
        genallchunks();

        cam = dtween(cam, new Vector2(player.X*6-player.Z*6,-player.Y*6+player.Z*3+player.X*3), 6);

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

        if (Keyboard.IsKeyPressed(Key.Escape))
        { pmopen = !pmopen; setopen = false; }

        if (pmopen) {
            ImGui.Begin("menu");

            ImGui.SetWindowPos(new Vector2(0,0));
            ImGui.SetWindowSize(ImGui.GetMainViewport().Size/new Vector2(8,1));

            if(!setopen) {
                if (ImGui.Button("resume"))
                    pmopen = false;
                if(ImGui.Button("settings"))
                    setopen = true;
                if (ImGui.Button("close"))
                    Environment.Exit(0);
            } else {
                if (ImGui.Button("x"))
                    setopen = false;

                ImGui.BeginTabBar("settings");

                if(ImGui.BeginTabItem("video")) {
                    ImGui.ListBox("resolution", ref resI, resses, resses.Length);

                    if (ImGui.Button("apply")) {
                        res = ressesV[resI];
                        //spritescale = 
                        Simulation.SetFixedResolution((int)res.X,(int)res.Y,Color.Black, false, false, true);
                    }

                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }

            ImGui.End();
        }
    }
}