using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class Character : MonoBehaviour {

    private GameObject charactorObject;
    private SpriteRenderer charactorImage;
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private CanvasGroup canvasGroup;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        charactorObject = gameObject;
        charactorImage = charactorObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        LoadImage();
    }

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/Charactors/"+Name).ToList();
        foreach (Sprite s in temp)
        {
            sprites.Add(s.name, s);
        }
    }

    public void SetImage(string imageID)
    {
        charactorImage.sprite = sprites[imageID];
        FadeIn();
    }

    public void Appear()
    {
        charactorObject.SetActive(true);
        FadeIn();
    }

    public void FadeIn()
    {
        charactorImage.color = new Color(1f, 1f, 1f, 0);
        charactorImage.DOFade(1.0f, 0.2f);
    }

    public void Destroy()
    {
        Destroy(this);
    }


}
