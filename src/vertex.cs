partial class farmlight {  
    struct Vertex { 
        public Vector3 Position;
        public Vector2 Texture;

        public Vertex(Vector3 position, Vector2 texture) {
            this.Position = position;
            this.Texture = texture;
        }
    }
}