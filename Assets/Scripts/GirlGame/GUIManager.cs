using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GUIManager : MonoBehaviour
{
    private enum DeltaType
    {
        SWINGING_AT_PERIOD,
        ROLLING_AT_PERIOD,
        BLINKING_AT_PERIOD,
        UNDER_MESSAGE_WINDOW,
        SPRITE_AS_TEXT
    }

    public Camera MainCamera;
    public Transform ButtonPanel;
    public Button OptionButton;
    public TextMeshProUGUI Text;
    public Text Speaker;
    public GameObject Delta;
    public int SpriteIndex;

    private DeltaType deltaType = DeltaType.SWINGING_AT_PERIOD;

    private float messageSpeed = 0.05f;

    private Sequence textSeq;
    public Sequence TextSeq => textSeq;

    public GameObject PeriodImage;

    private void Start()
    {
        PeriodImage.SetActive(false);
        textSeq = DOTween.Sequence();
        Delta.SetActive(false);
        switch (deltaType)
        {
            case DeltaType.UNDER_MESSAGE_WINDOW:
                Delta.transform.DOMoveY(-0.2f, 1.0f).SetRelative().SetEase(Ease.InCubic)
                    .SetLoops(-1, LoopType.Restart);
                break;
            case DeltaType.SPRITE_AS_TEXT:
                StartCoroutine(BlinkDelta());
                break;
        }
    }

    IEnumerator BlinkDelta()
    {
        while (true)
        {
            if (!DOTween.IsTweening(Text))
            {
                var spriteText = "<rotate=-90><sprite index=\"{0}\"></rotate>";
                switch (SpriteIndex)
                {
                    case int n when 0 <= n && n < 4:
                        Text.text = Text.text.Replace(String.Format(spriteText, SpriteIndex),
                            String.Format(spriteText, SpriteIndex + 1));
                        SpriteIndex++;
                        break;
                    case 4:
                        Text.text = Text.text.Replace(String.Format(spriteText, 4),
                            String.Format(spriteText, 0));
                        SpriteIndex = 0;
                        break;
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SetTextAndAnimate(string text)
    {
        if (textSeq.IsPlaying())
        {
            textSeq.Complete();
        }
        else
        {
            Text.text = text;
            Text.maxVisibleCharacters = 0;
            textSeq = DOTween.Sequence();
            switch (deltaType)
            {
                case DeltaType.SPRITE_AS_TEXT:
                    var spriteText = "<rotate=-90><sprite index=\"0\"></rotate>";
                    textSeq.Append(Text
                        .DOMaxVisibleCharacters(text.Length + spriteText.Length, text.Length * messageSpeed)
                        .SetEase(Ease.Linear).OnComplete(
                            () =>
                            {
                                SpriteIndex = 0;
                                Text.text += spriteText;
                            }
                        ));
                    break;
                case DeltaType.SWINGING_AT_PERIOD:
                case DeltaType.ROLLING_AT_PERIOD:
                case DeltaType.BLINKING_AT_PERIOD:
                    textSeq.Append(Text.DOMaxVisibleCharacters(text.Length, text.Length * messageSpeed)
                        .SetEase(Ease.Linear).OnComplete(
                            () => { SetPeriodImage(); }
                        ));
                    break;
                default:
                    textSeq.Append(Text.DOMaxVisibleCharacters(text.Length, text.Length * messageSpeed)
                        .SetEase(Ease.Linear));
                    break;
            }
        }
    }

    public void SetImageUnderMessageWindow()
    {
        if (deltaType == DeltaType.UNDER_MESSAGE_WINDOW)
        {
            Delta.gameObject.SetActive(!TextSeq.IsPlaying());
        }
    }

    public void SetPeriodImage()
    {
        var info = Text.textInfo.characterInfo[Text.textInfo.characterCount - 1];
        var vec = new Vector3(info.bottomRight.x, info.baseLine, info.bottomRight.z);
        Vector3 worldBottomLeft = Text.transform.TransformPoint(vec);
        Vector3 buttonSpacePos = PeriodImage.gameObject.transform.parent.InverseTransformPoint(worldBottomLeft);
        PeriodImage.gameObject.transform.localPosition = new Vector3(buttonSpacePos.x + 20, buttonSpacePos.y, 0);
        PeriodImage.gameObject.transform.DOKill();
        SetPeriodImageAnimation(PeriodImage.gameObject.transform);
        PeriodImage.gameObject.SetActive(true);
    }

    private void SetPeriodImageAnimation(Transform transform)
    {
        switch (deltaType)
        {
            case DeltaType.SWINGING_AT_PERIOD:
            {
                transform.DOLocalMoveY(-20, 0.5f)
                    .SetRelative()
                    .SetEase(Ease.InCubic)
                    .SetLoops(-1, LoopType.Restart);
                break;
            }
            case DeltaType.ROLLING_AT_PERIOD:
            {
                transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.5f)
                    .SetRelative()
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);
                break;
            }
            case DeltaType.BLINKING_AT_PERIOD:
            {
                var image = transform.GetComponent<Image>();
                var color = image.color;
                color = new Color(color.r, color.g, color.b, 1f);
                image.color = color;
                image.DOKill();
                image.DOFade(0f, 0.5f)
                    .SetEase(Ease.InCubic)
                    .SetLoops(-1, LoopType.Restart);
                break;
            }
        }
    }
}