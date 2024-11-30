using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("----ARABA AYARLARI")]
    public GameObject[] Arabalar;
    public int KacArabaOlsun;
    int KalanAracSayisiDegeri;
    int AktifAracIndex;
    

    [Header("----CANVAS AYARLARI")]
    public GameObject[] ArabaCanvasGorselleri;
    public Sprite AracGeldiGorseli;
    public TextMeshProUGUI KalanAracSayisi;
    public TextMeshProUGUI[] Textler;
    public GameObject[] Panellerim;
    public GameObject[] TapToButonlar;
 
    [Header("----PLATFORM AYARLARI")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;
    bool DonusVarmi;

    [Header("----LEVEL AYARLARI")]
    public int ElmasSayisi;
    public ParticleSystem PatlamaEfekti;
    public AudioSource[] Sesler;
    public bool YukselecekPlatformVarMi;
    bool DokunmaKilidi;


    void Start()
    {
        DokunmaKilidi = true;
        DonusVarmi=true;
        VarsayilanDegerleriKontrolEt();
        KalanAracSayisiDegeri = KacArabaOlsun;
        KalanAracSayisi.text=KalanAracSayisiDegeri.ToString();
         
        for (int i = 0; i < KacArabaOlsun; i++)
        {
            ArabaCanvasGorselleri[i].SetActive(true);
        }
        
        
    }

    public void YeniArabaGetir()
    {
        KalanAracSayisiDegeri--;
        if (AktifAracIndex < KacArabaOlsun)
        {
            Arabalar[AktifAracIndex].SetActive(true);
        }
        else
        {
            Kazandin();
        }

        
        
        ArabaCanvasGorselleri[AktifAracIndex-1].GetComponent<Image>().sprite = AracGeldiGorseli;
        
        KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();
        
    }
    
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch dokunma = Input.GetTouch(0);
            if (dokunma.phase == TouchPhase.Began)
            {
                if (DokunmaKilidi)
                {
                    Panellerim[0].SetActive(false);
                    Panellerim[3].SetActive(true);
                    DokunmaKilidi = false;
                }
                else
                {
                    Arabalar[AktifAracIndex].GetComponent<Araba>().ilerle = true;
                    AktifAracIndex++;
                }
            }
        }

        if (DonusVarmi)
        {
            Platform_1.transform.Rotate(new Vector3(0, 0, DonusHizlari[0]), Space.Self);
            if (Platform_2 != null)
            {
                Platform_2.transform.Rotate(new Vector3(0, 0, -DonusHizlari[1]), Space.Self);
            }
            
        }
        
    }


    public void Kaybettin()
    {
        DonusVarmi = false;
        //PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi);
        Textler[6].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[7].text = SceneManager.GetActiveScene().name;
        Textler[8].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[9].text = ElmasSayisi.ToString();
        Sesler[1].Play();
        Sesler[3].Play();
        Panellerim[1].SetActive(true);
        Panellerim[3].SetActive(false);

        Invoke("KaybettinButonuOrtayaCikar", 2f);

    }

    void KaybettinButonuOrtayaCikar()
    {
        TapToButonlar[0].SetActive(true);
    } 

    void KazandinButonuOrtayaCikar()
    {
        TapToButonlar[1].SetActive(true);
    }

    void Kazandin()
    {
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi);
        Textler[2].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[3].text = SceneManager.GetActiveScene().name;
        Textler[4].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[5].text = ElmasSayisi.ToString();
        Sesler[2].Play();
        Panellerim[2].SetActive(true);
        Panellerim[3].SetActive(false);
        Invoke("KazandinButonuOrtayaCikar", 2f);
    }


    // BELLEK YONETIMI

    void VarsayilanDegerleriKontrolEt()
    {
        Textler[0].text=PlayerPrefs.GetInt("Elmas").ToString();
        Textler[1].text = SceneManager.GetActiveScene().name;
    }


    public void izleVeDahaFazlaKazan()
    {

    }

    public void SonrakiLevelGec()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //DokunmaKilidi = true;
    }
}
