using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DelayDespawn());
    }

    IEnumerator DelayDespawn() {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
