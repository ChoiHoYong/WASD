using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AudioListener : ��
// AudioSource : �÷��̾� ���
// AudioClip : �������� ���� ����
// �� Ŭ������ Monobehaviour�� ��۹��� ���� Ŭ�����Դϴ�.
// �Ϲ����� ������δ� Invoke(�����Լ�), Coroutine(�񵿱� �Լ�) ����� �� �����ϴ�.


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

                // ���� ����� �Ŵ����� �ı����� �ʵ��� ó���մϴ�.
                DontDestroyOnLoad(obj);

            }
            return instance;
        }
    }

    // ��������� ������ ������ҽ�
    private AudioSource background;

    // ȿ������ ������ �����
    private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

    // ��������� ������ �����
    private Dictionary<string, AudioClip> backgroundDic = new Dictionary<string, AudioClip>();

    private List<AudioSource> audioList = new List<AudioSource>();

    public void LoadBackground()
    {
        AudioClip[] clips =
            Resources.LoadAll<AudioClip>("Sounds/Background");

        // ������ �����ŭ ��ȸ�մϴ�.
        for (int i = 0; i < clips.Length; i++)
        {
            if (backgroundDic.ContainsKey(clips[i].name) == false) // ������ ������ �ʴٸ�,
                backgroundDic.Add(clips[i].name, clips[i]); // �߰��Ѵ�.
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
        // ������� ���� ������ ��ϵǾ� �ִ��� üũ�մϴ�.
        if (backgroundDic.ContainsKey(name))
        {
            // ���� ���� ������ �޽��ϴ�.
            background.clip = backgroundDic[name];
            // 2D�� ������ ������ �����մϴ�.
            background.spatialBlend = spatialBlend;
            // �Ҹ��� ũ�⸦ �����մϴ�.
            background.volume = volume;
            // ������ �÷����մϴ�.
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
            // ������ �ε��� �� �ִٸ� ���� ��Ͽ� �����մϴ�.
            soundDic.Add(name, clip);
        }

        AudioSource audioSource = Pooling();
        audioSource.spatialBlend = 0;
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.transform.SendMessage("DeactiveDelay", soundDic[name].length,
                            SendMessageOptions.DontRequireReceiver); //������ �����Ű�� ���� ����
    }

    public void Play3D(string name, Vector3 point, float volume = 1)
    {
        if (soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);


            if (clip == null) // ���� ������ �ε����� ���ߴٸ� �Լ��� �����մϴ�.
                return;
            // ������ �ε��� �� �ִٸ� ���� ��Ͽ� �����մϴ�.
            soundDic.Add(name, clip);
        }
        // ���� ����� �� �ִ� ������ҽ��� �޽��ϴ�.
        AudioSource audioSource = Pooling();
        audioSource.spatialBlend = 1; // 3D������ ������ �� �ִ� ���·� �����մϴ�.
        audioSource.gameObject.transform.position = point; // ����� �ҽ��� ��ġ�� �����մϴ�.
        audioSource.clip = soundDic[name]; // ������ҽ��� ����� ������ �����մϴ�.
        audioSource.volume = volume;
        audioSource.Play(); // ������ �����մϴ�.
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
            // �߰���.!!
            obj.transform.SetParent(transform);

            audioSource = obj.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioList.Add(audioSource);

        }
        return audioSource;
    }

}
