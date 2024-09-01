partial class farmlight {
    class chunk {
        public byte[,,] tiles;
        //public bool[,,] coll;
        //public bool generated = false, 
        //            generatedtex = false,
        //            appliedtex = false;
        public ITexture lod;
        //public bool empty = true;
        public byte booldata = 8;
    }

    /// <summary>
    ///  bool is bit 0 or val "1"
    /// </summary>
    static bool cgenerated(chunk c) => (c.booldata & (1 << 0)) != 0;
    /// <summary>
    ///  bool is bit 1 or val "2"
    /// </summary>
    static bool cgeneratedtex(chunk c) => (c.booldata & (1 << 1)) != 0;
    /// <summary>
    ///  bool is bit 2 or val "4"
    /// </summary>
    static bool cappliedtex(chunk c) => (c.booldata & (1 << 2)) != 0;
    /// <summary>
    ///  bool is bit 3 or val "8"
    /// </summary>
    static bool cempty(chunk c) => (c.booldata & (1 << 3)) != 0;
}