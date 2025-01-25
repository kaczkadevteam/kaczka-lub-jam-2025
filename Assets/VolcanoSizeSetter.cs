using UnityEngine;

public class VolcanoSizeSetter : MonoBehaviour
{
    void Start()
    {
        float SOME_NUMBER = 6f;
        float scale = GlobalConfig.Instance.volcanoSizeFraction * SOME_NUMBER;
        transform.localScale = new Vector3(scale, 5f, scale);
    }
}
