partial class farmlight {
    static void initperlin() {
        perl = new perlin();
        seed = random(int.MinValue, int.MaxValue);
        perl.setseed(seed);
        perl.setfreq(.05f);
        perl.setfract();
    }

    static async void genchunk(int u, int v, int w) {
        world[u][v][w].tiles = new byte[chunksize,chunksize,chunksize];
        //world[u][v][w].coll = new bool[chunksize,chunksize,chunksize];
        world[u][v][w].lod = Graphics.CreateTexture(chunklodsizex, chunklodsizey);

        int i = 0;

        for(int x = 0; x < chunksize; x++)
            for(int y = 0; y < chunksize; y++)
                for(int z = 0; z < chunksize; z++) {
                    if(perl.get(u*chunksize+x,v*chunksize+y,w*chunksize+z)>.5f&&y+v*chunksize<perl.get(u*chunksize+x,w*chunksize+z)*6+175)
                    { world[u][v][w].tiles[x,y,z] = 1; world[u][v][w].booldata = 0; }
                    //else if(u*chunksize+x==0||w*chunksize+z==0)
                    //{ world[u][v][w].tiles[x,y,z] = 1; world[u][v][w].empty = false; }

                    //i++;
                    //if (i % maxasynccalls == 0)
                    //    await Task.Delay(1);
                }

        world[u][v][w].booldata = (byte)(world[u][v][w].booldata|1);

        for(int x = 0; x < chunksize; x++)
            for(int y = 0; y < chunksize; y++)
                for(int z = 0; z < chunksize; z++) {
                    if (world[u][v][w].tiles[x,y,z] == 1) {
                        if (y+v*chunksize>perl.get(u*chunksize+x,w*chunksize+z)*6+165) {
                            if(y<chunksize-1) {
                                if (world[u][v][w].tiles[x,y+1,z]==0)
                                    world[u][v][w].tiles[x,y,z] = 2;
                                else
                                    world[u][v][w].tiles[x, y, z] = 3;
                            } else if(v<world[u].len-1) {
                                if (world[u][v+1][w].tiles[x,0,z] == 0)
                                    world[u][v][w].tiles[x,y,z] = 2;
                                else
                                    world[u][v][w].tiles[x, y, z] = 3;
                            } else
                                world[u][v][w].tiles[x,y,z] = 2;
                        }
                        else
                            world[u][v][w].tiles[x,y,z] = 4;
                    }

                    //i++;
                    //if (i % maxasynccalls == 0)
                    //    await Task.Delay(1);
                }

        
        for (int z = 0; z < chunksize; z++)
            for (int x = 0; x < chunksize; x++)
                for (int y = 0; y < chunksize; y++)
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
                            int lodx = x*6-z*6+chunksize*6, lody = z*3+x*3-y*6+chunksize*6;

                            byte tilex = (byte)(world[u][v][w].tiles[x,y,z]%16*16), 
                                 tiley = (byte)(floor(world[u][v][w].tiles[x,y,z]/16)*16);

                            for(int j = 0; j < 16; j++)
                                for(int k = 0; k < 16; k++) {
                                    if(atlas[tilex+j,tiley+k].A > 0)
                                        world[u][v][w].lod[lodx+j,lody+k] = atlas[tilex+j,tiley+k];

                                    //i++;
                                    //if (i % maxasynccalls == 0)
                                    //    await Task.Delay(1);
                                }
                        }
                    }

        world[u][v][w].booldata = (byte)(world[u][v][w].booldata|2);
    }

    static async void mapexpand() {
        canexpand = false;

        int x = 0;

        if (world.len == 0)
            for(int i = 0; i < worldsizex; i++) {
                world.add(new listTS<listTS<chunk>>());

                //i++;
                //if (i % maxasynccalls == 0)
                //    await Task.Delay(1);
            }

        if (world[0].len == 0)
            for (int i = 0; i < worldsizex; i++)
                for (int j = 0; j < worldsizey; j++) {
                    world[i].add(new listTS<chunk>());

                    //i++;
                    //if (i % maxasynccalls == 0)
                    //    await Task.Delay(1);
                }

        if (world[0][0].len == 0)
            for (int i = 0; i < worldsizex; i++)
                for (int j = 0; j < worldsizey; j++)
                    for (int k = 0; k < worldsizez; k++) {
                        world[i][j].add(new chunk());

                        //i++;
                        //if (i % maxasynccalls == 0)
                        //    await Task.Delay(1);
                    }

        canexpand = true;
    }

    static void genallchunks() {
        for (int x = 0; x < worldsizex; x++)
            for (int y = worldsizey-1; y >= 0; y--)
                for (int z = 0; z < worldsizez; z++)
                    if (x < world.len && y < world[x].len && z < world[x][y].len)
                        if (!cgenerated(world[x][y][z]))
                            genchunk(x, y, z);
    }
}