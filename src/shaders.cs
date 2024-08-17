partial class farmlight {
    class CubeCanvasShader : CanvasShader {
        [VertexShaderOutput]
        Vector2 uv;

        public ITexture tex;

        public override ColorF GetPixelColor(Vector2 position) {
            ColorF x = /*tex.SampleUV(uv)*/ new ColorF(uv.X, uv.Y, 0, 1);
            if (x.A < 0.001f)
                ShaderIntrinsics.Discard();
            return x;
        }
    }

    class CubeVertexShader : VertexShader {
        [VertexData]
        Vertex vertex;

        public Matrix4x4 world;
        public Matrix4x4 view;
        public Matrix4x4 proj;

        [VertexShaderOutput]
        Vector2 uv;

        [UseClipSpace]
        public override Vector4 GetVertexPosition() {
            Vector4 result = new(vertex.Position, 1);
            result = Vector4.Transform(result, world);
            result = Vector4.Transform(result, view);
            result = Vector4.Transform(result, proj);

            uv = vertex.Texture;

            return result;
        }
    }
}