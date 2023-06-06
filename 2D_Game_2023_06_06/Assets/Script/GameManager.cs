using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public int TotalPoint;
    public int StagePoint;
    public int StageIndex;
    public int Hp;
    public Player player;
    public GameObject[] Stages;
    public AudioSource audioSource;
    public AudioClip AudioClear;

    public Image[] UIhealth;
    public Text Total;
    public Text UIStage;
    public GameObject RestarBTN;
    public GameObject ClearBTN;
    public GameObject MenuBtn;
    public Button CloseBtn;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Total.text = (TotalPoint + StagePoint).ToString();

        if (Input.GetButtonDown("Cancel"))
        {
            if (MenuBtn.activeSelf)
            {
                MenuBtn.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                MenuBtn.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void NextStage()
    {
        if (StageIndex < Stages.Length-1)
        {
            Stages[StageIndex].SetActive(false);
            StageIndex++;
            Stages[StageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (StageIndex + 1);
        }
        else
        {
            
            //Text btnText = RestarBTN.GetComponentInChildren<Text>();
            Debug.Log("깃발");
            Clear();
            //btnText.text = "축하드립니다";
        }
        TotalPoint += StagePoint;
        StagePoint = 0;
    }

    public void HpDown()
    {
        if (Hp > 0)
        {
            Hp--;
            UIhealth[Hp].color = new Color(1, 0, 0, 0.2f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.2f);
            player.OnDie();
            audioSource.Stop();
            RestarBTN.SetActive(true);
            //SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HpDown();
        }       
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-14, -3, 0);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Clear()
    {
        Time.timeScale = 0;
        audioSource.Stop();
        audioSource.clip = AudioClear;
        audioSource.Play();
        audioSource.loop = false;
 
        ClearBTN.SetActive(true);
        //Application.Quit();
    }

    
}
