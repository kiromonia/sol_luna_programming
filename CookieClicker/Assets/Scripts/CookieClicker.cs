using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ref: http://blog.livedoor.jp/asamasou/archives/217030.html

public class CookieClicker : MonoBehaviour
{
    public GameObject addTextObj;
    public float clickrate = 0.5f;

    int click = 0;
    Text countText, messageText;
    GameObject cookie;
    GameObject canvas;

    void Start ()
    {
        click = 0; // for Debug
        //click = PlayerPrefs.GetInt("ClickCount");
        countText = GameObject.Find("CountText").GetComponent<Text>();
        cookie = GameObject.Find("Cookie");
        canvas = GameObject.Find("Canvas");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        countText.text = "Count:" + click;
    }
	
    void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit && hit.transform.tag == "Cookie") Click();
        }
        switch (click / 10)
        {
            case 0:
                messageText.text = "あなたのクッキーからは生ごみの味がする。";
                break;
            case 1:
                messageText.text = "あなたのクッキーを家族が食べてくれた。";
                break;
            case 2:
                messageText.text = "あなたのクッキーは町の人が並んで買うほど人気がある。";
                break;
            case 3:
                messageText.text = "あなたのクッキーは今や空前の大ブームとなっている。";
                break;
            case 4:
                messageText.text = "この世界であなたのクッキーを知らない生物はいない。";
                break;
            case 5:
                messageText.text = "あなたのクッキーは異世界でも評判がいい。";
                break;
            default:
                break;
        }
    }

    void Click()
    {
        click++;
        GameObject obj = Instantiate(addTextObj);
        obj.transform.position = Input.mousePosition;
        obj.transform.SetParent(canvas.transform);
        obj.AddComponent<ObjectDestroy>();
        countText.text = "Count:" + click;
        StartCoroutine("ClickAnim");
    }

    IEnumerator ClickAnim()
    {
        float time = 0;
        float scale = 1;
        while (time < clickrate / 2)
        {
            time += Time.fixedDeltaTime;
            scale = 1 + time / clickrate / 8;
            cookie.transform.localScale = new Vector2(scale, scale);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while (time < clickrate / 2)
        {
            time += Time.fixedDeltaTime;
            scale -= time / clickrate / 8;
            cookie.transform.localScale = new Vector2(scale, scale);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cookie.transform.localScale = new Vector2(1, 1);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ClickCount", click);
    }
}

public class ObjectDestroy : MonoBehaviour
{
    float destroyTime = 0.5f;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (destroyTime <= timer) Destroy(gameObject);
    }
}