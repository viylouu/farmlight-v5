partial class farmlight {
    class chunk {
        public byte[,,] tiles;
        public bool[,,] coll;
        public bool generated = false;
        public ITexture lod;
    }
}