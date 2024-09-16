using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Araba : MonoBehaviour
{
    public bool ilerle;
    bool DurusNoktasiDurumu=false;
    
    //
    //
    public GameObject[] TekerIzleri;
    public Transform parent;
    public GameManager _GameManager;


    void Start()
    {
        
    }

    
    void Update()
    {
        if (!DurusNoktasiDurumu)
        {
            transform.Translate(7f * Time.deltaTime * transform.forward);
        }

        if (ilerle)
        {
            transform.Translate(15f*Time.deltaTime*transform.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Parking"))
        {
            ilerle = false;
            TekerIzleri[0].SetActive(false);
            TekerIzleri[1].SetActive(false);
            transform.SetParent(parent);
            
            _GameManager.YeniArabaGetir();
        }

       

        else if (collision.gameObject.CompareTag("Araba"))
        {
            Destroy(gameObject); // obje havuzunda false olmali
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
        }

        else if (other.CompareTag("OrtaGobek"))
        {
            Destroy(gameObject); // obje havuzunda false olmali
            _GameManager.Kaybettin();
        }

        else if (other.CompareTag("onParking"))
        {
            other.gameObject.GetComponent<OnParking>().ParkingAktiflestir();
        }
    }
}
