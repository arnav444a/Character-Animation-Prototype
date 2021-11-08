using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject particleEffect = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GameObject obj = Instantiate ( particleEffect );
            obj.transform.position = this.transform.position;

            Destroy ( this.gameObject );
        }
    }
}
