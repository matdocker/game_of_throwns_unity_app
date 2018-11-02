using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

  // Use this for initialization
  public AudioClip[] coindLanded;
  public AudioClip[] coinsfx;
  public AudioClip[] giantCoin;
  public AudioClip notAvailable;
  public AudioClip prize;
  public AudioClip other;
  public enum SFX : int
  {
    none, normal, regen, giant, levelup, shake, prize, wall, drop, nomore, other
  };
  public AudioSource NormalDropAudio;
  public AudioSource RegenAudio;
  public AudioSource GiantAudio;
  public AudioSource PrizeAudio;
  public AudioSource ShakeAudio;
  public AudioSource OtherAudio;
  public AudioSource WallsAudio;
  public AudioSource LevelupAudio;
  public AudioSource DropAudio;
  public AudioSource NoMoreAudio;
  public AudioSource ButtonAudio;
  public AudioSource BGM;

  public Slider slider;
  public Button menu;
  public Button back;
  private float volume;
  private int m_dropIndex = 0;
  private int m_giantIndex = 0;
  private bool m_isActive = false;

  void Start()
  {
    slider.onValueChanged.AddListener(UpdateSlider);
    back.onClick.AddListener(Save);
    menu.onClick.AddListener(ShowMenu);
    if (PlayerPrefs.HasKey("volume"))
    {
      volume = PlayerPrefs.GetFloat("volume");
    }
    else
    {
      volume = 1f;
    }
    UpdateSlider(volume);
  }

  // Update is called once per frame
  void Update()
  {

  }
  void Awake()
  {
    BGM.Play();
  }
  public void Save()
  {
    ButtonClicked();
    isActive = false;
    FindObjectOfType<GameScene>().isTouchEnabled = true;
    PlayerPrefs.SetFloat("volume", volume);
    back.transform.parent.gameObject.SetActive(false);
  }
  public void UpdateSlider(float meh)
  {
    NormalDropAudio.volume = meh;
    RegenAudio.volume = meh;
    ShakeAudio.volume = meh;
    WallsAudio.volume = meh;
    OtherAudio.volume = meh;
    LevelupAudio.volume = meh;
    PrizeAudio.volume = meh;
    GiantAudio.volume = meh;
    DropAudio.volume = meh;
    NoMoreAudio.volume = meh;
    BGM.volume = meh / 1.7f;
    ButtonAudio.volume = meh;
    volume = meh;
  }
  public bool isActive
  {
    set { m_isActive = value; }
    get { return m_isActive; }
  }
  void ShowMenu()
  {
    ButtonClicked();
    isActive = true;
    if (PlayerPrefs.HasKey("volume"))
    {
      volume = PlayerPrefs.GetFloat("volume");
    }
    else
    {
      volume = 1f;
    }
    slider.value = volume;
    UpdateSlider(volume);
  }
  public int DropCoinClip
  {
    get { return m_dropIndex; }
    set { m_dropIndex = value; }
  }
  public int GiantCoinClip
  {
    get { return m_giantIndex; }
    set { m_giantIndex = value; }
  }
  public void Play(SFX sfx)
  {
    if (sfx == SFX.normal)
    {
      if (NormalDropAudio.isPlaying)
      {
        NormalDropAudio.clip = coindLanded[Random.Range(0, coindLanded.Length)];
        NormalDropAudio.PlayDelayed(0.15f);
      }
      else
      {
        NormalDropAudio.clip = coindLanded[Random.Range(0, coindLanded.Length)];
        NormalDropAudio.Play();
      }
    }
    else if (sfx == SFX.other)
    {
      if (OtherAudio.isPlaying)
      {
        OtherAudio.clip = other;
        OtherAudio.PlayDelayed(0.15f);
      }
      else
      {
        OtherAudio.clip = coinsfx[Random.Range(0, coinsfx.Length)];
        OtherAudio.Play();
      }
    }
    else if (sfx == SFX.regen)
    {
      if (RegenAudio.isPlaying)
      {
        RegenAudio.PlayDelayed(0.12f);
      }
      else
      {
        RegenAudio.Play();
      }
    }
    else if (sfx == SFX.giant)
    {
      GiantAudio.clip = giantCoin[GiantCoinClip];
      if (GiantAudio.isPlaying)
      {
        GiantAudio.PlayDelayed(0.12f);
      }
      else
      {
        GiantAudio.Play();
      }
    }
    else if (sfx == SFX.prize)
    {
      if (PrizeAudio.isPlaying)
      {
        PrizeAudio.PlayDelayed(0.12f);
      }
      else
      {
        PrizeAudio.Play();
      }
    }
    else if (sfx == SFX.levelup)
    {
      if (LevelupAudio.isPlaying)
      {
        LevelupAudio.PlayDelayed(0.12f);
      }
      else
      {
        LevelupAudio.Play();
      }
    }
    else if (sfx == SFX.shake)
    {
      if (ShakeAudio.isPlaying)
      {
        ShakeAudio.PlayDelayed(0.12f);
      }
      else
      {
        ShakeAudio.Play();
      }
    }
    else if (sfx == SFX.wall)
    {
      if (NoMoreAudio.isPlaying)
      {
        NoMoreAudio.PlayDelayed(0.12f);
      }
      else
      {
        NoMoreAudio.Play();
      }
    }
    else if (sfx == SFX.nomore)
    {
      if (WallsAudio.isPlaying)
      {
        WallsAudio.PlayDelayed(0.12f);
      }
      else
      {
        WallsAudio.Play();
      }
    }
    else if (sfx == SFX.drop)
    {
      DropAudio.clip = coinsfx[DropCoinClip];
      if (DropAudio.isPlaying)
      {
        DropAudio.PlayDelayed(0.12f);
      }
      else
      {
        DropAudio.Play();
      }
    }

  }
  public void ButtonClicked()
  {
    ButtonAudio.Play();
  }
  public void PlaySFX(AudioClip clip)
  {
    if (GetComponent<AudioSource>().isPlaying)
    {
      StartCoroutine(wait(clip));
    }
    else
    {
      GetComponent<AudioSource>().clip = clip;
      GetComponent<AudioSource>().Play();
    }
  }
  IEnumerator wait(AudioClip clip)
  {
    yield return new WaitForSeconds(0.25f);
    GetComponent<AudioSource>().clip = clip;
    GetComponent<AudioSource>().Play();
  }
}
