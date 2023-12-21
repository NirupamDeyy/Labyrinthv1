using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ImageFaderScript : MonoBehaviour
{
    [SerializeField] private Image fadingImage;

    private void Start()
    {
        DOTween.Clear(fadingImage);
        fadingImage.gameObject.SetActive(false);
        //FadeImageMethod(2, false);
    }
    public void FadeImageMethod(float duration, bool makeTransperant)
    {
        fadingImage.gameObject.SetActive(true);

        fadingImage.DOFade(1, duration).OnComplete(() =>
        {
            if (makeTransperant)
            {
                fadingImage.DOFade(0, duration).OnComplete(() => { fadingImage.gameObject.SetActive(false); });
            }
        });
    }

    
}
