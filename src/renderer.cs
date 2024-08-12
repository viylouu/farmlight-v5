partial class farmlight {
    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        update();

        drawcalls = 0;

        renderworld(c);

        //c.Antialias(false);

        c.Fill(Color.White);
        //c.Font(font);
        drawcalls++;
        c.DrawAlignedText(round(1/Time.DeltaTime)+" fps", 16, 3,3, Alignment.TopLeft);
        drawcalls++;
        c.DrawAlignedText("seed "+seed, 16, 3,27, Alignment.TopLeft);





        drawcalls++;
        c.DrawAlignedText(drawcalls+" drawcalls", 16, 3,51, Alignment.TopLeft);
    }

    static void renderworld(ICanvas c) { 
        if(world != null)
            for (int v = 0; v < world[0].len; v++)
                for (int w = 0; w < world[0][0].len; w++)
                    for (int u = 0; u < world.len; u++) {
                        if (u != 0 || v != 0 || w != 0) {
                            float screenx = 1920/2+(u*chunksize*6-w*chunksize*6)*zoom-cam.X*16*zoom, screeny = 1080/2+(v*chunksize*-6+w*chunksize*3+u*chunksize*3)*zoom-cam.Y*16*zoom;
                            
                            if (world[u][v][w].generatedtex && !world[u][v][w].empty) {
                                if (!world[u][v][w].appliedtex)
                                { world[u][v][w].lod.ApplyChanges(); world[u][v][w].appliedtex = true; }

                                if(screenx>-chunklodsizex*(zoom*.5f)&&screeny>-chunklodsizey*(zoom*.5f)&&screenx<Window.Width+chunklodsizex*(zoom*.5f)&&screeny<Window.Height+chunklodsizey*(zoom*.5f)) {
                                    drawcalls++;
                                    c.DrawTexture(world[u][v][w].lod, screenx, screeny, world[u][v][w].lod.Width * zoom, world[u][v][w].lod.Height * -zoom, Alignment.Center);
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
                                    drawcalls++;
                                    c.DrawTexture(
                                        atlas,
                                        new Rectangle(world[u][v][w].tiles[x,y,z]*16%256, floor(world[u][v][w].tiles[x,y,z]/16)*16, 16, 16),
                                        new Rectangle(screenx, screeny, 16*zoom, -16*zoom, Alignment.Center)
                                    );
                                }
                            }
                        }
    }
}