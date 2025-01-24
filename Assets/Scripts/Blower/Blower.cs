using UnityEngine;

public class Blower : MonoBehaviour {
  void Start() {
    
    transform.position = new Vector3(0, 0, -GlobalConfig.Instance.blowerOrbitingRadius);
  }
}