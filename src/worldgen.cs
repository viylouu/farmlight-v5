partial class farmlight {
    static void initperlin() {
        perl = new perlin();
        perl.setseed(random(int.MinValue, int.MaxValue));
        perl.setfreq(.05f);
    }

    static async void genchunk(int u, int v, int w) {
        world[u][v][w].tiles = new byte[chunksize,chunksize,chunksize];
        world[u][v][w].coll = new bool[chunksize,chunksize,chunksize];
        world[u][v][w].lod = Graphics.CreateTexture(chunksize*12+16,chunksize*12+16);
        world[u][v][w].generated = true;

        byte i = 0;
        byte imod = 64;

        for(int x = 0; x < chunksize; x++)
            for(int y = 0; y < chunksize; y++)
                for(int z = 0; z < chunksize; z++) {
                    if(perl.get(u*chunksize+x,v*chunksize+y,w*chunksize+z) > .5f && y+v*chunksize < perl.get(u*chunksize+x,w*chunksize+z)*32)
                        world[u][v][w].tiles[x,y,z] = 19;

                    i++;
                    if (i % imod == 0)
                        await Task.Delay(1);
                }

        for (int y = 0; y < chunksize; y++)
            for (int z = 0; z < chunksize; z++)
                for (int x = 0; x < chunksize; x++) {
                    int lodx = x*6 - z*6 + chunksize*6, lody = z*3 + x*3 - y*6 + chunksize*6;

                    //xmax = chunksize*12
                    //ymax = chunksize*12

                    byte tilex = (byte)((world[u][v][w].tiles[x,y,z]%8)*16), 
                         tiley = (byte)(floor(world[u][v][w].tiles[x,y,z]/8)*16);

                    for(int j = 0; j < 16; j++)
                        for(int k = 0; k < 16; k++) {
                            if(atlas.GetPixel(tilex+j, tiley+k).A > 0)
                                world[u][v][w].lod.SetPixel(lodx+j,lody+k,atlas.GetPixel(tilex+j, tiley+k));

                            i++;
                            if (i % imod == 0)
                                await Task.Delay(1);
                        }
                }

        world[u][v][w].lod.ApplyChanges();
    }

    static async void mapexpand() {
        if (world.len == 0)
            world.add(new listTS<listTS<chunk>>());

        if (world[0].len == 0)
            world[0].add(new listTS<chunk>());

        if (world[0][0].len == 0)
            world[0][0].add(new chunk());

        if (!world[0][0][0].generated)
            genchunk(0,0,0);
    }
}