partial class farmlight { 
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
}