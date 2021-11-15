using UnityEngine;

public class Malus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            Player.Instance.OnMalusHit.Invoke();

        PoolingSystem.Instance.AddToMalusPool(this.gameObject);
    }
}
