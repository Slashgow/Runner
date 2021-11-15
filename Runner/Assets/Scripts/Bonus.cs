using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
            Player.Instance.OnBonusHit.Invoke();

        PoolingSystem.Instance.AddToBonusPool(this.gameObject);
    }
}
