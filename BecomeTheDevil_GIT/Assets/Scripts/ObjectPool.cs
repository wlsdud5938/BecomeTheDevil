using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is an object pool, that we use for recycling object
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// An array of prefabs used to create an object in the gameworld
    /// </summary>
    [SerializeField]
    private GameObject[] objectPrefabs;

    /// <summary>
    /// A list of objects in the pool
    /// </summary>
    private List<GameObject> pooledObjects = new List<GameObject>();

    /// <summary>
    /// Gets an object from the pool
    /// </summary>
    /// <param name="type">The type of object that we request</param>
    /// <returns>A GameObject of specific type</returns>
    public GameObject GetObject(string type)        // type과 같은 이름을 가진 GameObject를 반환해줌
    {
        //Check the pool for the object
        foreach (GameObject go in pooledObjects)
        {
            //If the pool contains the object that we need then we reuse it
            if (go.name == type && !go.activeInHierarchy)
            {
                /*gameObject.activeInHierarchy    // 하이어라키상에서 활성상태 여부. activeSelf가 활성상태여도 상위 부모 GameObject가 비활성상태라면 자식인 자신도 비활성상태이므로 activeInHierarchy는 false가 된다. 
                  gameObject.activeSelf    // 부모의 활성여부와 상관없이 오직 자신의 active 상태를 표시한다. 하이어라키상에서 부모에 의해 비활성상태여도 자신은 활성상태라면 activeSelft는 true 이다. */
                //Sets the object as active
                go.SetActive(true);

                //returns the objects
                return go;
            }
        }
        //If the pool didn't contain the object, that we needed then we need to create a new one
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            //If we have a prefab for creating the object
            if (objectPrefabs[i].name == type)
            {
                //We instantiate the prefab of the correct type
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
            }
        }

        //If everything fails return null
        return null;
    }

    /// <summary>
    /// Releases the object in the pool
    /// </summary>
    /// <param name="gameObject"></param>
    public void ReleaseObject(GameObject gameObject)
    {
        //Sets the object to inactive
        gameObject.SetActive(false);
    }
}