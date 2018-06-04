using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveEnemy : MonoBehaviour
{
    public bool IsActive;
    public bool TurnIsOver;

    public bool FirstCell;
    public bool SecondCell;
    public bool ThirdCell;
    public bool FourthCell;
    public bool FifthCell;

    public bool firstPlayer;
    public bool secondPlayer;
    public bool thirdPlayer;
    public bool fourthPlayer;
    public bool fifthPlayer;

    public int TurnNumber;
    public int EnemyHealth;

    private RaycastHit DownRayHit;
    private Ray DownRay;
    public bool DownHit;

    public Transform Player;

    public Transform FirstTargetCell;
    public Transform SecondTargetCell;
    public Transform ThirdTargetCell;
    public Transform FourthTargetCell;
    public Transform FifthTargetCell;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DownRay = new Ray(transform.position, -transform.up);
        Debug.DrawLine(transform.position, transform.position - Vector3.up, Color.cyan);
        DownHit = Physics.Raycast(DownRay, out DownRayHit, 1.0f);

        if(DownRayHit.collider.GetComponent<FindNeighbor>())
        {
            //Debug.Log("hjk");
        }

        if (FirstTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
        {
            firstPlayer = true;
            secondPlayer = false;
            thirdPlayer = false;
            fourthPlayer = false;
            fifthPlayer = false;
        }
        if (SecondTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
        {
            firstPlayer = false;
            secondPlayer = true;
            thirdPlayer = false;
            fourthPlayer = false;
            fifthPlayer = false;
        }
        if (ThirdTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
        {
            firstPlayer = false;
            secondPlayer = false;
            thirdPlayer = true;
            fourthPlayer = false;
            fifthPlayer = false;
        }
        if (FourthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
        {
            firstPlayer = false;
            secondPlayer = false;
            thirdPlayer = false;
            fourthPlayer = true;
            fifthPlayer = false;
        }
        if (FifthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
        {
            firstPlayer = false;
            secondPlayer = false;
            thirdPlayer = false;
            fourthPlayer = false;
            fifthPlayer = true;
        }
        if( !FirstTargetCell.GetComponent<FindNeighbor>().HasPlayerOn    &&
            !SecondTargetCell.GetComponent<FindNeighbor>().HasPlayerOn   &&
            !ThirdTargetCell.GetComponent<FindNeighbor>().HasPlayerOn    &&
            !FourthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn   &&
            !FifthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn    )
        {
            firstPlayer = false;
            secondPlayer = false;
            thirdPlayer = false;
            fourthPlayer = false;
            fifthPlayer = false;
        }

        if (EnemyHealth == 0)
        {
            transform.gameObject.SetActive(false);
        }

        if (Player.GetComponent<MovePlayer>().EnemyTurn && !TurnIsOver)
        {
            StartCoroutine(Example());
            TurnNumber++;

            if (!FirstCell && !SecondCell && !ThirdCell && !FourthCell && !FifthCell)
            {
                if (FirstTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                }
                else if (!FirstTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    transform.position = FirstTargetCell.position + 0.2f * Vector3.up;
                    FirstCell = true;
                }
                TurnIsOver = true;
            }
            else if (FirstCell && !SecondCell && !ThirdCell && !FourthCell && !FifthCell)
            {
                if (SecondTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                }
                else if(!SecondTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    transform.position = SecondTargetCell.position + 0.2f * Vector3.up;
                    SecondCell = true;
                }
                TurnIsOver = true;
            }
            else if (FirstCell && SecondCell && !ThirdCell && !FourthCell && !FifthCell)
            {
                if (ThirdTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                }
                else if(!ThirdTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    transform.position = ThirdTargetCell.position + 0.2f * Vector3.up;
                    ThirdCell = true;
                }
                TurnIsOver = true;
            }
            else if (FirstCell && SecondCell && ThirdCell && !FourthCell && !FifthCell)
            {
                if (FourthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                }
                else if (!FourthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    transform.position = FourthTargetCell.position + 0.2f * Vector3.up;
                    FourthCell = true;
                }
                TurnIsOver = true;
            }
            else if (FirstCell && SecondCell && ThirdCell && FourthCell && !FifthCell)
            {
                if (FifthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                    Player.GetComponent<MovePlayer>().PlayerHealth--;
                }
                else if (!FifthTargetCell.GetComponent<FindNeighbor>().HasPlayerOn)
                {
                    transform.position = FifthTargetCell.position + 0.2f * Vector3.up;
                    FifthCell = true;
                }
                TurnIsOver = true;
            }

            if (FifthCell)
            {
                if(Player.GetComponent<MovePlayer>().MoveCounter == 0)
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
	}


    IEnumerator Example()
    {
        //print(Time.time);
        yield return new WaitForSeconds(2);
        //print(Time.time);
    }
}
