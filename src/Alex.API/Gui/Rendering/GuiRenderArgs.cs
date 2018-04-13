﻿using System;
using Alex.API.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alex.API.Gui.Rendering
{
    public class GuiRenderArgs
    {
        public IGuiRenderer Renderer { get; }

        public GraphicsDevice Graphics { get; }
        public SpriteBatch SpriteBatch { get; }

        public GuiRenderArgs(IGuiRenderer renderer, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Renderer = renderer;
            Graphics = graphicsDevice;
            SpriteBatch = spriteBatch;
        }
        
        

        public void DrawRectangle(Rectangle bounds, Color color, int thickness = 1)
        {
            var texture = new Texture2D(Graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData(new Color[] {color});

            // Top
            SpriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y, bounds.Width, thickness), color);

            // Right
            SpriteBatch.Draw(texture, new Rectangle(bounds.X + bounds.Width - thickness, bounds.Y, thickness, bounds.Height),
                             color);

            // Bottom
            SpriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y + bounds.Height - thickness, bounds.Width, thickness),
                             color);

            // Left
            SpriteBatch.Draw(texture, new Rectangle(bounds.X, bounds.Y, thickness, bounds.Height), color);
        }

        public void Draw(TextureSlice2D    texture, Rectangle bounds,
                         TextureRepeatMode repeatMode = TextureRepeatMode.Stretch)
        {
            if (texture is NinePatchTexture2D ninePatch)
            {
                DrawNinePatch(bounds, ninePatch, repeatMode);
            }
            else
            {
                FillRectangle(bounds, texture, repeatMode);
            }
        }
        
        public void DrawNinePatch(Rectangle bounds, NinePatchTexture2D ninePatchTexture, TextureRepeatMode repeatMode)
        {
            if (ninePatchTexture.Padding == Thickness.Zero)
            {
                FillRectangle(bounds, ninePatchTexture.Texture, repeatMode);
                return;
            }
			
            var sourceRegions = ninePatchTexture.SourceRegions;
            var destRegions   = ninePatchTexture.ProjectRegions(bounds);

            for (var i = 0; i < sourceRegions.Length; i++)
            {
                var srcPatch = sourceRegions[i];
                var dstPatch = destRegions[i];

                if(dstPatch.Width > 0 && dstPatch.Height > 0)
                    SpriteBatch.Draw(ninePatchTexture.Texture, sourceRectangle: srcPatch, destinationRectangle: dstPatch, color: Color.White);
            }
        }
        public void FillRectangle(Rectangle bounds, TextureSlice2D texture, TextureRepeatMode repeatMode)
        {
            if (repeatMode == TextureRepeatMode.NoRepeat)
            {
                SpriteBatch.Draw(texture.Texture, new Rectangle(bounds.Location, new Point(texture.Width, texture.Height)), texture.Bounds,
                                 Color.White);
            }
            else if (repeatMode == TextureRepeatMode.Stretch)
            {
                SpriteBatch.Draw(texture.Texture, bounds, Color.White);
            }
            else if (repeatMode == TextureRepeatMode.ScaleToFit)
            {
            }
            else if (repeatMode == TextureRepeatMode.Tile)
            {
                var repeatX = Math.Ceiling((float) bounds.Width  / texture.Width);
                var repeatY = Math.Ceiling((float) bounds.Height / texture.Height);

                for (int i = 0; i < repeatX; i++)
                {
                    for (int j = 0; j < repeatY; j++)
                    {
                        var p = bounds.Location.ToVector2() + new Vector2(i * texture.Width, j * texture.Height);
                        SpriteBatch.Draw(texture.Texture, p, texture.Bounds, Color.White);
                    }
                }
            }
            else if (repeatMode == TextureRepeatMode.NoScaleCenterSlice)
            {

                var halfWidth = bounds.Width / 2f;
                var halfHeight = bounds.Height / 2f;
                int xOffset = bounds.X + (int)Math.Max(0, (bounds.Width - texture.Width) / 2f);
                int yOffset = bounds.Y + (int)Math.Max(0, (bounds.Height - texture.Height) / 2f);

                int dstLeftWidth = (int) Math.Floor(halfWidth);
                int dstRightWidth = (int) Math.Ceiling(halfWidth);
                int dstLeftHeight = (int) Math.Floor(halfHeight);
                int dstRightHeight = (int) Math.Ceiling(halfHeight);

                var srcHalfWidth = Math.Min(texture.Width / 2f, halfWidth);
                var srcHalfHeight = Math.Min(texture.Height / 2f, halfHeight);

                var srcX = texture.Bounds.X;
                var srcY = texture.Bounds.Y;

                int srcLeftWidth   = (int) Math.Floor(srcHalfWidth);
                int srcRightWidth  = (int) Math.Ceiling(srcHalfWidth);
                int srcLeftHeight  = (int) Math.Floor(srcHalfHeight);
                int srcRightHeight = (int) Math.Ceiling(srcHalfHeight);

                // Top Left
                SpriteBatch.Draw(texture.Texture, new Rectangle(xOffset               , yOffset, dstLeftWidth, dstLeftHeight), new Rectangle(srcX, srcY, srcLeftWidth, srcLeftHeight), Color.White);
                
                // Top Right
                SpriteBatch.Draw(texture.Texture, new Rectangle(xOffset + dstLeftWidth, yOffset, dstRightWidth, dstRightHeight), new Rectangle(texture.Width - srcRightWidth, srcY, srcRightWidth, srcRightHeight), Color.White);


                // Bottom Left
                SpriteBatch.Draw(texture.Texture, new Rectangle(xOffset               , yOffset + dstLeftHeight , dstLeftWidth, dstLeftHeight), new Rectangle(srcX, texture.Height - srcRightHeight, srcLeftWidth, srcLeftHeight), Color.White);
                
                // Bottom Right
                SpriteBatch.Draw(texture.Texture, new Rectangle(xOffset + dstLeftWidth, yOffset + dstRightHeight, dstRightWidth, dstRightHeight), new Rectangle(texture.Width - srcRightWidth, texture.Height - srcRightHeight, srcRightWidth, srcRightHeight), Color.White);
            }
        }

        public void DrawString(Vector2 position, SpriteFont font, string text, Color color, float scale = 1f)
        {
            SpriteBatch.DrawString(font, text, position, color, 0f, Vector2.Zero, new Vector2(scale), SpriteEffects.None, 0);
        }
    }
}