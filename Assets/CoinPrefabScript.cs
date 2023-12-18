using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CoinPrefabScript : MonoBehaviour
{
    [SerializeField] private Transform coinModel, flares;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    bool canRotate = false;
    GameObject player;
    public void IsPlaced()
    {
        player = GameObject.FindGameObjectWithTag("player");
        canRotate = true;
        coinModel.DOLocalJump(new Vector3(0,.1f, 0), jumpPower, 1, 1f);
        flares.gameObject.SetActive(true);
        coinModel.DOPunchScale(Vector3.up, jumpPower);
        
        coinModel.DORotate(Vector3.up, .1f).SetLoops(-1, LoopType.Incremental);
    }
    public void StartDestroying()
    {
        Collider boxCollider = GetComponent<Collider>();
        boxCollider.enabled = false;
        //DOTween.Clear(coinModel);
        flares.gameObject.SetActive(false);
        coinModel.DOScale(Vector3.zero, .5f).OnComplete(Destroy);

        if (player != null)
        {
            transform.DOMove(player.transform.position, .1f).OnComplete(Destroy);

        }
        else
        {
            Debug.Log( "PLAYER NOT FOUND");
        }

    }
    private void Destroy()
    {
    
        DOTween.Clear(coinModel);
       // Destroy(transform);
    }


}
