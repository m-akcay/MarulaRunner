using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCube : MonoBehaviour
{
    public static DroppedCube[] DummyCubes = null;
    private const int MAX_IDX = 10;

    private Material mat = null;

    public static void init()
    {
        if (DummyCubes != null)
        {
            for (int i = 0; i < MAX_IDX; i++)
            {
                Destroy(DummyCubes[i]);
            }
        }

        var cubePrefab = Resources.Load("Prefabs/DroppedCube") as GameObject;
        DummyCubes = new DroppedCube[MAX_IDX];
        for (int i = 0; i < MAX_IDX; i++)
        {
            var cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            DummyCubes[i] = cube.GetComponent<DroppedCube>();
        }
    }

    private void OnEnable()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void dropToPosition(Vector3 pos, float height)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        var scale = transform.localScale;
        transform.localScale = new Vector3(scale.x, height * Tower.SCALE, scale.z);
        //mat.mainTextureScale = new Vector2(1, height * Tower.SCALE);
    }

    public static DroppedCube getAvailableCube()
    {
        foreach (var cube in DummyCubes)
        {
            if (!cube.gameObject.activeInHierarchy)
            {
                return cube;
            }
        }

        // won't reach here
        return null;
    }

    public void destroyCube()
    {
        this.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        Destroy(mat);
    }
}
