using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Araba : MonoBehaviour
{
    public bool ilerle;
    bool DurusNoktasiDurumu=false;
    float YukselmeDegeri;
    bool PlatformYukselt;

    //
    //
    public GameObject[] TekerIzleri;
    public Transform parent;
    public GameManager _GameManager;
    public GameObject PatlamaPozisyonu;
    


    void Start()
    {
        
    }

    
    void Update()
    {
        if (!DurusNoktasiDurumu)
        {
            transform.Translate(6f * Time.deltaTime * transform.forward);
        }

        if (ilerle)
        {
            transform.Translate(14f*Time.deltaTime*transform.forward);
        }

        if (PlatformYukselt)
        {
            if(YukselmeDegeri> _GameManager.Platform_1.transform.position.y)
            {
                _GameManager.Platform_1.transform.position = Vector3.Lerp(_GameManager.Platform_1.transform.position,
            new Vector3(_GameManager.Platform_1.transform.position.x,
            _GameManager.Platform_1.transform.position.y + 1.6f, _GameManager.Platform_1.transform.position.z), 0.04f);
            }
            else
            {
                PlatformYukselt = false;
            }
                           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Parking"))
        {
            
            Debug.Log("Parking çarpıldı");
            ilerle = false;
            GetComponent<Rigidbody>().isKinematic = true;
            TekerIzleri[0].SetActive(false);
            TekerIzleri[1].SetActive(false);
            transform.SetParent(parent);
            if (_GameManager.YukselecekPlatformVarMi)
            {
                YukselmeDegeri = _GameManager.Platform_1.transform.position.y + 1.3f;
                PlatformYukselt = true;
            }
            
            _GameManager.YeniArabaGetir();

        }

       

        else if (collision.gameObject.CompareTag("Araba"))
        {
            Debug.Log("Arabaya çarpıldı");
            _GameManager.PatlamaEfekti.transform.position = PatlamaPozisyonu.transform.position;
            _GameManager.PatlamaEfekti.Play();
            ilerle = false;
            TekerIzleri[0].SetActive(false);
            TekerIzleri[1].SetActive(false);
            _GameManager.Kaybettin();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DurusNoktasi"))
        {
            DurusNoktasiDurumu = true;
            
        }

        else if (other.gameObject.CompareTag("Elmas"))
        {
            other.gameObject.SetActive(false);
            _GameManager.ElmasSayisi++;
            _GameManager.Sesler[0].Play();
        }

        else if (other.CompareTag("OrtaGobek"))
        {
            Debug.Log("orta göbek çarpıldı");
           
            _GameManager.PatlamaEfekti.transform.position=PatlamaPozisyonu.transform.position;
            _GameManager.PatlamaEfekti.Play();
            ilerle=false;
            TekerIzleri[0].SetActive(false);
            TekerIzleri[1].SetActive(false);
            _GameManager.Kaybettin();
        }

        else if (other.CompareTag("onParking"))
        {
            other.gameObject.GetComponent<OnParking>().ParkingAktiflestir();
        }
    }
}
