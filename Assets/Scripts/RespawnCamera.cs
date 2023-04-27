using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RespawnCamera : MonoBehaviour
{
    public Transform forwardPos;
    public Transform backPos;

    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(forwardPos.position, forwardPos.rotation);
        StartCoroutine(DoubleMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoubleMove()
    {
        masterMixer.SetFloat("Volume", Mathf.Log10(0.001f) * 20f);
        yield return new WaitForSeconds(1.0f);

        float curVolume = 0f;
        while (curVolume <= 1f)
        {
            transform.position = Vector3.Lerp(forwardPos.position, backPos.position, curVolume);
            masterMixer.SetFloat("Volume", Mathf.Log10(curVolume) * 20f);
            curVolume += Time.deltaTime / 1f;
            yield return null;
        }
        transform.SetPositionAndRotation(backPos.position, backPos.rotation);

        yield return new WaitForSeconds(5f);

        curVolume = 1f;
        while (curVolume > 0f)
        {
            transform.position = Vector3.Lerp(backPos.position, forwardPos.position, 1 - curVolume);
            masterMixer.SetFloat("Volume", Mathf.Log10(curVolume) * 20f);
            curVolume -= Time.deltaTime / 5f;
            yield return null;
        }
        SceneManager.LoadScene("Floating Islands");
    }
}
