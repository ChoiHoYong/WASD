using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static
// AudioListener : 귀
// AudioSource : 플레이어 기기
// AudioClip : 실질적인 사운드 파일
public static class GameAudioManager
{
    // 배경음악을 실행할 오디오소스
    private static AudioSource background;
    // 효과음을 저장할 저장소
    private static Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();
    // 배경음악을 저장할 저장소
    private static Dictionary<string, AudioClip> backgroundDic = new Dictionary<string, AudioClip>();

    private static List<AudioSource> audioList = new List<AudioSource>();

    public static void LoadBackground()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/Background");
        // 사운드의 개수 만큼 순회합니다.
        for(int i = 0; i<clips.Length;i++)
        {
            if (backgroundDic.ContainsKey(clips[i].name) == false)
                backgroundDic.Add(clips[i].name, clips[i]);
        }
    }

    public static void Setting()
    {
        LoadBackground();
        GameObject music = new GameObject("Background",typeof(AudioSource));
        // 생성된 게임 오브젝트에서 오디오 소스를 얻습니다.
        background = music.GetComponent<AudioSource>();
        // 반복재생
        background.loop = true;
        // 값이 0 이면 2D, 1 이면 3D음악 재생
        background.spatialBlend = 0;

        // 다른 신으로 이동되더라도 배경음악을 실행하기 위해서
        // 생성된 게임오브젝트가 파괴되지 않도록 처리합니다.
        Object.DontDestroyOnLoad(music);
    }
    public static void PlayBackground(string name, float volume = 1, float spatialBlend = 0)
    {
        // 배경음악 사운드 파일이 등록되어 있는지 체크합니다.
        if(backgroundDic.ContainsKey(name))
        {
            // 배경된 사운드 파일을 받습니다.
            background.clip = backgroundDic[name];
            // 2D로 실행할 것인지 결정합니다.
            background.spatialBlend = spatialBlend;
            // 소리의 크기를 설정합니다.
            background.volume = volume;
            // 음원을 플레이합니다.
            background.Play();
        }
    }
    public static void Play2D(string name, float volume = 1)
    {
        if(soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

            if (clip == null)
                return;

            // 파일을 로드할 수 있다면 사운드 목록에 저장합니다.
            soundDic.Add(name, clip);
        }
        // 현재 사용할 수 있는 오디오소스를 받습니다.
        AudioSource audioSource = Pooling();
        // 2D 음원을 실행할 수 있는 상태로 변경합니다.
        audioSource.spatialBlend = 0;
        // 오디오 소스에 실행될 음원을 연결합니다.
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        // 오디오를 실행합니다.
        audioSource.Play();
        audioSource.transform.SendMessage("DeactiveDelay", soundDic[name].length, SendMessageOptions.DontRequireReceiver);
    }
    public static void Play3D(string name, Vector3 point, float volume = 1)
    {
        if(soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

            // 파일을 로드할 수 있다면 사운드 목록에 저장합니다.
            if (clip == null)
            {
                return;
            }
            soundDic.Add(name, clip);
        }
        // 현재 사용할 수 있는 오디오소스를 받습니다.
        AudioSource audioSource = Pooling();
        // 3D 음원을 실행할 수 있는 상태로 변경합니다.
        audioSource.spatialBlend = 1;
        // 오디오 소스의 위치를 변경합니다.
        audioSource.gameObject.transform.position = point;
        // 오디오 소스에 실행될 음원을 연결합니다.
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        // 오디오를 실행합니다.
        audioSource.Play();
    }
    public static AudioSource Pooling()
    {
        AudioSource audioSource = null;
        for(int i = 0;i<audioList.Count;++i)
        {
            if (audioList[i].gameObject.activeSelf == false)
            {
                audioSource = audioList[i];
                audioSource.gameObject.SetActive(true);
                break;
            }
        }
        if(audioSource == null)
        {
            GameObject obj = new GameObject("Sound", typeof(AudioSource), typeof(PoollingObject));
            audioSource = obj.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioList.Add(audioSource);
        }
        return audioSource;
    }
}
