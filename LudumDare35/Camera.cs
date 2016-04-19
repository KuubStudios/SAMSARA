using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare35
{
    public class Camera
    {
        public float Zoom;
        public Vector2 Position;
        public float Rotation;
        public Matrix Transform;

        public Camera()
        {
            
        }

        public void Translate(Vector2 amount)
        {
            
        }

        public void Focus(Vector2 target, float x)
        {
            
        }

        public Matrix Transformation(GraphicsDevice graphicsDevice)
        {
            Transform = Matrix.CreateTranslation(
                new Vector3(-Position.X, -Position.Y, 0))*
                        Matrix.CreateRotationZ(Rotation)*
                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))*
                        Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width*0.5f,
                            graphicsDevice.Viewport.Height*0.5f, 0));

            return Transform;;
        }
    }
}
