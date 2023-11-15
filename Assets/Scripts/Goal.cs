using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Paddle _paddle;
    [SerializeField] private BouncingBall _ball;
    
    [SerializeField] private float viewAngleRange = 180f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsBallPass())
        {
            _ball.ResetBall();
            _paddle.IncreaseScore();
        }
    }
    
    private bool IsBallPass()
    {
        Vector3 directionToPlayer = _ball.transform.position - transform.position;
        float dotProduct = DotProduct(NormalizeVector(directionToPlayer), transform.right);
        
        return dotProduct > ConvertViewAngle(viewAngleRange);
    }
    
    private float ConvertViewAngle(float angle)
    {
        return Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }
    
    private float DotProduct(Vector3 firstPos, Vector3 secondPos)
    {
        float xProduct = firstPos.x * secondPos.x;
        float yProduct = firstPos.y * secondPos.y;
        float zProduct = firstPos.z * secondPos.z;
        
        return xProduct + yProduct + zProduct;
    }

    private float Magnitude(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }

    private Vector3 NormalizeVector(Vector3 v)
    {
        float mag = Magnitude(v);

        v.x /= mag;
        v.y /= mag;
        v.z /= mag;
        
        return v;
    }
    
    
}