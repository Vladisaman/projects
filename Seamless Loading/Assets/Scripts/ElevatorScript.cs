using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] Animator Door1Anim;
    [SerializeField] Animator Door2Anim;
    [SerializeField] Texture2D FadeTexture;
    [SerializeField] float fadeSpeed;
    int textureDepth = -1000;
    float textureTransparency = 1.0F;
    float fadeDir = -1.0F;

    private void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, textureTransparency);
        GUI.depth = textureDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeTexture);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textureTransparency += fadeDir * fadeSpeed * Time.deltaTime;
        textureTransparency = Mathf.Clamp01(textureTransparency);
    }

    public void OpenDoors()
    {
        Door1Anim.SetTrigger("OpenDoor");
        Door2Anim.SetTrigger("OpenDoor");
    }

    public void SecondFloor()
    {
        StartCoroutine(SceneLoader(1));
    }

    public void FirstFloor()
    {
        StartCoroutine(SceneLoader(0));
    }

    IEnumerator SceneLoader(int Index)
    {
        GetComponent<Animator>().SetTrigger("Fly");
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);
        operation.allowSceneActivation = false;
        fadeDir = 1.5F;
        yield return new WaitForSeconds(3.0F);
        operation.allowSceneActivation = true;
    }
}
