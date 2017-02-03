using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CYINT.XPlatformMediaPlayer
{    
    public delegate void StateChangeEventHandler(int state);

    public interface IXPlatformMediaPlayer
    {
        event StateChangeEventHandler StateChanged;
        void LoadMedia(string resource); 
        void PlayMedia(); 
        void SetMediaLooping(bool loopMedia);
        void SetPlayFlag(bool playFlag);
        void SetStartPosition(double startPosition);
        void SetPlayerState(int state);
        void SetCurrentSeekerPosition(double position);
        void StopPlaying();
        void ResetResources();
        bool IsMediaLooping();
        bool GetPlayFlag();
        double GetStartPosition();
        int GetPlayerState();
        int GetMediaLength();
        double GetCurrentSeekerPosition();
        string GetResource();
        IXPlatformMediaObject GetMediaPlayer();
        void SetMediaPlayer(IXPlatformMediaObject mediaPlayer);
    }
}
