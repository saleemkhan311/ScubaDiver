using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashCollect : MonoBehaviour
{
    // public Transform trashBag;
    private Animator _anim;

    [SerializeField] private List<Trash> trashList = new List<Trash>();
    private int _holdingTrash;
    [SerializeField] private Transform shootPoint;

    [Header("Audio Clip")] [SerializeField]
    private AudioClip pickup;

    [SerializeField] private Transform pCanvas;
    [SerializeField] private TMP_Text holding;
    [SerializeField] private Vector3 pCanvasOffsetPosition;

    private void Start()
    {
        _holdingTrash = 0;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        pCanvas.position = transform.position + pCanvasOffsetPosition;
    }

    public void ThrowTrash(float powerCharge)
    {
        foreach (var trash in trashList)
        {
            trash.Throw(shootPoint.right * (powerCharge * 65));
        }

        trashList.Clear();
        _holdingTrash = 0;
        holding.text = "Holding: " + $"{_holdingTrash}".PadLeft(2, '0');
    }

    // void destroyChild()
    // {
    //     if (trashBag.transform.childCount>= 3)
    //     {
    //         foreach (Transform child in trashBag.transform)
    //         {
    //             Destroy(child.gameObject);
    //         }
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Trash")) return;
        if (_holdingTrash == 3) return;
        var script = collision.gameObject.GetComponent<Trash>();
        if (script.thrown) return;
        GameManager.Singleton.PlayAudio(pickup);
        _anim.SetBool("isPicking", true);
        var trans = collision.transform;
        trans.position = shootPoint.position;
        trans.SetParent(shootPoint);
        script.DisableTrash();
        trashList.Add(script);
        _holdingTrash++;
        holding.text = "Holding: " + $"{_holdingTrash}".PadLeft(2, '0');
        StartCoroutine(Timer(1f));
    }


    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        _anim.SetBool("isPicking", false);
    }
}