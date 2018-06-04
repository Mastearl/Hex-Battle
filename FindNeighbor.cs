using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNeighbor : MonoBehaviour
{

    public Transform Player;
    public Transform Enemy01;

    public RaycastHit UpRightRayHit;
    public RaycastHit RightRayHit;
    public RaycastHit DownRightRayHit;
    public RaycastHit UpLeftRayHit;
    public RaycastHit LeftRayHit;
    public RaycastHit DownLeftRayHit;

    private Ray UpRightRay;
    private Ray RightRay;
    private Ray DownRightRay;
    private Ray UpLeftRay;
    private Ray LeftRay;
    private Ray DownLeftRay;

    private RaycastHit UpRayHit;
    private Ray UpRay;
    public bool UpHit;
    public bool HasPlayerOn;
    public bool hasLanded;

    public bool UpRightHit;
    public bool RightHit;
    public bool DownRightHit;
    public bool UpLeftHit;
    public bool LeftHit;
    public bool DownLeftHit;

    public bool StartCell;
    public bool EnemyCell;
    public bool EndCell;
    public bool TallCell;

    public bool ULB;
    public bool URB;
    public bool RRB;
    public bool LLB;
    public bool DRB;
    public bool DLB;

    public Transform UpRightBody;
    public Transform UpLeftBody;
    public Transform LeftBody;
    public Transform RightBody;
    public Transform DownLeftBody;
    public Transform DownRightBody;

    public Transform UpRightArrow;
    public Transform UpleftArrow;
    public Transform LeftArrow;
    public Transform RightArrow;
    public Transform DownRightArrow;
    public Transform DownLeftArrow;

    // Use this for initialization
    void Start ()
    {
        UpRightArrow.gameObject.SetActive(false);
        UpleftArrow.gameObject.SetActive(false);
        LeftArrow.gameObject.SetActive(false);
        RightArrow.gameObject.SetActive(false);
        DownRightArrow.gameObject.SetActive(false);
        DownLeftArrow.gameObject.SetActive(false);

        Player = FindObjectOfType<MovePlayer>().transform;
        if (FindObjectOfType<MoveEnemy>().transform != null)
        {
            Enemy01 = FindObjectOfType<MoveEnemy>().transform;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        DownRay = new Ray(transform.position, -transform.forward);
        Debug.DrawLine(transform.position, transform.position - Vector3.forward, Color.yellow);
        DownHit = Physics.Raycast(DownRay, out DownRayHit, 1.0f);
        */
        if (!TallCell)
        {
            RightRay = new Ray(transform.position, transform.right);
            Debug.DrawRay(transform.position, transform.right, Color.white);
            RightHit = Physics.Raycast(RightRay, out RightRayHit, 1.5f);
            RightBody = RightRayHit.transform;

            LeftRay = new Ray(transform.position, -transform.right);
            Debug.DrawRay(transform.position, -transform.right, Color.white);
            LeftHit = Physics.Raycast(LeftRay, out LeftRayHit, 1.5f);
            LeftBody = LeftRayHit.transform;

            UpRightRay = new Ray(transform.position, 0.866f * transform.forward + 0.5f * transform.right);
            Debug.DrawRay(transform.position, 0.866f * transform.forward + 0.5f * transform.right, Color.white);
            UpRightHit = Physics.Raycast(UpRightRay, out UpRightRayHit, 2.0f);
            UpRightBody = UpRightRayHit.transform;

            DownRightRay = new Ray(transform.position, -0.866f * transform.forward + 0.5f * transform.right);
            Debug.DrawRay(transform.position, -0.866f * transform.forward + 0.5f * transform.right, Color.white);
            DownRightHit = Physics.Raycast(DownRightRay, out DownRightRayHit, 2.0f);
            DownRightBody = DownRightRayHit.transform;

            UpLeftRay = new Ray(transform.position, 0.866f * transform.forward - 0.5f * transform.right);
            Debug.DrawRay(transform.position, 0.866f * transform.forward - 0.5f * transform.right, Color.white);
            UpLeftHit = Physics.Raycast(UpLeftRay, out UpLeftRayHit, 2.0f);
            UpLeftBody = UpLeftRayHit.transform;

            DownLeftRay = new Ray(transform.position, -0.866f * transform.forward - 0.5f * transform.right);
            Debug.DrawRay(transform.position, -0.866f * transform.forward - 0.5f * transform.right, Color.white);
            DownLeftHit = Physics.Raycast(DownLeftRay, out DownLeftRayHit, 2.0f);
            DownLeftBody = DownLeftRayHit.transform;
        }
        else
        {
            RightRay = new Ray(transform.position + 0.5f * Vector3.up, transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, transform.right, Color.white);
            RightHit = Physics.Raycast(RightRay, out RightRayHit, 1.5f);
            RightBody = RightRayHit.transform;

            LeftRay = new Ray(transform.position + 0.5f * Vector3.up, -transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, -transform.right, Color.white);
            LeftHit = Physics.Raycast(LeftRay, out LeftRayHit, 1.5f);
            LeftBody = LeftRayHit.transform;

            UpRightRay = new Ray(transform.position + 0.5f * Vector3.up, 0.866f * transform.forward + 0.5f * transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, 0.866f * transform.forward + 0.5f * transform.right, Color.white);
            UpRightHit = Physics.Raycast(UpRightRay, out UpRightRayHit, 2.0f);
            UpRightBody = UpRightRayHit.transform;

            DownRightRay = new Ray(transform.position + 0.5f * Vector3.up, -0.866f * transform.forward + 0.5f * transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, -0.866f * transform.forward + 0.5f * transform.right, Color.white);
            DownRightHit = Physics.Raycast(DownRightRay, out DownRightRayHit, 2.0f);
            DownRightBody = DownRightRayHit.transform;

            UpLeftRay = new Ray(transform.position + 0.5f * Vector3.up, 0.866f * transform.forward - 0.5f * transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, 0.866f * transform.forward - 0.5f * transform.right, Color.white);
            UpLeftHit = Physics.Raycast(UpLeftRay, out UpLeftRayHit, 2.0f);
            UpLeftBody = UpLeftRayHit.transform;

            DownLeftRay = new Ray(transform.position + 0.5f * Vector3.up, -0.866f * transform.forward - 0.5f * transform.right);
            Debug.DrawRay(transform.position + 0.5f * Vector3.up, -0.866f * transform.forward - 0.5f * transform.right, Color.white);
            DownLeftHit = Physics.Raycast(DownLeftRay, out DownLeftRayHit, 2.0f);
            DownLeftBody = DownLeftRayHit.transform;
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.blue);
        UpRay = new Ray(transform.position, transform.up);
        UpHit = Physics.Raycast(UpRay, out UpRayHit, 1.0f);

        if(StartCell)
        {
            hasLanded = true;
        }

        if (UpRayHit.collider != null)
        {           
            if (UpRayHit.collider.GetComponent<MoveEnemy>())
            {
                if (UpRayHit.collider.GetComponent<MoveEnemy>().IsActive)
                {
                    EnemyCell = true;
                }            
                if (UpLeftBody != null)
                {
                    ULB = UpLeftBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }                
                if (UpRightBody != null)
                {
                    URB = UpRightBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }                
                if (RightBody != null)
                {
                    RRB = RightBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }                
                if (LeftBody != null)
                {
                    LLB = LeftBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }                
                if (DownRightBody != null)
                {
                    DRB = DownRightBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }               
                if (DownLeftBody != null)
                {
                    DLB = DownLeftBody.parent.GetComponent<FindNeighbor>().hasLanded;
                }
            }
            if (UpRayHit.collider.GetComponent<MovePlayer>())
            {
                HasPlayerOn = true;
                if (Player.GetComponent<MovePlayer>().PlayerTurn)
                {
                    hasLanded = true;
                }
            }
            else
            {
                HasPlayerOn = false;
            }
        }
        else
        {
            EnemyCell = false;
            HasPlayerOn = false;
        }

        if (Player.GetComponent<MovePlayer>().MoveCounter == 0 && !StartCell)
        {
            hasLanded = false;
        }
    }
}
