using UnityEngine;
namespace RunMarrakech.Audio
{
    public sealed class RunnerAudioManager : MonoBehaviour
    {
        public static RunnerAudioManager Instance{get;private set;}
        public AudioSource Music;
        public AudioSource Sfx;
        public AudioClip CoinClip,JumpClip,PowerUpClip,HitClip;
        void Awake(){if(Instance!=null){Destroy(gameObject);return;}Instance=this;DontDestroyOnLoad(gameObject);}
        public void Coin(){if(Sfx&&CoinClip)Sfx.PlayOneShot(CoinClip);}
        public void Jump(){if(Sfx&&JumpClip)Sfx.PlayOneShot(JumpClip);}
        public void PowerUp(){if(Sfx&&PowerUpClip)Sfx.PlayOneShot(PowerUpClip);}
        public void Hit(){if(Sfx&&HitClip)Sfx.PlayOneShot(HitClip);}
    }
}