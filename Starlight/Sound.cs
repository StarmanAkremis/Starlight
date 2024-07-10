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
        WaveOutEvent device;

        /// <summary>
        /// Create Sound Class
        /// </summary>
        /// <param name="filePath">Path to audio file</param>
        public Sound(string filePath)
        {
            audioFile = new(filePath);
            device = new WaveOutEvent();
            device.Init(audioFile);

        }

        /// <summary>
        /// Play Sound File
        /// </summary>
        /// <param name="loop">loop sound file</param>
        public void Play(bool loop = false)
        {
            device.Play();

            device.PlaybackStopped += (s,e) =>
            { 
                if (loop)
                {
                    Play(loop);
                }
            };
        }

        /// <summary>
        /// Stop sound file
        /// </summary>
        public void Stop()
        {
            device.Stop();
        }

        ~Sound()
        {
            device.Dispose();
            audioFile.Dispose();
        }
    }
}
