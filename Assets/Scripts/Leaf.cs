using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] private ColourType colourType;
    [SerializeField] private int leafScore;
    [SerializeField] private GameObject leafSprite;
    [SerializeField] private float minAngleSwing;
    [SerializeField] private float maxAngleSwing;
    [SerializeField] private float angleSwingSpeed;
    [SerializeField] private float leafSpinSpeed;
    private Quaternion targetAngle = new Quaternion();

    private void Start() 
    {
        targetAngle = GetTargetAngle();
    }

    private void Update() 
    {
        leafSprite.transform.Rotate(Vector3.forward * leafSpinSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, angleSwingSpeed  * Time.deltaTime);
        if ((transform.eulerAngles - targetAngle.eulerAngles).magnitude < 1f)
        {
            targetAngle = GetTargetAngle();
            //Debug.Log("Target Angle: " + targetAngle.eulerAngles);
        }
    }

    private Quaternion GetTargetAngle()
    {
        if (transform.eulerAngles.z >= 0)
        {
            return Quaternion.Euler( new Vector3(0, 0, -(targetAngle.eulerAngles.z + Random.Range(minAngleSwing, maxAngleSwing))));
        }
        else 
        {
            return Quaternion.Euler( new Vector3(0, 0, ((-targetAngle.eulerAngles.z) + Random.Range(minAngleSwing, maxAngleSwing))));
        }
    }

    public int GetLeafScore()
    {
        return leafScore;
    }

    public ColourType GetLeafColour()
    {
        return colourType;
    }

    public void ResetLeaf()
    {
        leafSprite.transform.position = transform.position;
        leafSprite.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        targetAngle = new Quaternion();
        targetAngle = GetTargetAngle();
    }
}
