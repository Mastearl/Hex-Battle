using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private Transform Object1;
    private Transform Object2;
    private Transform Object3;
    private Transform Object4;
    private Transform Object5;
    private Transform Object6;
    private Transform UpLeftHex;
    private Transform LeftHex;
    private Transform DownLeftHex;
    private Transform DownRightHex;
    private Transform RightHex;
    private Transform UpRightHex;
    private Transform NewHexagon;
    private Transform Hexagon;
    public Transform currentArrow;

    private List<Transform> arrows = new List<Transform>();
    //private List<Transform> enemies = new List<Transform>();

    public Transform Enemy01;

    private int index;
    public int MoveCounter;

    public int MaxHealth;
    public int MaxMoves;
    public int PlayerHealth;

    public Text PlayerHealthText;
    public Text MovesLeftText;
    public Text ModeText;

    private bool HitObjectUR;
    private bool HitObjectRR;
    private bool HitObjectDR;
    private bool HitObjectDL;
    private bool HitObjectLL;
    private bool HitObjectUL;
    private bool left;
    private bool right;
    private bool DownHit;

    public bool PlayerTurn;
    public bool EnemyTurn;

    private Ray DownRay;
    private RaycastHit DownRayHit;

    // Use this for initialization
    void Start()
    {
        PlayerHealth = MaxHealth;
        //MovesLeftText.text = "Moves Left: " + MaxMoves;
        //PlayerHealthText.text = "HP: " + PlayerHealth;
        StartPlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealthText.text = "HP: " + PlayerHealth;
        MovesLeftText.text = "Moves Left: " + (MaxMoves - MoveCounter);
        if (PlayerHealth > 0)
        {
            //At the start or when having moved to another cell
            RayInfo();
            SetArrows();
            setTruth();
            GetHitInfo();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                IndexDown();
                left = true;
                right = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                IndexUp();
                right = true;
                left = false;
            }

            CycleThroughList();

            //move player
            if (Input.GetKeyDown(KeyCode.Space) && MoveCounter < MaxMoves)
            {
                MoveThePlayer();
            }

            if(Hexagon.GetComponent<ReleaseHiddenPath>())
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Hexagon.GetComponent<ReleaseHiddenPath>().Reveal = true;
                }
            }
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        if(MoveCounter == 3 && Enemy01.GetComponent<MoveEnemy>().FifthCell)
        {
            SceneManager.LoadScene("Menu");
        }
        if ((MaxMoves - MoveCounter) == 0 && PlayerTurn)
        {
            StartEnemyTurn();
            ModeText.text = "Enemy Turn";
        }
        if (Enemy01.GetComponent<MoveEnemy>().TurnIsOver && EnemyTurn)
        {
            //MoveCounter = 0;
            StartPlayerTurn();
            ModeText.text = "Player Turn";
        }
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            StartEnemyTurn();
        }
        if (Hexagon.GetComponent<FindNeighbor>().EndCell && !Enemy01.gameObject.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    //Use arrows to cycle through path options
    public void IndexUp()
    {
        index++;
    }
    public void IndexDown()
    {
        index--;
    }
    public void CycleThroughList()
    {
        index = index < 0 ? arrows.Count - 1 : index >= arrows.Count ? 0 : index;
        currentArrow = arrows[index];
    }

    //Set Play modes -> Player or Enemy's Turn?
    public void StartPlayerTurn()
    {
        PlayerTurn = true;
        EnemyTurn = false;
        MoveCounter = 0;
    }
    public void StartEnemyTurn()
    {
        EnemyTurn = true;
        PlayerTurn = false;
        if (Enemy01.GetComponent<MoveEnemy>().TurnIsOver )
        {
            Enemy01.GetComponent<MoveEnemy>().TurnIsOver = false;
        }
        if(!Enemy01.gameObject.activeInHierarchy)
        {
            StartPlayerTurn();
        }
    }

    //Make the correct arrow show and remove others
    public void setTruth()
    {
        if (currentArrow == Object1 && HitObjectUR)
        {
            Object1.gameObject.SetActive(true);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(false);
        }
        else if (currentArrow == Object1 && !HitObjectUR)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
        else if (currentArrow == Object2 && HitObjectRR)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(true);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(false);
        }
        else if (currentArrow == Object2 && !HitObjectRR)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
        else if (currentArrow == Object3 && HitObjectDR)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(true);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(false);
        }
        else if (currentArrow == Object3 && !HitObjectDR)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
        else if (currentArrow == Object4 && HitObjectDL)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(true);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(false);
        }
        else if (currentArrow == Object4 && !HitObjectDL)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
        else if (currentArrow == Object5 && HitObjectLL)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(true);
            Object6.gameObject.SetActive(false);
        }
        else if (currentArrow == Object5 && !HitObjectLL)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
        else if (currentArrow == Object6 && HitObjectUL)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(true);
        }
        else if (currentArrow == Object6 && !HitObjectUL)
        {
            if (left)
            {
                IndexDown();
            }
            else if (right)
            {
                IndexUp();
            }
        }
    }

    //Check if there are neighbors
    public void GetHitInfo()
    {
        HitObjectUR = Hexagon.GetComponent<FindNeighbor>().UpRightHit;
        HitObjectRR = Hexagon.GetComponent<FindNeighbor>().RightHit;
        HitObjectDR = Hexagon.GetComponent<FindNeighbor>().DownRightHit;
        HitObjectDL = Hexagon.GetComponent<FindNeighbor>().DownLeftHit;
        HitObjectLL = Hexagon.GetComponent<FindNeighbor>().LeftHit;
        HitObjectUL = Hexagon.GetComponent<FindNeighbor>().UpLeftHit;
    }

    //Ray Down to find base
    public void RayInfo()
    {
        DownRay = new Ray(transform.position, -transform.up);
        Debug.DrawLine(transform.position, transform.position - Vector3.up, Color.white);
        DownHit = Physics.Raycast(DownRay, out DownRayHit, 5.0f);
        Hexagon = DownRayHit.transform.parent;
    }

    //Get neighboring objects to travel to
    public void SetArrows()
    {
        Object1 = Hexagon.GetComponent<FindNeighbor>().UpRightArrow;
        if (Hexagon.GetComponent<FindNeighbor>().UpRightBody != null )//&& Hexagon == NewHexagon)
        {
            UpRightHex = Hexagon.GetComponent<FindNeighbor>().UpRightBody.parent;
        }
        Object2 = Hexagon.GetComponent<FindNeighbor>().RightArrow;
        if (Hexagon.GetComponent<FindNeighbor>().RightBody != null)//&& Hexagon == NewHexagon)
        {
            RightHex = Hexagon.GetComponent<FindNeighbor>().RightBody.parent;
        }
        Object3 = Hexagon.GetComponent<FindNeighbor>().DownRightArrow;
        if (Hexagon.GetComponent<FindNeighbor>().DownRightBody != null)//&& Hexagon == NewHexagon)
        {
            DownRightHex = Hexagon.GetComponent<FindNeighbor>().DownRightBody.parent;
        }
        Object4 = Hexagon.GetComponent<FindNeighbor>().DownLeftArrow;
        if (Hexagon.GetComponent<FindNeighbor>().DownLeftBody != null)// && Hexagon == NewHexagon)
        {
            DownLeftHex = Hexagon.GetComponent<FindNeighbor>().DownLeftBody.parent;
        }
        Object5 = Hexagon.GetComponent<FindNeighbor>().LeftArrow;
        if (Hexagon.GetComponent<FindNeighbor>().LeftBody != null)// && Hexagon == NewHexagon)
        {
            LeftHex = Hexagon.GetComponent<FindNeighbor>().LeftBody.parent;
        }
        Object6 = Hexagon.GetComponent<FindNeighbor>().UpleftArrow;
        if (Hexagon.GetComponent<FindNeighbor>().UpLeftBody != null)//&& Hexagon == NewHexagon)
        {
            UpLeftHex = Hexagon.GetComponent<FindNeighbor>().UpLeftBody.parent;
        }

        if (arrows.Count < 6)
        {
            arrows.Add(Object1);
            arrows.Add(Object2);
            arrows.Add(Object3);
            arrows.Add(Object4);
            arrows.Add(Object5);
            arrows.Add(Object6);
        }
    }

    //Move to neighboring cell
    public void MoveThePlayer()
    {
        if (currentArrow.gameObject.activeInHierarchy)
        {
            Object1.gameObject.SetActive(false);
            Object2.gameObject.SetActive(false);
            Object3.gameObject.SetActive(false);
            Object4.gameObject.SetActive(false);
            Object5.gameObject.SetActive(false);
            Object6.gameObject.SetActive(false);


            if (currentArrow == Object1 && HitObjectUR)
            {
                //Attack
                if (UpRightHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (UpRightHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = UpRightHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = UpRightHex.transform.position + 0.2f * Vector3.up;
                    }
                    if (!UpRightHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = UpRightHex.transform;
                }
            }
            else if (currentArrow == Object2 && HitObjectRR)
            {
                //Attack
                if (RightHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (RightHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = RightHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = RightHex.transform.position + 0.2f * Vector3.up;
                    }                        
                    if (!RightHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = RightHex.transform;
                }
            }
            else if (currentArrow == Object3 && HitObjectDR)
            {
                //Attack
                if (DownRightHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (DownRightHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = DownRightHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = DownRightHex.transform.position + 0.2f * Vector3.up;
                    }                    
                    if (!DownRightHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = DownRightHex.transform;
                }
            }
            else if (currentArrow == Object4 && HitObjectDL)
            {
                //Attack
                if (DownLeftHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (DownLeftHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = DownLeftHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = DownLeftHex.transform.position + 0.2f * Vector3.up;
                    }
                    if (!DownLeftHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = DownLeftHex.transform;
                }
            }
            else if (currentArrow == Object5 && HitObjectLL)
            {
                //Attack
                if (LeftHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (LeftHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = LeftHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = LeftHex.transform.position + 0.2f * Vector3.up;
                    }
                    if (!LeftHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = LeftHex.transform;
                }
            }
            else if (currentArrow == Object6 && HitObjectUL)
            {
                //Attack
                if (UpLeftHex.GetComponent<FindNeighbor>().EnemyCell)
                {
                    Enemy01.GetComponent<MoveEnemy>().EnemyHealth--;
                    MoveCounter++;
                }
                //Move
                else
                {
                    if (UpLeftHex.GetComponent<FindNeighbor>().TallCell)
                    {
                        transform.position = UpLeftHex.transform.position + 1.0f * Vector3.up;
                    }
                    else
                    {
                        transform.position = UpLeftHex.transform.position + 0.2f * Vector3.up;
                    }                    
                    if (!UpLeftHex.GetComponent<FindNeighbor>().hasLanded)
                    {
                        MoveCounter++;
                    }
                    Hexagon.GetComponent<FindNeighbor>().StartCell = false;
                    NewHexagon = UpLeftHex.transform;
                }
            }

            arrows[0] = NewHexagon.GetComponent<FindNeighbor>().UpRightArrow;
            arrows[1] = NewHexagon.GetComponent<FindNeighbor>().RightArrow;
            arrows[2] = NewHexagon.GetComponent<FindNeighbor>().DownRightArrow;
            arrows[3] = NewHexagon.GetComponent<FindNeighbor>().DownLeftArrow;
            arrows[4] = NewHexagon.GetComponent<FindNeighbor>().LeftArrow;
            arrows[5] = NewHexagon.GetComponent<FindNeighbor>().UpleftArrow;
        }
        else
        {
            Debug.Log("err");
            index++;
        }
    }
}