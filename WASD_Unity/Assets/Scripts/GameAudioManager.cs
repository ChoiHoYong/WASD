using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AudioListener : 귀
// AudioSource : 플레이어 기기
// AudioClip : 실질적인 사운드 파일
// 이 클래스는 Monobehaviour를 상송받지 않은 클래스입니다.
// 일반적인 방법으로는 Invoke(지연함수), Coroutine(비동기 함수) 사용할 수 없습니다.


public class GameAudioManager : MonoBehaviour
{
    private static GameAudioManager instance;

    private GameAudioManager() { }

    public static GameAudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj =
                new GameObject("GameAudioManager", typeof(GameAudioManager), typeof(AudioSource));

                instance = obj.GetComponent<GameAudioManager>();
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                audioSource.spatialBlend = 0;
                audioSource.volume = 1.0f;
                instance.background = audioSource;

                // 게임 오디오 매니저가 파괴되지 않도록 처리합니다.
                DontDestroyOnLoad(obj);

            }
            return instance;
        }
    }

    // 배경음악을 실행할 오디오소스
    private AudioSource background;

    // 효과음을 저장할 저장소
    private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

    // 배경음악을 저장할 저장소
    private Dictionary<string, AudioClip> backgroundDic = new Dictionary<string, AudioClip>();

    private List<AudioSource> audioList = new List<AudioSource>();

    public void LoadBackground()
    {
        AudioClip[] clips =
            Resources.LoadAll<AudioClip>("Sounds/Background");

        // 사운드의 사이즈만큼 순회합니다.
        for (int i = 0; i < clips.Length; i++)
        {
            if (backgroundDic.ContainsKey(clips[i].name) == false) // 파일이 들어가있지 않다면,
                backgroundDic.Add(clips[i].name, clips[i]); // 추가한다.
            if (soundDic.ContainsKey(clips[i].name) == false)
                soundDic.Add(clips[i].name, clips[i]);
        }
    }
    public void Setting()
    {
        LoadBackground();
    }
    public void PlayBackground(string name, float volume = 1, float spatialBlend = 0)
    {
        // 배경음악 사운드 파일이 등록되어 있는지 체크합니다.
        if (backgroundDic.ContainsKey(name))
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

    public void Play2D(string name, float volume = 1)
    {
        if (soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

            if (clip == null)
                return;
            // 파일을 로드할 수 있다면 사운드 목록에 저장합니다.
            soundDic.Add(name, clip);
        }

        AudioSource audioSource = Pooling();
        audioSource.spatialBlend = 0;
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.transform.SendMessage("DeactiveDelay", soundDic[name].length,
                            SendMessageOptions.DontRequireReceiver); //있으면 실행시키고 없음 말고
    }

    public void Play3D(string name, Vector3 point, float volume = 1)
    {
        if (soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);


            if (clip == null) // 사운드 파일을 로드하지 못했다면 함수를 종료합니다.
                return;
            // 파일을 로드할 수 있다면 사운드 목록에 저장합니다.
            soundDic.Add(name, clip);
        }
        // 현재 사용할 수 있는 오디오소스를 받습니다.
        AudioSource audioSource = Pooling();
        audioSource.spatialBlend = 1; // 3D음원을 실행할 수 있는 상태로 변경합니다.
        audioSource.gameObject.transform.position = point; // 오디오 소스의 위치를 변경합니다.
        audioSource.clip = soundDic[name]; // 오디오소스에 실행될 음원을 연결합니다.
        audioSource.volume = volume;
        audioSource.Play(); // 음원을 실행합니다.
        audioSource.transform.SendMessage("DeactiveDelay", soundDic[name].length,
                            SendMessageOptions.DontRequireReceiver);
    }
    public AudioSource Pooling()
    {
        AudioSource audioSource = null;
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].gameObject.activeSelf == false)
            {
                audioSource = audioList[i];
                audioSource.gameObject.SetActive(true);
                break;
            }
        }
        if (audioSource == null)
        {
            GameObject obj = new GameObject("Sound", typeof(AudioSource), typeof(PoollingObject));
            // 추가함.!!
            obj.transform.SetParent(transform);

            audioSource = obj.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioList.Add(audioSource);

        }
        return audioSource;
    }

}
