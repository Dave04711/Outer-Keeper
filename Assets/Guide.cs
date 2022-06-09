using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject veroPanel;
    [SerializeField] GameObject gregPanel;
    [SerializeField] GameObject richPanel;

    [Space]

    [SerializeField] TMP_Text guideChat;
    [SerializeField] TMP_Text veroChat;
    [SerializeField] TMP_Text gregChat;
    [SerializeField] TMP_Text richChat;

    [Space]

    [SerializeField] Transform guide;

    [Space]

    [SerializeField][NonReorderable] List<GuidePoint> points;//NonReorderable 8P
    [Space]
    [SerializeField] float speed = 1;
    [SerializeField] float range = 3;

    bool canMove = true;
    bool canTouch = true;
    bool has2Wait = false;
    int index = 0;

    [SerializeField] GameObject defNPC1, defNPC2, defNPC3;
    [SerializeField] GameObject tutNPCParent;

    private void Start()
    {
        if(PlayerPrefs.HasKey("tutorial") && PlayerPrefs.GetInt("tutorial") != 0)
        {
            defNPC1.SetActive(true);
            defNPC2.SetActive(true);
            defNPC3.SetActive(true);
            Destroy(guidePanel);
            Destroy(veroPanel);
            Destroy(gregPanel);
            Destroy(richPanel);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        has2Wait = Vector2.Distance(guide.position, Player.instance.transform.position) >= range && index > 2;
        if (has2Wait) 
        {
            guide.GetComponent<Animator>().SetBool("isMoving", false);
            return;
        }
        if(index >= points.Count) 
        {
            tutNPCParent.SetActive(false);
            defNPC1.SetActive(true);
            defNPC2.SetActive(true);
            defNPC3.SetActive(true);
            PlayerPrefs.SetInt("tutorial", 1);
            return;
        }
        canMove = !IsClose2Point();
        if (canMove)
        {
            Move();
        }
        else
        {
            guide.GetComponent<Animator>().SetBool("isMoving", false);
            ShowChat(points[index].whichDialogue);
            ReloadTxt(points[index].whichDialogue, points[index].txt[points[index].index]);
        }
    }

    bool IsClose2Point(float minDistance = .2f)
    {
        return Vector2.Distance(guide.position,points[index].target.position) < minDistance;
    }

    void Move()
    {
        guide.position = Vector3.MoveTowards(guide.position, points[index].target.position, Time.deltaTime * speed);
        guide.GetComponent<Animator>().SetBool("isMoving", true);
        if(points[index].target.position.x >= guide.position.x) { guide.eulerAngles = Vector2.zero; }
        else { guide.eulerAngles = Vector2.up * 180; }
    }

    void ShowChat(NPC _p)
    {
        switch (_p)
        {
            case NPC.Guide:
                guidePanel.SetActive(true);
                break;
            case NPC.Veronica:
                veroPanel.SetActive(true);
                break;
            case NPC.Gregory:
                gregPanel.SetActive(true);
                break;
            case NPC.Richard:
                richPanel.SetActive(true);
                break;
            default:
                index++;
                break;
        }
    }

    void ReloadTxt(NPC _p, string txt)
    {
        switch (_p)
        {
            case NPC.Guide:
                StartCoroutine(ShowText(guideChat, txt));
                break;
            case NPC.Veronica:
                StartCoroutine(ShowText(veroChat, txt));
                break;
            case NPC.Gregory:
                StartCoroutine(ShowText(gregChat, txt));
                break;
            case NPC.Richard:
                StartCoroutine(ShowText(richChat, txt));
                break;
                default: break;
        }
    }

    public void NextText()
    {
        if (canTouch)
        {
            points[index].index++;
            if (points[index].index >= points[index].txt.Length)
            {
                CloseChat();
                index++;
            }
            else
            {
                ReloadTxt(points[index].whichDialogue, points[index].txt[points[index].index]);
            } 
        }
    }

    void CloseChat()
    {
        guidePanel.SetActive(false);
        veroPanel.SetActive(false);
        gregPanel.SetActive(false);
        richPanel.SetActive(false);
    }

    IEnumerator ShowText(TMP_Text text, string txt)
    {
        canTouch = false;
        text.text = "";
        for (int i = 0; i < txt.Length; i++)
        {
            text.text += txt[i];
            yield return null;
        }
        canTouch = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (points.Count > 1)
        {
            Gizmos.color = Color.red;
            for (int i = 1; i < points.Count; i++)
            {
                Gizmos.DrawLine(points[i - 1].target.position, points[i].target.position);
            }
        }
        Gizmos.DrawWireSphere(guide.position, range);
    }
}

[System.Serializable]
public class GuidePoint
{
    public Transform target;
    public string[] txt;
    public int index;
    public NPC whichDialogue;
}
public enum NPC { None, Guide, Veronica, Gregory, Richard }