using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public class Sound
    {
        AudioFileReader? audioFile;
        WaveOutEvent? device;
        public void Open(string filePath)
        {
            audioFile = new(filePath);
        }

        public void Play()
        {
            ArgumentNullException.ThrowIfNull(audioFile);
            device = new WaveOutEvent();
            
            device.Init(audioFile);
            device.Play();
        }

        public void Stop()
        {
            ArgumentNullException.ThrowIfNull(audioFile);
            ArgumentNullException.ThrowIfNull(device);
            device.Stop();
            device.Dispose();
        }
    }
}
