using UnityEngine;

public class UpgradeVisuals : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5f * Time.deltaTime, transform.position.z);
        transform.localScale += Vector3.one * 0.15f * Time.deltaTime;
        if (transform.position.y > 10) {
            Destroy(gameObject);
        }
    }
}
