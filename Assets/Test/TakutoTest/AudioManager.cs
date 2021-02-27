using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	//ボリューム保存用のkeyとデフォルト値
	private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
	private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
	private const float BGM_VOLUME_DEFULT = 1.0f;
	private const float SE_VOLUME_DEFULT = 1.0f;

	//BGMがフェードするのにかかる時間
	public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
	public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
	private float m_bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

	//次流すBGM名、SE名
	private string m_nextBGMName;
	private string m_nextSEName;

	//BGMをフェードアウト中か
	private bool m_isFadeOut = false;

	//BGM用、SE用に分けてオーディオソースを持つ
	public AudioSource[] m_attachSESource;
	int m_SESourceCounter = 0;
	public AudioSource m_attachBGMSource;

	//全Audioを保持
	private Dictionary<string, AudioClip> m_bgmDic, m_seDic;

	//=================================================================================
	//初期化
	//=================================================================================

	private new void Awake()
	{
		base.Awake();

		//リソースフォルダから全SE&BGMのファイルを読み込みセット
		m_bgmDic = new Dictionary<string, AudioClip>();
		m_seDic = new Dictionary<string, AudioClip>();

		object[] bgmList = Resources.LoadAll("Audio/BGM");
		object[] seList = Resources.LoadAll("Audio/SE");

		foreach (AudioClip bgm in bgmList)
		{
			m_bgmDic[bgm.name] = bgm;
		}
		foreach (AudioClip se in seList)
		{
			m_seDic[se.name] = se;
		}
	}

	private void Start()
	{
        m_attachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
		
        foreach (var item in m_attachSESource)
        {
			item.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
		}
	}

	//=================================================================================
	//SE
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>
	public void PlaySE(string seName, float delay = 0.0f)
	{
		if (!m_seDic.ContainsKey(seName))
		{
			Debug.Log(seName + "という名前のSEがありません");
			return;
		}

		m_nextSEName = seName;
		Invoke("DelayPlaySE", delay);
	}

	private void DelayPlaySE()
	{
		m_attachSESource[m_SESourceCounter].PlayOneShot(m_seDic[m_nextSEName] as AudioClip);
		m_SESourceCounter++;
        if (m_SESourceCounter >= m_attachSESource.Length) m_SESourceCounter = 0;
	}

	//=================================================================================
	//BGM
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH, float volume = 1)
	{
		if (!m_bgmDic.ContainsKey(bgmName))
		{
			Debug.Log(bgmName + "という名前のBGMがありません");
			return;
		}

		//現在BGMが流れていない時はそのまま流す
		if (!m_attachBGMSource.isPlaying)
		{
			m_nextBGMName = "";
			m_attachBGMSource.clip = m_bgmDic[bgmName] as AudioClip;
			m_attachBGMSource.Play();
			m_attachBGMSource.volume = volume;
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (m_attachBGMSource.clip.name != bgmName)
		{
			m_nextBGMName = bgmName;
			FadeOutBGM(fadeSpeedRate);
		}

	}

	/// <summary>
	/// 現在流れている曲をフェードアウトさせる
	/// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
	{
		if (m_isFadeOut) return;

		m_bgmFadeSpeedRate = fadeSpeedRate;
		m_isFadeOut = true;
	}

	private void Update()
	{
		if (!m_isFadeOut)
		{
			return;
		}

		//徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
		m_attachBGMSource.volume -= Time.deltaTime * m_bgmFadeSpeedRate;
		if (m_attachBGMSource.volume <= 0)
		{
			m_attachBGMSource.Stop();
			m_attachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
			m_isFadeOut = false;

			if (!string.IsNullOrEmpty(m_nextBGMName))
			{
				PlayBGM(m_nextBGMName);
			}
		}

	}

	//=================================================================================
	//音量変更
	//=================================================================================

	/// <summary>
	/// BGMとSEのボリュームを別々に変更&保存
	/// </summary>
	public void ChangeVolume(float BGMVolume, float SEVolume)
	{
		m_attachBGMSource.volume = BGMVolume;
		m_attachSESource[0].volume = SEVolume;

		PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
		PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
	}
}