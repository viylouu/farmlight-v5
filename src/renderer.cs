partial class farmlight {
    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        //for chunk rendering

        /*
         * 
         * ok so basically all chunks have their data 
         * and a texture that can be rendered instead of 
         * manually rendering every block when you're not in that chunk
         * 
         * if you are in a chunk, then it renders every block in that chunk one by one
         * no silly lods for every y position (takes up too much memory and isnt worth it)
         * 
         * i made a thread safe list so just use old basilisk code for generating new
         * data in the thing, just make it have a z axis, so no need for lock
         * statements (maybe keep the try catch, but if no errors pop up, remove it
         * 
         * use the perlin class, ex:
         * 
         
        perlin perl = new perlin(random(0, 16));
        perl.get(x, y); or perl.get(x, y, z); (returns val between 0 and 1)

         * 
         * 
         */

        mapexpand();

        if(world != null)
            for (int v = 0; v < world[0].len; v++)
                for (int w = 0; w < world[0][0].len; w++)
                    for (int u = 0; u < world.len; u++) {
                        if (u==0||v==0||w==0) { 
                            float screenx = 1920/2 + (u*chunksize*6 - w*chunksize*6)*4, screeny = 1080/2 + (v*chunksize*-6 + w*chunksize*3 + u*chunksize*3)*4;

                            c.DrawTexture(world[u][v][w].lod, screenx, screeny, world[u][v][w].lod.Width*4, world[u][v][w].lod.Height*4, Alignment.Center);
                        }
                        else
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
                                                    float screenx = 1920/2 + (x*6 - z*6)*4, screeny = 1080/2 + (-y*6 + z*3 + x*3)*4;

                                                    c.DrawTexture(
                                                        atlas, 
                                                        new Rectangle(world[u][v][w].tiles[x,y,z]*16%128, floor(world[u][v][w].tiles[x,y,z]/8)*16, 16, 16),
                                                        new Rectangle(screenx, screeny, 64, 64, Alignment.Center)
                                                    );
                                                }
                                            }
                    }

        c.Fill(Color.White);
        c.DrawText(round(1/Time.DeltaTime)+" fps", Vector2.One*3);
    }
}