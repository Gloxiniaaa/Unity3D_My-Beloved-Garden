using DG.Tweening;
using ObjectPool;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class Flower : MonoBehaviour, IPooledObject<Flower>
{
    private Collider _collider;
    private IPool<Flower> _flowerPool;

    [Header("Broadcast on:")]
    [SerializeField] private VoidEventChannelSO _onUndoFlowerBloom;
    [SerializeField] private VoidEventChannelSO _onStepOnFlower;

    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _LoadLevelChannel;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        _collider = GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        _LoadLevelChannel.OnEventRaised += SetDefault;
    }

    private void SetDefault(bool arg0)
    {
        transform.localScale = Vector3.zero;
        transform.DOKill();
        _flowerPool.Return(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            Debug.Log("You stepped on a flower!!!");
            _onStepOnFlower.RaiseEvent();
            transform.position += Vector3.down * 0.25f;
            transform.DOKill();
        }
        if (other.CompareTag(Constant.REVERSE_PLAYER_TAG))
        {
            ReverseBloom();
        }
    }

    [ContextMenu("Bloom")]
    public void Bloom()
    {
        _collider.enabled = true;
        transform.DOScale(1.0f, 1.0f).SetEase(Ease.OutBack);
        transform.DORotate(new Vector3(0, 720, 0), 2.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }

    [ContextMenu("Reverse Bloom")]
    public void ReverseBloom()
    {
        _onUndoFlowerBloom.RaiseEvent();
        _collider.enabled = false;
        transform.DOKill();
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() => ReturnToPool());
    }

    private void OnDisable()
    {
        _LoadLevelChannel.OnEventRaised -= SetDefault;
        transform.localScale = Vector3.zero;
        transform.DOKill();
    }

    public void ReturnToPool()
    {
        _flowerPool.Return(this);
    }

    public void AssignPool(IPool<Flower> pool)
    {
        _flowerPool = pool;
    }
}