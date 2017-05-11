using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LethalWeapon
{
    /// <summary>
    /// A Basic Camera for MonoGame. Create by Jeppe & Sebbe @ Malmö Högskola.
    /// 
    /// Denna klass skapar och hanterar en kamera.
    /// Klassen är väldigt enkel och räknar om en matris enligt en 2d-vector
    /// Ni kan använda den när ni anropar spriteBatch.Begin() i t.ex Game1.
    /// Exempel: spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
    /// 
    /// För att få kameran att följa ett objekt kan ni i update kalla på camera.SetPosition och ange objektets position.
    /// Behöver ni göra uträkningar på kamerans nuarande position använder ni camera.GetPosition().
    /// </summary>
    class Camera
    {
        private Matrix transform;
        private Vector2 position;
        private Viewport view;
        protected float zoomX;
        protected float zoomY;
        protected float rotation;

        /// <summary>
        /// Creates an instance of the Camera class.
        /// </summary>
        /// <param name="view">A Viewport used to calculate the view transform</param>
        public Camera(Viewport view)
        {
            this.view = view;
        }

        /// <summary>
        /// Sets the position of the camera. The camera will be centered around the given vector.
        /// </summary>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
            transform = Matrix.CreateTranslation(-position.X, -position.Y, 0) * Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(ZoomX, ZoomY, 1) * Matrix.CreateTranslation(view.Width * 0.5f, view.Height * 0.5f, 0);
        }

        /// <summary>
        /// Gets the position of the camera.
        /// </summary>
        public Vector2 GetPosition()
        {
            return position;
        }

        public float ZoomX
        {
            get { return zoomX; }
            set { zoomX = value; if (zoomX < 0.1f) zoomX = 0.1f; } // Negative zoom will flip image
        }

        public float ZoomY
        {
            get { return zoomY; }
            set { zoomY = value; if (zoomY < 0.1f) zoomY = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Gets the Camera transform.
        /// </summary>
        public Matrix GetTransform()
        {
            return transform;
        }
    }
}
