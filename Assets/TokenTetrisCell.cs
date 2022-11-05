using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenTetrisCell : MonoBehaviour
{
    public GameObject tokenCellPrefab;

    public void init(Token token)
    {
        var go = Instantiate(tokenCellPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TokenCell>().image .sprite = token.info.image;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
