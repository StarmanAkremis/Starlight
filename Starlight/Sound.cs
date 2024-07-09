using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    /// <summary>
    /// Sound class, used to play sound files
    /// </summary>
    public class Sound
    {
        AudioFileReader audioFile;
        WaveOutEvent? device;

        /// <summary>
        /// Create Sound Class
        /// </summary>
        /// <param name="filePath">Path to audio file</param>
        public Sound(string filePath)
        {
            audioFile = new(filePath);
        }

        /// <summary>
        /// Play sound file
        /// </summary>
        public void Play()
        {
            device = new WaveOutEvent();
            
            device.Init(audioFile);
            device.Play();
        }

        /// <summary>
        /// Stop sound file
        /// </summary>
        public void Stop()
        {
            ArgumentNullException.ThrowIfNull(device);
            device.Stop();
            device.Dispose();
        }
    }
}
