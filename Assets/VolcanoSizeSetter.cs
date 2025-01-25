using UnityEngine;

public class VolcanoSizeSetter : MonoBehaviour
{
    void Start()
    {
        float scaleX = transform.localScale.x * GlobalConfig.Instance.volcanoSizeFraction;
        float scaleZ = transform.localScale.y * GlobalConfig.Instance.volcanoSizeFraction;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, scaleZ);
    }
}
