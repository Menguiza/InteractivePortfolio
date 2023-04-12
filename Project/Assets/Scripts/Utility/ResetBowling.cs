using System.Collections.Generic;
using UnityEngine;

public class ResetBowling : MonoBehaviour
{
    [SerializeField] private List<Transform> equipment = new List<Transform>();
    
    private Vector3[] initialPos;
    private Quaternion[] initialRot;
    private Rigidbody[] rb;
    private int index;

    private void Awake()
    {
        initialPos = new Vector3[equipment.Count];
        initialRot = new Quaternion[equipment.Count];
        rb = new Rigidbody[equipment.Count];
    }

    private void Start()
    {
        foreach (Transform element in equipment)
        {
            initialPos[index] = element.position;
            initialRot[index] = element.rotation;
            element.TryGetComponent(out rb[index]);
            index++;
        }
    }

    public void Reset()
    {
        index = 0;
        
        foreach (Transform element in equipment)
        {
            element.position = initialPos[index];
            element.rotation = initialRot[index];
            rb[index].velocity = Vector3.zero;
            index++;
        }
    }
}
