partial class farmlight {
    static void update() {
        //if (canexpand)
        //    mapexpand();
        genallchunks();

        cam = dtween(cam, new Vector2(player.X*6-player.Z*6,-player.Y*6+player.Z*3+player.X*3), 6);

        movement();

        zoomact += Mouse.ScrollWheelDelta/3;
        zoomact = clamp(zoomact,1,6);
        zoom = dtween(zoom, zoomact, 12);

        if (Keyboard.IsKeyPressed(Key.C))
            view3D = !view3D;

        pmenu();
    }

    static void movement() {
        Vector2 targetvel = new();

        if (Keyboard.IsKeyDown(Key.W)) {
            targetvel.X += -1;
            targetvel.Y += -1;
        }
        if (Keyboard.IsKeyDown(Key.S)) {
            targetvel.X += 1;
            targetvel.Y += 1;
        }
        if (Keyboard.IsKeyDown(Key.A)) {
            targetvel.X += -1;
            targetvel.Y += 1;
        }
        if (Keyboard.IsKeyDown(Key.D)) {
            targetvel.X += 1;
            targetvel.Y += -1;
        }

        //if (Keyboard.IsKeyPressed(Key.Space))
        //    playervel.Y = 12;

        if (Keyboard.IsKeyDown(Key.LeftShift))
            player.Y -= 16 * Time.DeltaTime;
        if (Keyboard.IsKeyDown(Key.Space))
            player.Y += 16 * Time.DeltaTime;

        targetvel = targetvel.Normalized() * 12;

        playervel.X = dtween(playervel.X, targetvel.X, 8);
        //playervel.Y -= 48 * Time.DeltaTime;
        playervel.Z = dtween(playervel.Z, targetvel.Y, 8);

        playervel.Y = clamp(playervel.Y, -24, 24);

        player.X += playervel.X * Time.DeltaTime;
        player.Z += playervel.Z * Time.DeltaTime;
        //player.Y += playervel.Y * Time.DeltaTime;

        if(player.Y > chunksize*worldsizey-1) {
            player.Y = 175;
            cam = new Vector2(player.X*6-player.Z*6,-player.Y*6+player.Z*3+player.X*3);
        }
        if(player.Y < 1) {
            player.Y = 175;
            cam = new Vector2(player.X*6-player.Z*6,-player.Y*6+player.Z*3+player.X*3);
        }

        //if (world[floor(player.X/chunksize)][floor((player.Y-2)/chunksize)][floor(player.Z/chunksize)].tiles[(int)player.X%chunksize,(int)(player.Y-2)%chunksize,(int)player.Z%chunksize]>0)
        //{ player.Y -= playervel.Y * Time.DeltaTime; playervel.Y = 0; }
    }

    static void pmenu() { 
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