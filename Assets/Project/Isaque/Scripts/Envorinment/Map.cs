using UnityEngine;

public class Map : MonoBehaviour
{

    private Quaternion my_rotation;

    private void Start()
    {
        my_rotation = this.transform.rotation;
    }
    private void Update()
    {
        this.transform.rotation = my_rotation;
    }

}
