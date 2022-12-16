using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;
    TerrainGeneration _terrain;
    float xMin, xMax, yMin, yMax; // camera bounds

    private void Start() 
    {
        _terrain = GameObject.FindGameObjectWithTag("TerrainGenerator").GetComponent<TerrainGeneration>();

        if (!_terrain)
        {
            Debug.LogWarning("No terrain generator in the scene. Camera will not be bound");
            return;
        }

        CalculateCameraBounds(out xMin , out xMax, out yMin , out yMax);
    }

    private void CalculateCameraBounds(out float xMin , out float xMax, out float yMin , out float yMax)
    {
        float camHeight = 2f * gameObject.GetComponent<Camera>().orthographicSize;
        float camWidth = camHeight * gameObject.GetComponent<Camera>().aspect;
        _terrain.GetTerrainSize(out float tHeight, out float tWidth);
        xMin = camWidth/2;
        xMax = tWidth - camWidth/2;
        yMax = 0;
        yMin = (tHeight - camHeight/2) * -1;
    }

    private void FixedUpdate()
    {
        if (!player)
            return;

        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * 2);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax),
                                         Mathf.Clamp(transform.position.y, yMin, yMax),
                                         transform.position.z);

    }
}
