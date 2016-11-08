using UnityEngine;

public class Coin : MonoBehaviour
{
    public int worth = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        FindObjectOfType<GM>().CoinWasPickedUp(worth);
    }
}
