using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NN.PathFinding;

public class TestPos : MonoBehaviour
{
    public NN.PathFinding.Grid grid1l;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        Debug.Log(new Vector3(grid1l.GetNode(new Vector3(this.transform.position.x, 0, this.transform.position.z)).GridLocalPosX, grid1l.GetNode(new Vector3(this.transform.position.x, 0, this.transform.position.z)).GridLocalPosY));
        
    }
}
