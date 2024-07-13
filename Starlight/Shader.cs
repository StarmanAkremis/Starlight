using Microsoft.VisualBasic.FileIO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    /// <summary>
    /// Shader Manager
    /// </summary>
    public class Shader : IDisposable
    {

        /// <summary>
        /// OpenGL Handle to Shader program
        /// </summary>
        public int Handle;

        /// <summary>
        /// Create Shader
        /// </summary>
        /// <param name="vertexPath">Path to vertex shader file</param>
        /// <param name="fragmentPath">Path to fragment shader file</param>
        /// <exception cref="Exception">Shader compile\linking error</exception>
        public void init(string vertexPath, string fragmentPath)
        {
            int VertexShader, FragmentShader;

            string VertexShaderSource = File.ReadAllText(vertexPath);
            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vertSuccess);
            if (vertSuccess == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
                throw new Exception($"VERTEX SHADER COMPILE ERROR: {infoLog}");
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fragSuccess);
            if (fragSuccess == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                throw new Exception($"FRAGMENT SHADER COMPILE ERROR: {infoLog}");
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                throw new Exception($"SHADER LINKING ERROR: {infoLog}");
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        /// <summary>
        /// Sets Shader Uniforms
        /// </summary>
        /// <param name="name">Name of Uniform</param>
        /// <param name="value">The value to set the uniform to</param>
        public void SetUniform(string name, int value)
        {
            Use();

            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        /// <summary>
        /// Sets Shader Uniforms
        /// </summary>
        /// <param name="name">Name of Uniform</param>
        /// <param name="value">The value to set the uniform to</param>
        public void SetUniform(string name, Matrix4 value)
        {
            Use();

            int location = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(location, true, ref value);
        }

        /// <summary>
        /// Sets Shader Uniforms
        /// </summary>
        /// <param name="name">Name of Uniform</param>
        /// <param name="value">The value to set the uniform to</param>
        public void SetUniform(string name, float value)
        {
            Use();

            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        /// <summary>
        /// Sets Shader Uniforms
        /// </summary>
        /// <param name="name">Name of Uniform</param>
        /// <param name="value">The value to set the uniform to</param>
        public void SetUniform(string name, double value)
        {
            Use();

            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        /// <summary>
        /// Sets Shader Uniforms
        /// </summary>
        /// <param name="name">Name of Uniform</param>
        /// <param name="value">The value to set the uniform to</param>
        public void SetUniform(string name, Vector4 value)
        {
            Use();

            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform4(location, value);
        }

        /// <summary>
        /// Use the Shader program
        /// </summary>
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);
                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                //throw new Exception("GPU Resource leak! Did you forget to call Dispose()?");
                Console.Error.WriteLine("leak");
            }
        }

        /// <summary>
        /// Dispose of the Shader program
        /// It's mandatory to dispose of the shader program when shutting down the aplication
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int GetAttribLocation(string attribName)
        {
            Use();
            return GL.GetAttribLocation(Handle, attribName);
        }
    }
}