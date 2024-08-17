﻿partial class farmlight {
    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        Gradient sky = new LinearGradient(0, 0, 0, Window.Height, new ColorF[] { skyL.ToColorF(), skyD.ToColorF() });

        update();

        c.Fill(sky);
        c.DrawRect(0, 0, Window.Width, Window.Height);

        if (!view3D)
            renderworld(c);
        else
            render3D(c);

        c.Fill(Color.White);
        c.DrawAlignedText(round(1/Time.DeltaTime)+" fps", 16, 3,3, Alignment.TopLeft);
        c.DrawAlignedText("seed "+seed, 16, 3,27, Alignment.TopLeft);
    }

    static void render3D(ICanvas c) {
        var canvasShader = new CubeCanvasShader() {
            tex = atlas,
        };

        var vertexShader = new CubeVertexShader() {
            world = Matrix4x4.CreateRotationY(Time.TotalTime*Angle.ToRadians(60)),
            view = Matrix4x4.CreateLookAtLeftHanded(Vector3.One,Vector3.Zero,Vector3.UnitY),
            proj = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(MathF.PI/3f,c.Width/(float)c.Height,1f,100f) 
        };

        c.Fill(canvasShader, vertexShader);
        c.DrawTriangles<Vertex>(vertices);
    }

    static void renderworld(ICanvas c) {
        int minx = (int)clamp(player.X-renderdist/2,0,world.len),
            maxx = (int)clamp(player.X+renderdist/2,0,world.len),
            minz = (int)clamp(player.Z-renderdist/2,0,world[0][0].len),
            maxz = (int)clamp(player.Z+renderdist/2,0,world[0][0].len);

        if(world != null)
            for (int v = 0; v < world[0].len; v++)
                for (int w = minz; w < maxz; w++)
                    for (int u = minx; u < maxx; u++) {
                        if (u != 0 || v != 0 || w != 0) {
                            float screenx = 1920/2+(u*chunksize*6-w*chunksize*6)*zoom-cam.X*16*zoom, screeny = 1080/2+(v*chunksize*-6+w*chunksize*3+u*chunksize*3)*zoom-cam.Y*16*zoom;
                            
                            if (world[u][v][w].generatedtex && !world[u][v][w].empty) {
                                if (!world[u][v][w].appliedtex)
                                { world[u][v][w].lod.ApplyChanges(); world[u][v][w].appliedtex = true; }

                                if(screenx>-chunklodsizex*(zoom*.5f)&&screeny>-chunklodsizey*(zoom*.5f)&&screenx<Window.Width+chunklodsizex*(zoom*.5f)&&screeny<Window.Height+chunklodsizey*(zoom*.5f)) {
                                    float bright = v*chunksize/((float)chunksize*worldsizey);
                                    if(hshaded)
                                        c.DrawTexture(
                                            world[u][v][w].lod, 
                                            new Rectangle(
                                                screenx, screeny,
                                                world[u][v][w].lod.Width * zoom, 
                                                world[u][v][w].lod.Height * -zoom, 
                                                Alignment.Center
                                            ),
                                            new ColorF(bright, bright, bright, 1)
                                        );
                                    else
                                        c.DrawTexture(
                                            world[u][v][w].lod, 
                                            new Rectangle(
                                                screenx, screeny,
                                                world[u][v][w].lod.Width * zoom, 
                                                world[u][v][w].lod.Height * -zoom, 
                                                Alignment.Center
                                            )
                                        );
                                }
                            }
                        }
                        else
                            rendindividualblocks(c, u,v,w);
                    }
    }

    static void rendindividualblocks(ICanvas c, int u, int v, int w) { 
        for (int y = 0; y < chunksize; y++)
            for (int z = 0; z < chunksize; z++)
                for (int x = 0; x < chunksize; x++)
                    if (world[u][v][w].generated)
                        if (world[u][v][w].tiles[x,y,z] != 0) {
                            byte neighbors = 0;

                            if(x>0?(world[u][v][w].tiles[x-1,y,z]!=0):true)
                                neighbors++;
                            if(x<chunksize-1 && world[u][v][w].tiles[x+1,y,z]!=0)
                                neighbors++;
                            if(z>0?(world[u][v][w].tiles[x,y,z-1]!=0):true)
                                neighbors++;
                            if(z<chunksize-1 && world[u][v][w].tiles[x,y,z+1]!=0)
                                neighbors++;
                            if(y>0?(world[u][v][w].tiles[x,y-1,z]!=0):true)
                                neighbors++;
                            if(y<chunksize-1 && world[u][v][w].tiles[x,y+1,z]!=0)
                                neighbors++;

                            if(neighbors < 6) {
                                float size = 16*zoom;

                                float screenx = 1920/2+(x*6-z*6)*zoom-cam.X*size, screeny = 1080/2+(-y*6+z*3+x*3)*zoom-cam.Y*size;

                                if(screenx>-size&&screeny>-size&&screenx<Window.Width+size&&screeny<Window.Height+size) {
                                    c.DrawTexture(
                                        atlas,
                                        new Rectangle(world[u][v][w].tiles[x,y,z]*16%256, 240-floor(world[u][v][w].tiles[x,y,z]/16)*16, 16, 16),
                                        new Rectangle(screenx, screeny, 16*zoom, -16*zoom, Alignment.Center)
                                    );
                                }
                            }
                        }
    }
}