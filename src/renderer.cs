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
    }
}